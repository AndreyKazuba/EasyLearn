using EasyLearn.Data.Models;
using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.ExpandedElements;
using EasyLearn.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EasyLearn.Data.Enums;
using EasyLearn.Data.Helpers;
using EasyLearn.UI.Pages;
using EasyLearn.Data.Constants;
using EasyLearn.Infrastructure.Exceptions;

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
        private DictionaryComboBoxItem selectedDictionaryComboBoxItem;
        private int wrongAnswers;
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
            get
            {
                DictionaryType selectedDictionaryType = SelectedDictionaryComboBoxItem.DictionaryType;
                switch (selectedDictionaryType)
                {
                    case DictionaryType.CommonDictionary:
                        return cdLoadedDictionary.Relations.Count;
                    case DictionaryType.VerbPrepositionDictionary:
                        return vpLoadedDictionary.VerbPrepositions.Count;
                    case DictionaryType.IrregularVerbDictionary:
                        return ModelConstants.IrregularVerbsCount;
                    default:
                        throw new Exception(ExceptionMessagesHelper.NoSuchDictationType);
                }
            }
        }
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
        private void StartDictation()
        {
            DictionaryType selectedDictionaryType = selectedDictionaryComboBoxItem.DictionaryType;
            switch (selectedDictionaryType)
            {
                case DictionaryType.CommonDictionary:
                    CdStart();
                    break;
                case DictionaryType.VerbPrepositionDictionary:
                    VpStart();
                    break;
                case DictionaryType.IrregularVerbDictionary:
                    IvStart();
                    break;
            }
        }
        private void CheckAnswer()
        {
            DictionaryType selectedDictionaryType = selectedDictionaryComboBoxItem.DictionaryType;
            switch (selectedDictionaryType)
            {
                case DictionaryType.CommonDictionary:
                    CdCheck();
                    break;
                case DictionaryType.VerbPrepositionDictionary:
                    VpCheck();
                    break;
                case DictionaryType.IrregularVerbDictionary:
                    IvCheck();
                    break;
            }
        }
        private void TryGoNext()
        {
            DictionaryType selectedDictionaryType = selectedDictionaryComboBoxItem.DictionaryType;
            switch (selectedDictionaryType)
            {
                case DictionaryType.CommonDictionary:
                    CdTryGoNext();
                    break;
                case DictionaryType.VerbPrepositionDictionary:
                    VpTryGoNext();
                    break;
                case DictionaryType.IrregularVerbDictionary:
                    IvTryGoNext();
                    break;
            }
        }
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
            DictionaryType selectedDictionaryType = this.SelectedDictionaryComboBoxItem.DictionaryType;
            switch (selectedDictionaryType)
            {
                case DictionaryType.CommonDictionary:
                    this.cdLoadedDictionary = commonDictionaryRepository.GetCommonDictionary(selectedDictionaryId);
                    break;
                case DictionaryType.VerbPrepositionDictionary:
                    this.vpLoadedDictionary = verbPrepositionDictionaryRepository.GetVerbPrepositionDictionary(selectedDictionaryId);
                    break;
            }
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
            CdSetPromtValue(commonDictationManager.CurrentEnglishValue);
        }
        private void OnCdPromtMouseLeave()
        {
            if (commonDictationManager is null)
                return;
            CdSetMysteriousPromtValue(commonDictationManager.CurrentEnglishValue);
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
            switch (currentIrregularVerbForm)
            {
                case IrregularVerbForm.FirstForm:
                    IvSetPromtValue(ivDictationManager.CurrentV1Value);
                    break;
                case IrregularVerbForm.SecondForm:
                    IvSetPromtValue(ivDictationManager.CurrentV2Value);
                    break;
                case IrregularVerbForm.ThirdForm:
                    IvSetPromtValue(ivDictationManager.CurrentV3Value);
                    break;
            }

        }
        private void OnIvPromtMouseLeave()
        {
            if (ivDictationManager is null)
                return;
            switch (currentIrregularVerbForm)
            {
                case IrregularVerbForm.FirstForm:
                    IvSetMysteriousPromtValue(ivDictationManager.CurrentV1Value);
                    break;
                case IrregularVerbForm.SecondForm:
                    IvSetMysteriousPromtValue(ivDictationManager.CurrentV2Value);
                    break;
                case IrregularVerbForm.ThirdForm:
                    IvSetMysteriousPromtValue(ivDictationManager.CurrentV3Value);
                    break;
            }
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
        private void SetCurrentDictationSection()
        {
            DictionaryType currentDictionaryType = SelectedDictionaryComboBoxItem.DictionaryType;
            switch (currentDictionaryType)
            {
                case DictionaryType.CommonDictionary:
                    CdShowSection();
                    break;
                case DictionaryType.VerbPrepositionDictionary:
                    VpShowSection();
                    break;
                case DictionaryType.IrregularVerbDictionary:
                    IvShowSection();
                    break;
            }
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
            DictionaryType currentDictionaryType = SelectedDictionaryComboBoxItem.DictionaryType;
            if (currentDictionaryType == DictionaryType.IrregularVerbDictionary)
                this.DictationProgressBarMaxValue = this.DictationLengthSliderValue * 3;
            else
                this.DictationProgressBarMaxValue = this.DictationLengthSliderValue;
        }
        private void IncreaseDictationProgressBarValue() => this.DictationProgressBarValue++;
        private void SetDictationProgressBarDefaultValue() => this.DictationProgressBarValue = 0;
        #endregion
    }
}
