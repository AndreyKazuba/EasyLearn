using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EasyLearn.Data.Enums;
using EasyLearn.Data.Helpers;
using EasyLearn.UI.Pages;
using EasyLearn.Data.Constants;
using EasyLearn.Infrastructure.Exceptions;
using EasyLearn.Data.Models;
using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.ExpandedElements;
using EasyLearn.Data.Repositories.Interfaces;
using System.Windows.Media;
using System.Windows.Controls;
using EasyLearn.Infrastructure.Helpers;
using EasyLearn.Infrastructure.UIConstants;
using EasyLearn.VM.Windows;
using EasyLearn.UI;

namespace EasyLearn.VM.ViewModels.Pages
{
    public partial class DictationPageVM : ViewModel
    {
        #region Repositories
        private readonly IEasyLearnUserRepository userRepository;
        private readonly ICommonDictionaryRepository commonDictionaryRepository;
        private readonly IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository;
        private readonly IIrregularVerbRepository irregularVerbRepository;
        #endregion

        #region Private fields
        private bool dictationIsStarted;
        private bool answerTextBoxIsOnDefaultState = true;
        private int currentUserId;
        private int wrongAnswers;
        private bool currentAnswerIsCorrect;
        private DictionaryComboBoxItem selectedDictionaryComboBoxItem;
        #endregion

        #region Private helper props
        private int ItemsInSelectedLoadedDictionary
        {
            get => ExecuteForCurrentDictionaryType(
                    () => cdLoadedDictionary.Relations.Count,
                    () => vpLoadedDictionary.VerbPrepositions.Count,
                    () => ModelConstants.IrregularVerbsCount);
        }
        private int ItemsInSelectedLoadedDictionaryLeftToLearn
        {
            get => ExecuteForCurrentDictionaryType(
                    () => cdLoadedDictionary.Relations.Where(commonRelation => !commonRelation.Studied).Count(),
                    () => vpLoadedDictionary.VerbPrepositions.Where(verbPreposition => !verbPreposition.Studied).Count(),
                    () => irregularVerbRepository.GetAllIrregularVerbs().Where(irregularVerb => irregularVerb.Rating != 100).Count());
        }
        private DictionaryType CurrentDictionaryType => selectedDictionaryComboBoxItem.DictionaryType;
        #endregion

        #region Binding props
        public ObservableCollection<DictionaryComboBoxItem> DictionaryComboBoxItems { get; set; } = new ObservableCollection<DictionaryComboBoxItem>();
        public DictionaryComboBoxItem SelectedDictionaryComboBoxItem
        {
            get { return selectedDictionaryComboBoxItem; }
            set
            {
                selectedDictionaryComboBoxItem = value;
                if (value is null)
                    return;
                LoadSelectedDictionary();
                UpdateDictationLengthSlider();
                SetCurrentDictationSection();
                SetDictationDirectionButtonsVisibility();
            }
        }
        public int DictationLengthSliderMaxValue { get; set; }
        public int DictationLengthSliderMinValue { get; set; }
        public int DictationLengthSliderValue { get; set; }
        public int DictationProgressBarMaxValue { get; set; }
        public int DictationProgressBarValue { get; set; }
        public bool StartButtonIsVisible { get; set; }
        public bool StartButtonIsEnabled { get; set; } = true;
        public bool StopButtonIsVisible { get; set; }
        public bool DictationDirectionButtonsIsVisible { get; set; }
        public string AnswerValue { get; set; }
        public string Grade { get; set; }
        public string DictationWordsCount { get; set; }
        public string DictationAnswersCount { get; set; }
        public string DictationWrongAnswersCount { get; set; }
        public SolidColorBrush PageBackground { get; set; }
        public SolidColorBrush GradeForeground { get; set; }
        #endregion

#pragma warning disable CS8618
        public DictationPageVM(
            IEasyLearnUserRepository userRepository,
            ICommonDictionaryRepository commonDictionaryRepository,
            IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository,
            IIrregularVerbRepository irregularVerbRepository)
        {
            this.userRepository = userRepository;
            this.commonDictionaryRepository = commonDictionaryRepository;
            this.verbPrepositionDictionaryRepository = verbPrepositionDictionaryRepository;
            this.irregularVerbRepository = irregularVerbRepository;
            UpdatePage();
            SetDefaultPageState();
        }
#pragma warning restore CS8618

        #region Commands 
        public Command UpdateDictationLengthSliderCommand { get; private set; }
        public Command StartDictationCommand { get; private set; }
        public Command TryGoNextCommand { get; private set; }
        public Command StopDictationCommand { get; private set; }
        public Command UpdatePageCommand { get; private set; }
        public Command DisableAppWindowNavigationBarCommand { get; private set; }
        public Command EnableAppWindowNavigationBarCommand { get; private set; }
        protected override void InitCommands()
        {
            UpdateDictationLengthSliderCommand = new Command(UpdateDictationLengthSlider);
            StartDictationCommand = new Command(StartDictation);
            TryGoNextCommand = new Command(TryGoNext);
            StopDictationCommand = new Command(StopDictation);
            UpdatePageCommand = new Command(UpdatePage);
            DisableAppWindowNavigationBarCommand = new Command(DisableAppWindowNavigationBar);
            EnableAppWindowNavigationBarCommand = new Command(EnableAppWindowNavigationBar);
        }
        private void UpdateDictationLengthSlider()
        {
            DictationLengthSliderMinValue = ItemsInSelectedLoadedDictionary > 0 ? 1 : 0;
            DictationLengthSliderMaxValue = ItemsInSelectedLoadedDictionary;
            DictationLengthSliderValue = ItemsInSelectedLoadedDictionaryLeftToLearn;
        }
        private void StartDictation() => ExecuteForCurrentDictionaryType(CdStart, VpStart, IvStart);
        private void TryGoNext() => ExecuteForCurrentDictionaryType(CdTryGoNext, VpTryGoNext, IvTryGoNext);
        private void StopDictation()
        {
            SetDefaultPageState();
            SetStopWindow();
            SaveDictationResults();
            commonDictationManager = null;
            vpDictationManager = null;
            ivDictationManager = null;
            UpdatePage();
        }
        public void UpdatePage()
        {
            SetCurrentUserId();
            UpdateDictionaryComboBoxItems();
            LoadSelectedDictionary();
            UpdateDictationLengthSlider();
        }
        private void DisableAppWindowNavigationBar() => App.GetService<AppWindowVM>().DisableNavigationBarCommand.Execute();
        private void EnableAppWindowNavigationBar() => App.GetService<AppWindowVM>().EnableNavigationBarCommand.Execute();
        #endregion

        #region Event handling
        protected override void InitEvents()
        {
            DictationPage.EnterClick += OnEnterClick;
            DictationPage.PromtMouseEnter += OnCdPromtMouseEnter;
            DictationPage.PromtMouseLeave += OnCdPromtMouseLeave;
            DictationPage.PromtMouseEnter += OnVpPromtMouseEnter;
            DictationPage.PromtMouseLeave += OnVpPromtMouseLeave;
            DictationPage.PromtMouseEnter += OnIvPromtMouseEnter;
            DictationPage.PromtMouseLeave += OnIvPromtMouseLeave;
            App.GetService<AppWindowVM>().CurrentPageChanged += OnCurrentPageChanged;
        }
        private void OnEnterClick()
        {
            if (!dictationIsStarted)
                return;

            if (currentAnswerIsCorrect)
                TryGoNext();
            else
                CheckAnswer();
        }
        private void OnCdPromtMouseEnter()
        {
            if (commonDictationManager is null)
                return;
            CdSetPromtValue(commonDictationManager.CurrentAnswerValue);
        }
        private void OnCdPromtMouseLeave()
        {
            if (commonDictationManager is null)
                return;
            CdSetMysteriousPromtValue(commonDictationManager.CurrentAnswerValue);
        }
        private void OnVpPromtMouseEnter()
        {
            if (vpDictationManager is null)
                return;
            VpSetPromtValue(vpDictationManager.CurrentPrepositionValue);
        }
        private void OnVpPromtMouseLeave()
        {
            if (vpDictationManager is null)
                return;
            VpSetMysteriousPromtValue(vpDictationManager.CurrentPrepositionValue);
        }
        private void OnIvPromtMouseEnter()
        {
            if (ivDictationManager is null)
                return;
            ExecuteForCurrentIrregularVerbForm(
                () => IvSetPromtValue(ivDictationManager.CurrentV1Value),
                () => IvSetPromtValue(ivDictationManager.CurrentV2Value),
                () => IvSetPromtValue(ivDictationManager.CurrentV3Value));
        }
        private void OnIvPromtMouseLeave()
        {
            if (ivDictationManager is null)
                return;
            ExecuteForCurrentIrregularVerbForm(
                () => IvSetMysteriousPromtValue(ivDictationManager.CurrentV1Value),
                () => IvSetMysteriousPromtValue(ivDictationManager.CurrentV2Value),
                () => IvSetMysteriousPromtValue(ivDictationManager.CurrentV3Value));
        }
        private void OnCurrentPageChanged()
        {
            if (App.GetService<AppWindowVM>().CurrentPage == Infrastructure.Enums.Page.Dictation)
                UpdatePage();
        }
        #endregion

        #region Private page helpers
        private void LoadSelectedDictionary()
        {
            int? selectedDictionaryId = SelectedDictionaryComboBoxItem?.DictionaryId;
            ExecuteForCurrentDictionaryType(
                () => cdLoadedDictionary = commonDictionaryRepository.GetCommonDictionary(selectedDictionaryId ?? throw new NullReferenceException()),
                () => vpLoadedDictionary = verbPrepositionDictionaryRepository.GetVerbPrepositionDictionary(selectedDictionaryId ?? throw new NullReferenceException()),
                () => { });
        }
        private void SetCurrentUserId()
        {
            int? currentUserId = userRepository.TryGetCurrentUser()?.Id;
            if (!currentUserId.HasValue)
                throw new Exception(ExceptionMessagesHelper.FailedToGetCurrentUserId);
            this.currentUserId = currentUserId.Value;
        }
        private void CheckAnswer() => ExecuteForCurrentDictionaryType(CdCheck, VpCheck, IvCheck);
        private void SetDefaultPageState()
        {
            dictationIsStarted = false;
            currentAnswerIsCorrect = false;
            wrongAnswers = 0;
            currentIrregularVerbForm = IrregularVerbForm.FirstForm;
            CdHideUnitType();
            CdHideSynonyms();
            CdHidePromt();
            CdHideExamples();
            VpHideExamples();
            VpHidePromt();
            VpHideSecondDisplay();
            SetDefaultMainDisplayValue();
            SetDefaultCommentValue();
            SetDefaultAnswerValue();
            VpSetDefaultTranslationValue();
            ShowStartButton();
            SetDictationProgressBarDefaultValue();
            ResetAllIcons();
            SetAnswerTextBoxAsDefault();
            IvHideAllIcons();
            IvSetDefaultFixedAnswerValues();
            IvHidePromt();
            ClearStopWindow();
            IvClearFixedAnswerValues();
        }
        private void SaveDictationResults() => ExecuteForCurrentDictionaryType(
            () =>
            {
                if (commonDictationManager is null)
                    throw new Exception(ExceptionMessagesHelper.DictationManagerIsNull);
                commonDictationManager.SaveDictationResults();
            },
            () =>
            {
                if (vpDictationManager is null)
                    throw new Exception(ExceptionMessagesHelper.DictationManagerIsNull);
                vpDictationManager.SaveDictationResults();
            },
            () =>
            {
                if (ivDictationManager is null)
                    throw new Exception(ExceptionMessagesHelper.DictationManagerIsNull);
                ivDictationManager.SaveDictationResults();
            });
        #endregion

        #region Private UI methods (page)
        private void SetCurrentDictationSection() => ExecuteForCurrentDictionaryType(CdShowSection, VpShowSection, IvShowSection);
        #endregion

        #region Private UI methods (main display)
        private void SetDefaultMainDisplayValue()
        {
            string easyLearn = "Easy Learn";
            CdMainDisplayValue = easyLearn;
            VpMainDisplayValue = easyLearn;
            IvMainDisplayValue = easyLearn;
        }
        #endregion

        #region Private UI methods (wrong and correct icons)
        private void ResetAllIcons()
        {
            IvShowGrayIcons();
        }
        #endregion

        #region Private UI methods (start modal window)
        private void SetDictationDirectionButtonsVisibility()
        {
            if (CurrentDictionaryType == DictionaryType.CommonDictionary)
                DictationDirectionButtonsIsVisible = true;
            else
                DictationDirectionButtonsIsVisible = false;
        }
        private void UpdateDictionaryComboBoxItems()
        {
            IEnumerable<CommonDictionary> commonDictionaries = commonDictionaryRepository.GetUsersCommonDictionaries(currentUserId);
            IEnumerable<VerbPrepositionDictionnary> verbPrepositionDictionnaries = verbPrepositionDictionaryRepository.GetUsersVerbPreposotionDictionaries(currentUserId);
            IEnumerable<DictionaryComboBoxItem> commonDictionaryComboBoxItems = commonDictionaries
                .Select(dictionary => new DictionaryComboBoxItem(StringHelper.NormalizeRegister(dictionary.Name), dictionary.Id, DictionaryType.CommonDictionary, !dictionary.Relations.Any()));
            IEnumerable<DictionaryComboBoxItem> verbPrepositionDictionnaryComboBoxItems = verbPrepositionDictionnaries
                .Select(dictionary => new DictionaryComboBoxItem(StringHelper.NormalizeRegister(dictionary.Name), dictionary.Id, DictionaryType.VerbPrepositionDictionary, !dictionary.VerbPrepositions.Any()));
            DictionaryComboBoxItem irregularVerbDictionaryComboBoxItem = new DictionaryComboBoxItem(DictionaryTypeRussianNames.IrregularVerbDictionary, int.MinValue, DictionaryType.IrregularVerbDictionary);
            List<DictionaryComboBoxItem> dictionaryComboBoxItems = commonDictionaryComboBoxItems.Union(verbPrepositionDictionnaryComboBoxItems).ToList();
            dictionaryComboBoxItems.Add(irregularVerbDictionaryComboBoxItem);
            DictionaryComboBoxItems = new ObservableCollection<DictionaryComboBoxItem>(dictionaryComboBoxItems);
            SelectedDictionaryComboBoxItem = DictionaryComboBoxItems.First(сomboBoxItem => сomboBoxItem.IsEnabled);
        }
        #endregion

        #region Private UI methods (stop modal window)
        private void StopDictationButtonSoftClick() => App.GetService<DictationPage>().stopDictationButton.SoftClick();
        private void ClearStopWindow()
        {
            Grade = string.Empty;
            DictationWordsCount = string.Empty;
            DictationAnswersCount = string.Empty;
            DictationWrongAnswersCount = string.Empty;
        }
        private void SetStopWindow() => ExecuteForCurrentDictionaryType(CdSetStopWindow, VpSetStopWindow, IvSetStopWindow);

        #endregion

        #region Private UI methods (comment)
        private void SetDefaultCommentValue()
        {
            CdCommentValue = string.Empty;
            VpCommentValue = string.Empty;
            IvCommentValue = string.Empty;
        }
        #endregion

        #region Private UI methods (start and stop dictation buttons)
        private void ShowStartButton()
        {
            StartButtonIsVisible = true;
            StopButtonIsVisible = false;
        }
        private void SwitchStartAndStopButtons()
        {
            StartButtonIsVisible = !StartButtonIsVisible;
            StopButtonIsVisible = !StopButtonIsVisible;
        }
        #endregion

        #region Private UI methods (dictation progress bar)
        private void SetDictationProgressBar()
        {
            SetDictationProgressBarMaxValue();
            SetDictationProgressBarDefaultValue();
        }
        private void SetDictationProgressBarMaxValue()
        {
            if (CurrentDictionaryType == DictionaryType.IrregularVerbDictionary)
                DictationProgressBarMaxValue = DictationLengthSliderValue * 3;
            else
                DictationProgressBarMaxValue = DictationLengthSliderValue;
        }
        private void IncreaseDictationProgressBarValue() => DictationProgressBarValue++;
        private void SetDictationProgressBarDefaultValue() => DictationProgressBarValue = 0;
        #endregion

        #region Private UI methods (answer textBox)
        private void FocusAnswerTextBox() => App.GetService<DictationPage>().answerTextBox.Focus();
        private void SetDefaultAnswerValue() => AnswerValue = string.Empty;
        private void SetAnswerTextBoxAsWrong()
        {
            TextBox answerTextBox = App.GetService<DictationPage>().answerTextBox;
            SolidColorBrush background = ColorCodes.EasyRedSuperLight.GetBrushByHex();
            SolidColorBrush foreground = ColorCodes.EasyRed.GetBrushByHex();
            SolidColorBrush border = ColorCodes.EasyRed.GetBrushByHex();
            answerTextBox.Background = background;
            answerTextBox.BorderBrush = border;
            answerTextBox.Foreground = foreground;
            answerTextBoxIsOnDefaultState = false;
        }
        private void SetAnswerTextBoxAsCorrect()
        {
            TextBox answerTextBox = App.GetService<DictationPage>().answerTextBox;
            SolidColorBrush background = ColorCodes.EasyGreenSuperLight.GetBrushByHex();
            SolidColorBrush foreground = ColorCodes.EasyGreen.GetBrushByHex();
            SolidColorBrush border = ColorCodes.EasyGreen.GetBrushByHex();
            answerTextBox.Background = background;
            answerTextBox.BorderBrush = border;
            answerTextBox.Foreground = foreground;
            answerTextBoxIsOnDefaultState = false;
        }
        private void SetAnswerTextBoxAsDefault()
        {
            if (answerTextBoxIsOnDefaultState)
                return;
            TextBox answerTextBox = App.GetService<DictationPage>().answerTextBox;
            SolidColorBrush background = ColorCodes.EasyGrayLight.GetBrushByHex();
            SolidColorBrush foreground = ColorCodes.EasyBlack.GetBrushByHex();
            SolidColorBrush border = ColorCodes.MainColor.GetBrushByHex();
            answerTextBox.Background = background;
            answerTextBox.BorderBrush = border;
            answerTextBox.Foreground = foreground;
            answerTextBoxIsOnDefaultState = true;
        }
        #endregion

        #region Private switch helpers
        private void ExecuteForCurrentDictionaryType(Action commonAction, Action verbPrepositionAction, Action irregularVerbAction)
        {
            switch (CurrentDictionaryType)
            {
                case DictionaryType.CommonDictionary:
                    commonAction();
                    break;
                case DictionaryType.VerbPrepositionDictionary:
                    verbPrepositionAction();
                    break;
                case DictionaryType.IrregularVerbDictionary:
                    irregularVerbAction();
                    break;
            }
        }
        private T ExecuteForCurrentDictionaryType<T>(Func<T> commonFunc, Func<T> verbPrepositionFunc, Func<T> irregularVerbFunc)
        {
            switch (CurrentDictionaryType)
            {
                case DictionaryType.CommonDictionary:
                    return commonFunc();
                case DictionaryType.VerbPrepositionDictionary:
                    return verbPrepositionFunc();
                case DictionaryType.IrregularVerbDictionary:
                    return irregularVerbFunc();
                default:
                    throw new Exception(ExceptionMessagesHelper.NoSuchDictationType);
            }
        }
        private void ExecuteForCurrentIrregularVerbForm(Action V1Action, Action V2Action, Action V3Action)
        {
            switch (currentIrregularVerbForm)
            {
                case IrregularVerbForm.FirstForm:
                    V1Action();
                    break;
                case IrregularVerbForm.SecondForm:
                    V2Action();
                    break;
                case IrregularVerbForm.ThirdForm:
                    V3Action();
                    break;
            }
        }
        #endregion
    }
}
