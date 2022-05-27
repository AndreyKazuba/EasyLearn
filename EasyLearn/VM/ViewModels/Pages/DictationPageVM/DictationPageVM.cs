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
        private DictionaryType CurrentDictionaryType => selectedDictionaryComboBoxItem.DictionaryType;
        #endregion

        #region Binding props
        public ObservableCollection<DictionaryComboBoxItem> DictionaryComboBoxItems { get; set; }
        public DictionaryComboBoxItem SelectedDictionaryComboBoxItem
        {
            get { return selectedDictionaryComboBoxItem; }
            set
            {
                selectedDictionaryComboBoxItem = value;
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
            UpdatePageForNewUser();
            SetDefaultPageState();
        }
#pragma warning restore CS8618

        #region Commands 
        public Command UpdateDictationLengthSliderCommand { get; private set; }
        public Command StartDictationCommand { get; private set; }
        public Command TryGoNextCommand { get; private set; }
        public Command StopDictationCommand { get; private set; }
        public Command UpdatePageForNewUserCommand { get; private set; }
        protected override void InitCommands()
        {
            UpdateDictationLengthSliderCommand = new Command(UpdateDictationLengthSlider);
            StartDictationCommand = new Command(StartDictation);
            TryGoNextCommand = new Command(TryGoNext);
            StopDictationCommand = new Command(StopDictation);
            UpdatePageForNewUserCommand = new Command(UpdatePageForNewUser);
        }
        private void UpdateDictationLengthSlider()
        {
            DictationLengthSliderMinValue = ItemsInSelectedLoadedDictionary > 0 ? 1 : 0;
            DictationLengthSliderMaxValue = ItemsInSelectedLoadedDictionary;
            DictationLengthSliderValue = DictationLengthSliderMaxValue;
        }
        private void StartDictation() => ExecuteForCurrentDictionaryType(CdStart, VpStart, IvStart);
        private void TryGoNext() => ExecuteForCurrentDictionaryType(CdTryGoNext, VpTryGoNext, IvTryGoNext);
        private void StopDictation()
        {
            SetDefaultPageState();
            SetStopWindow();
            commonDictationManager = null;
            vpDictationManager = null;
            ivDictationManager = null;
        }
        public void UpdatePageForNewUser()
        {
            SetCurrentUserId();
            UpdateDictionaryComboBoxItems();
            LoadSelectedDictionary();
            UpdateDictationLengthSlider();
        }
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
        }
        private void OnEnterClick()
        {
            if (!dictationIsStarted)
                return;
            if (currentAnswerIsCorrect)
            {
                TryGoNext();
            }
            else
            {
                CheckAnswer();
            }
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
        #endregion

        #region Private page helpers
        private void LoadSelectedDictionary()
        {
            int selectedDictionaryId = SelectedDictionaryComboBoxItem.DictionaryId;
            ExecuteForCurrentDictionaryType(
                () => cdLoadedDictionary = commonDictionaryRepository.GetCommonDictionary(selectedDictionaryId),
                () => vpLoadedDictionary = verbPrepositionDictionaryRepository.GetVerbPrepositionDictionary(selectedDictionaryId),
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
            IvSetDefaultFixedAnswerValues();
            IvHidePromt();
            SetDefaultPageBackground();
            ClearStopWindow();
        }
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
            VpHideIcons();
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
                .Select(dictionary => new DictionaryComboBoxItem(StringHelper.NormalizeRegister(dictionary.Name), dictionary.Id, DictionaryType.CommonDictionary));
            IEnumerable<DictionaryComboBoxItem> verbPrepositionDictionnaryComboBoxItems = verbPrepositionDictionnaries
                .Select(dictionary => new DictionaryComboBoxItem(StringHelper.NormalizeRegister(dictionary.Name), dictionary.Id, DictionaryType.VerbPrepositionDictionary));
            DictionaryComboBoxItem irregularVerbDictionaryComboBoxItem = new DictionaryComboBoxItem(DictionaryTypeRussianNames.IrregularVerbDictionary, int.MinValue, DictionaryType.IrregularVerbDictionary);
            List<DictionaryComboBoxItem> dictionaryComboBoxItems = commonDictionaryComboBoxItems.Union(verbPrepositionDictionnaryComboBoxItems).ToList();
            dictionaryComboBoxItems.Add(irregularVerbDictionaryComboBoxItem);
            DictionaryComboBoxItems = new ObservableCollection<DictionaryComboBoxItem>(dictionaryComboBoxItems);
            SelectedDictionaryComboBoxItem = DictionaryComboBoxItems[0];
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
            BrushConverter brushConverter = new BrushConverter();
            SolidColorBrush background = brushConverter.ConvertFrom("#f6eeee") as SolidColorBrush ?? throw new Exception();
            SolidColorBrush border = brushConverter.ConvertFrom("#cf222e") as SolidColorBrush ?? throw new Exception();
            answerTextBox.Background = background;
            answerTextBox.BorderBrush = border;
            answerTextBoxIsOnDefaultState = false;
        }
        private void SetAnswerTextBoxAsCorrect()
        {
            TextBox answerTextBox = App.GetService<DictationPage>().answerTextBox;
            BrushConverter brushConverter = new BrushConverter();
            SolidColorBrush background = brushConverter.ConvertFrom("#eff5f1") as SolidColorBrush ?? throw new Exception();
            SolidColorBrush border = brushConverter.ConvertFrom("#2da44e") as SolidColorBrush ?? throw new Exception();
            answerTextBox.Background = background;
            answerTextBox.BorderBrush = border;
            answerTextBoxIsOnDefaultState = false;
        }
        private void SetAnswerTextBoxAsDefault()
        {
            if (answerTextBoxIsOnDefaultState)
                return;
            TextBox answerTextBox = App.GetService<DictationPage>().answerTextBox;
            BrushConverter brushConverter = new BrushConverter();
            SolidColorBrush background = brushConverter.ConvertFrom("#f2f2f2") as SolidColorBrush ?? throw new Exception();
            SolidColorBrush border = brushConverter.ConvertFrom("#bfbfbf") as SolidColorBrush ?? throw new Exception();
            answerTextBox.Background = background;
            answerTextBox.BorderBrush = border;
            answerTextBoxIsOnDefaultState = true;
        }
        #endregion

        #region Private UI methods (page background)
        private void SetDefaultPageBackground() => PageBackground = new BrushConverter().ConvertFrom("#ffffff") as SolidColorBrush ?? throw new Exception();
        private void SetCorrectPageBackground() => PageBackground = new BrushConverter().ConvertFrom("#eff5f1") as SolidColorBrush ?? throw new Exception();
        private void SetWrongPageBackground() => PageBackground = new BrushConverter().ConvertFrom("#f6eeee") as SolidColorBrush ?? throw new Exception();
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
