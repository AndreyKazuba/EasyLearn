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
        private int currentUserId;
        private int wrongAnswers;
        private DictionaryComboBoxItem selectedDictionaryComboBoxItem;
        #endregion

#pragma warning disable CS8618
        public DictationPageVM(IEasyLearnUserRepository userRepository,
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

        #region Helper props
        private int ItemsInSelectedLoadedDictionary
        {
            get => ExecuteForCurrentDictionaryType(
                    () => cdLoadedDictionary.Relations.Count,
                    () => vpLoadedDictionary.VerbPrepositions.Count,
                    () => ModelConstants.IrregularVerbsCount);
        }
        private DictionaryType CurrentDictionaryType => selectedDictionaryComboBoxItem.DictionaryType;
        #endregion

        #region Props for binding
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
                SetDictationDirestionButtonsVisibility();
            }
        }
        public int DictationLengthSliderMaxValue { get; set; }
        public int DictationLengthSliderMinValue { get; set; }
        public int DictationLengthSliderValue { get; set; }
        public int DictationProgressBarMaxValue { get; set; }
        public int DictationProgressBarValue { get; set; }
        public bool CheckAnswerButtonIsVisible { get; set; }
        public bool NextButtonIsVisible { get; set; }
        public bool StartButtonIsVisible { get; set; }
        public bool StartButtonIsEnabled { get; set; } = true;
        public bool StopButtonIsVisible { get; set; }
        public bool DictationDirectionButtonsIsVisible { get; set; }
        public string AnswerValue { get; set; }
        #endregion

        #region Commands 
        public Command UpdateDictationLengthSliderCommand { get; private set; }
        public Command StartDictationCommand { get; private set; }
        public Command CheckAnswerCommand { get; private set; }
        public Command TryGoNextCommand { get; private set; }
        public Command StopDictationCommand { get; private set; }
        public Command UpdatePageForNewUserCommand { get; private set; }
        protected override void InitCommands()
        {
            this.UpdateDictationLengthSliderCommand = new Command(UpdateDictationLengthSlider);
            this.StartDictationCommand = new Command(StartDictation);
            this.CheckAnswerCommand = new Command(CheckAnswer);
            this.TryGoNextCommand = new Command(TryGoNext);
            this.StopDictationCommand = new Command(StopDictation);
            this.UpdatePageForNewUserCommand = new Command(UpdatePageForNewUser);
        }
        #endregion

        #region Command logic methods
        private void UpdateDictationLengthSlider()
        {
            this.DictationLengthSliderMinValue = this.ItemsInSelectedLoadedDictionary > 0 ? 1 : 0;
            this.DictationLengthSliderMaxValue = this.ItemsInSelectedLoadedDictionary;
            this.DictationLengthSliderValue = this.DictationLengthSliderMaxValue;
        }
        private void StartDictation() => ExecuteForCurrentDictionaryType(CdStart, VpStart, IvStart);
        private void CheckAnswer() => ExecuteForCurrentDictionaryType(CdCheck, VpCheck, IvCheck);
        private void TryGoNext() => ExecuteForCurrentDictionaryType(CdTryGoNext, VpTryGoNext, IvTryGoNext);
        private void StopDictation()
        {
            SetDefaultPageState();
            this.commonDictationManager = null;
            this.vpDictationManager = null;
            this.ivDictationManager = null;
        }
        public void UpdatePageForNewUser()
        {
            SetCurrentUserId();
            UpdateDictionaryComboBoxItems();
            LoadSelectedDictionary();
            UpdateDictationLengthSlider();
        }
        #endregion

        #region Other private methods
        private void SetCurrentUserId()
        {
            int? currentUserId = userRepository.TryGetCurrentUser()?.Id;
            if (!currentUserId.HasValue)
                throw new Exception(ExceptionMessagesHelper.FailedToGetCurrentUserId);
            this.currentUserId = currentUserId.Value;
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
            this.DictionaryComboBoxItems = new ObservableCollection<DictionaryComboBoxItem>(dictionaryComboBoxItems);
            this.SelectedDictionaryComboBoxItem = this.DictionaryComboBoxItems[0];
        }
        private void LoadSelectedDictionary()
        {
            int selectedDictionaryId = this.SelectedDictionaryComboBoxItem.DictionaryId;
            ExecuteForCurrentDictionaryType(
                () => this.cdLoadedDictionary = commonDictionaryRepository.GetCommonDictionary(selectedDictionaryId),
                () => this.vpLoadedDictionary = verbPrepositionDictionaryRepository.GetVerbPrepositionDictionary(selectedDictionaryId),
                () => { });
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
            if (!this.dictationIsStarted)
                return;
            if (this.CheckAnswerButtonIsVisible)
                CheckAnswer();
            else
                TryGoNext();
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

        #region Display actions
        private void SetDefaultPageState()
        {
            this.dictationIsStarted = false;
            this.wrongAnswers = 0;
            this.currentIrregularVerbForm = IrregularVerbForm.FirstForm;
            CdHideUnitType();
            CdHideAnotherAnswers();
            CdHidePromt();
            VpHidePromt();
            VpHideSecondDisplay();
            SetDefaultMainDisplayValue();
            SetDefaultCommentValue();
            SetDefaultAnswerValue();
            VpSetDefaultTranslationValue();
            ShowStartButton();
            ShowCheckButton();
            SetDictationProgressBarDefaultValue();
            ResetAllIcons();
            IvSetDefaultFixedAnswerValues();
            IvHidePromt();
        }
        private void SetDefaultAnswerValue() => this.AnswerValue = String.Empty;
        private void SetDefaultMainDisplayValue()
        {
            string easyLearn = "Easy Learn";
            this.CdMainDisplayValue = easyLearn;
            this.VpMainDisplayValue = easyLearn;
            this.IvMainDisplayValue = easyLearn;
        }
        private void SetDefaultCommentValue()
        {
            this.CdCommentValue = String.Empty;
            this.VpCommentValue = String.Empty;
            this.IvCommentValue = String.Empty;
        }
        private void ResetAllIcons()
        {
            CdHideIcons();
            VpHideIcons();
            IvShowGrayIcons();
        }
        private void FocusAnswerTextBox() => App.GetService<DictationPage>().dictationTextBox.Focus();
        private void SetCurrentDictationSection() => ExecuteForCurrentDictionaryType(CdShowSection, VpShowSection, IvShowSection);
        private void SetDictationDirestionButtonsVisibility()
        {
            if (CurrentDictionaryType == DictionaryType.CommonDictionary)
                this.DictationDirectionButtonsIsVisible = true;
            else
                this.DictationDirectionButtonsIsVisible = false;
        }
        #endregion

        #region Start & stop buttons
        private void ShowStartButton()
        {
            this.StartButtonIsVisible = true;
            this.StopButtonIsVisible = false;
        }
        private void SwitchStartAndStopButtons()
        {
            this.StartButtonIsVisible = !this.StartButtonIsVisible;
            this.StopButtonIsVisible = !this.StopButtonIsVisible;
        }
        #endregion

        #region Check & next buttons
        private void ShowCheckButton()
        {
            this.CheckAnswerButtonIsVisible = true;
            this.NextButtonIsVisible = false;
        }
        private void SwitchCheckAndNextButtons()
        {
            this.CheckAnswerButtonIsVisible = !this.CheckAnswerButtonIsVisible;
            this.NextButtonIsVisible = !this.NextButtonIsVisible;
        }
        #endregion

        #region Dictation progress bar
        private void SetDictationProgressBar()
        {
            SetDictationProgressBarMaxValue();
            SetDictationProgressBarDefaultValue();
        }
        private void SetDictationProgressBarMaxValue()
        {
            if (CurrentDictionaryType == DictionaryType.IrregularVerbDictionary)
                this.DictationProgressBarMaxValue = this.DictationLengthSliderValue * 3;
            else
                this.DictationProgressBarMaxValue = this.DictationLengthSliderValue;
        }
        private void IncreaseDictationProgressBarValue() => this.DictationProgressBarValue++;
        private void SetDictationProgressBarDefaultValue() => this.DictationProgressBarValue = 0;
        #endregion

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
    }
}
