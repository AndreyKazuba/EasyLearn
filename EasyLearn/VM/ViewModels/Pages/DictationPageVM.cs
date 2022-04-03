using EasyLearn.Data.Models;
using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.ExpandedElements;
using EasyLearn.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EasyLearn.Data.Enums;
using EasyLearn.Infrastructure.DictationManagers;
using EasyLearn.Data.Helpers;
using EasyLearn.UI.Pages;
using EasyLearn.Infrastructure.Constants;
using EasyLearn.Infrastructure.Enums;
using System.Windows.Media;
using EasyLearn.Data;
using EasyLearn.UI.CustomControls;

namespace EasyLearn.VM.ViewModels.Pages
{
    public class DictationPageVM : ViewModel
    {
        #region Repositories
        private readonly IEasyLearnUserRepository userRepository;
        private readonly ICommonDictionaryRepository commonDictionaryRepository;
        private readonly IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository;
        private readonly IIrregularVerbRepository irregularVerbRepository;
        #endregion

        #region Private fields
        private bool isDictationStarted;
        private int currentUserId;
        private IrregularVerbForm currentIrregularVerbForm;
        private CommonDictionary loadedCommonDictionary;
        private VerbPrepositionDictionnary loadedVerbPrepositionDictionary;
        private CoommonDictationManager? commonDictationManager;
        private VerbPrepositionDictationManager? verbPrepositionDictationManager;
        private IrregularVerbDictationManager? irregularVerbDictationManager;
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
                        return loadedCommonDictionary.Relations.Count;
                    case DictionaryType.VerbPrepositionDictionary:
                        return loadedVerbPrepositionDictionary.VerbPrepositions.Count;
                    case DictionaryType.IrregularVerbDictionary:
                        return ModelConstants.IrregularVerbsCount;
                    default:
                        throw new Exception("Нет такого типа");
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
        public Brush CommonDictationUnitTypeForegraundColor { get; set; }
        public ObservableCollection<AvailableRelationView> AvailableRelationViews { get; set; }
        public int DictationLengthSliderMaxValue { get; set; }
        public int DictationLengthSliderMinValue { get; set; }
        public int DictationLengthSliderCurrentValue { get; set; }
        public int DictationProgressBarMaxValue { get; set; }
        public int DictationProgressBarCurrentValue { get; set; }
        public string MainCommonDictationDisplayValue { get; set; }
        public string UnitTypeCommonDictationDisplayValue { get; set; }
        public string CommentCommonDictationDisplayValue { get; set; }
        public string MainVerbPrepositionDictationDisplayValue { get; set; }
        public string SecondVerbPrepositionDictationDisplayValue { get; set; }
        public string TranslationVerbPrepositionDictationDisplayValue { get; set; }
        public string? CommentVerbPrepositionDictationDisplayValue { get; set; }
        public string MainIrregularVerbDictationDisplayValue { get; set; }
        public string CommentIrregularVerbDictationDisplayValue { get; set; }
        public string IrregularVerbDictationFirstFormFixedAnswerValue { get; set; }
        public string IrregularVerbDictationSecondFormFixedAnswerValue { get; set; }
        public string IrregularVerbDictationThirdFormFixedAnswerValue { get; set; }
        public string AnswerValue { get; set; }
        public string PromtCommonDictationDisplayValue { get; set; }
        public bool CommonDictationCorrectAnswerIconIsVisible { get; set; }
        public bool CommonDictationWrongAnswerIconIsVisible { get; set; }
        public bool CommonDictationTypeChipIsVisible { get; set; }
        public bool VerbPrepositionDictationCorrectAnswerIconIsVisible { get; set; }
        public bool VerbPrepositionDictationWrongAnswerIconIsVisible { get; set; }
        public bool CheckAnswerButtonIsVisible { get; set; }
        public bool NextButtonIsVisible { get; set; }
        public bool StartButtonIsVisible { get; set; }
        public bool StartButtonIsEnabled { get; set; } = true;
        public bool StopButtonIsVisible { get; set; }
        public bool IsCommonDictationSectionVisible { get; set; }
        public bool IsVerbPrepositionDictationSectionVisible { get; set; }
        public bool IsIrregularVerbDictationSectionVisible { get; set; }
        public bool IrregularVerbDictationFirstFormGrayIconIsVisible { get; set; }
        public bool IrregularVerbDictationFirstFormCorrectAnswerIconIsVisible { get; set; }
        public bool IrregularVerbDictationFirstFormWrongAnswerIconIsVisible { get; set; }
        public bool IrregularVerbDictationSecondFormGrayIconIsVisible { get; set; }
        public bool IrregularVerbDictationSecondFormCorrectAnswerIconIsVisible { get; set; }
        public bool IrregularVerbDictationSecondFormWrongAnswerIconIsVisible { get; set; }
        public bool IrregularVerbDictationThirdFormGrayIconIsVisible { get; set; }
        public bool IrregularVerbDictationThirdFormCorrectAnswerIconIsVisible { get; set; }
        public bool IrregularVerbDictationThirdFormWrongAnswerIconIsVisible { get; set; }
        public bool AvailableRelationsSectionIsVisible { get; set; }
        public bool PromtCommonDictationDisplayIsVisible { get; set; }
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
            this.DictationLengthSliderCurrentValue = this.DictationLengthSliderMaxValue;
        }
        private void StartDictation()
        {
            DictionaryType selectedDictionaryType = selectedDictionaryComboBoxItem.DictionaryType;
            switch (selectedDictionaryType)
            {
                case DictionaryType.CommonDictionary:
                    StartCommonDictation();
                    break;
                case DictionaryType.VerbPrepositionDictionary:
                    StartVerbPrepositionDictionary();
                    break;
                case DictionaryType.IrregularVerbDictionary:
                    StartIrregularVerbDictation();
                    break;
            }
        }
        private void CheckAnswer()
        {
            DictionaryType selectedDictionaryType = selectedDictionaryComboBoxItem.DictionaryType;
            switch (selectedDictionaryType)
            {
                case DictionaryType.CommonDictionary:
                    CheckAnswerForCommonDictionary();
                    break;
                case DictionaryType.VerbPrepositionDictionary:
                    CheckAnswerForVerbPrepositionDictionary();
                    break;
                case DictionaryType.IrregularVerbDictionary:
                    CheckAnswerForIrregularVerbDictionary();
                    break;
            }
        }
        private void TryGoNext()
        {
            DictionaryType selectedDictionaryType = selectedDictionaryComboBoxItem.DictionaryType;
            switch (selectedDictionaryType)
            {
                case DictionaryType.CommonDictionary:
                    TryGoNextForCommonDictionary();
                    break;
                case DictionaryType.VerbPrepositionDictionary:
                    TryGoNextForVerbPrepositionDictionary();
                    break;
                case DictionaryType.IrregularVerbDictionary:
                    TryGoNextForIrregularVerbDictionary();
                    break;
            }
        }
        private void StopDictation()
        {
            SetDefaultPageState();
            this.commonDictationManager = null;
            this.verbPrepositionDictationManager = null;
            this.irregularVerbDictationManager = null;
        }
        public void UpdatePageForNewUser()
        {
            SetCurrentUserId();
            UpdateDictionaryComboBoxItems();
            LoadSelectedDictionary();
            UpdateDictationLengthSlider();
        }
        #endregion

        #region Dictation process methods
        private void StartCommonDictation()
        {
            SetDefaultPageState();
            this.isDictationStarted = true;
            this.CommonDictationTypeChipIsVisible = true;
            int countOfRelations = this.DictationLengthSliderCurrentValue;
            List<CommonRelation> commonRelations = this.loadedCommonDictionary.Relations;
            this.commonDictationManager = new CoommonDictationManager(commonRelations, countOfRelations);
            CommonRelation firstCommonRelation = commonDictationManager.Start();
            this.MainCommonDictationDisplayValue = firstCommonRelation.RussianUnit.Value.NormalizeRegister();
            this.CommentCommonDictationDisplayValue = firstCommonRelation.Comment.TryNormalizeRegister();
            this.UnitTypeCommonDictationDisplayValue = firstCommonRelation.RussianUnit.Type.GetRussianValue();
            this.CommonDictationUnitTypeForegraundColor = firstCommonRelation.RussianUnit.Type.GetColor();
            SwitchStartAndStopButtons();
            FocusAnswerTextBox();
            SetDictationProgressBar();
        }
        private void CheckAnswerForCommonDictionary()
        {
            if (!isDictationStarted || this.commonDictationManager is null)
                return;
            bool answerIsCorrect = this.commonDictationManager.AvailableRelations.Any(relation => StringHelper.Equals(relation.EnglishUnit.Value, this.AnswerValue));
            if (answerIsCorrect)
            {
                if (this.commonDictationManager.CurrentRelationHasSynonyms)
                    ShowAvailableRelationsSection(this.commonDictationManager.AvailableRelations, this.AnswerValue);
                ShowCommonDictationCorrectAnswerIcon();
                IncreaseDictationProgressBarCurrentValue();
                SwitchCheckAnswerAndNextButtons();
                SetDefaultAnswerValue();
                wrongAnswers = 0;
            }
            else
            {
                ShowCommonDictationWrongAnswerIcon();
                if (wrongAnswers++ > 2)
                    ShowCommonDictationPromt();
            }
        }
        private void TryGoNextForCommonDictionary()
        {
            if (!this.isDictationStarted || this.commonDictationManager is null)
                return;
            if (this.commonDictationManager.GoNext())
            {
                this.MainCommonDictationDisplayValue = commonDictationManager.CurrentRelation.RussianUnit.Value.NormalizeRegister();
                this.CommentCommonDictationDisplayValue = commonDictationManager.CurrentRelation.Comment.TryNormalizeRegister();
                this.UnitTypeCommonDictationDisplayValue = commonDictationManager.CurrentRelation.RussianUnit.Type.GetRussianValue();
                this.CommonDictationUnitTypeForegraundColor = commonDictationManager.CurrentRelation.RussianUnit.Type.GetColor();
                SetDefaultAnswerValue();
                HideAvailableRelationsSection();
                HideCommonDictationCorrectAndWrongAnswerIcons();
                HideCommonDictationPromt();
                SwitchCheckAnswerAndNextButtons();
            }
            else
            {
                StopDictation();
            }
        }
        private void StartVerbPrepositionDictionary()
        {
            SetDefaultPageState();
            this.isDictationStarted = true;
            int countOfVerbPrepositions = this.DictationLengthSliderCurrentValue;
            List<VerbPreposition> verbPrepositions = DictationManagerHelper.Shuffle(this.loadedVerbPrepositionDictionary.VerbPrepositions).Take(countOfVerbPrepositions).ToList();
            this.verbPrepositionDictationManager = new VerbPrepositionDictationManager(verbPrepositions);
            VerbPreposition firstVerbPreposition = verbPrepositionDictationManager.Start();
            this.MainVerbPrepositionDictationDisplayValue = firstVerbPreposition.Verb.Value.NormalizeRegister();
            this.SecondVerbPrepositionDictationDisplayValue = "...";
            this.TranslationVerbPrepositionDictationDisplayValue = firstVerbPreposition.Translation.NormalizeRegister();
            this.CommentVerbPrepositionDictationDisplayValue = firstVerbPreposition.Comment?.ToLower();
            SwitchStartAndStopButtons();
            FocusAnswerTextBox();
            SetDictationProgressBar();
        }
        private void CheckAnswerForVerbPrepositionDictionary()
        {
            if (!isDictationStarted || this.verbPrepositionDictationManager is null)
                return;
            bool answerIsCorrect = this.verbPrepositionDictationManager.IsAnswerCorrect(this.AnswerValue);
            if (answerIsCorrect)
            {
                ShowVerbPrepositionDictationCorrectAnswerIcon();
                IncreaseDictationProgressBarCurrentValue();
                SwitchCheckAnswerAndNextButtons();
                SetDefaultAnswerValue();
            }
            else
            {
                ShowVerbPrepositionDictationWrongAnswerIcon();
            }
        }
        private void TryGoNextForVerbPrepositionDictionary()
        {
            if (!this.isDictationStarted || this.verbPrepositionDictationManager is null)
                return;
            if (this.verbPrepositionDictationManager.GoNext())
            {
                this.MainVerbPrepositionDictationDisplayValue = verbPrepositionDictationManager.CurrentVerbPreposition.Verb.Value.NormalizeRegister();
                this.CommentVerbPrepositionDictationDisplayValue = verbPrepositionDictationManager.CurrentVerbPreposition.Comment?.ToLower();
                this.TranslationVerbPrepositionDictationDisplayValue = verbPrepositionDictationManager.CurrentVerbPreposition.Translation.NormalizeRegister();
                SetDefaultAnswerValue();
                HideVerbPrepositionDictationCorrectAndWrongAnswerIcons();
                SwitchCheckAnswerAndNextButtons();
            }
            else
            {
                StopDictation();
            }
        }
        private void StartIrregularVerbDictation()
        {
            SetDefaultPageState();
            this.isDictationStarted = true;
            int countOfIrregularVerbs = this.DictationLengthSliderCurrentValue;
            List<IrregularVerb> irregularVerbs = DictationManagerHelper.Shuffle(this.irregularVerbRepository.GetAllIrregularVerbs()).Take(countOfIrregularVerbs).ToList();
            this.irregularVerbDictationManager = new IrregularVerbDictationManager(irregularVerbs);
            IrregularVerb firstIrregularVerb = irregularVerbDictationManager.Start();
            this.MainIrregularVerbDictationDisplayValue = firstIrregularVerb.RussianUnit.Value.NormalizeRegister();
            this.CommentIrregularVerbDictationDisplayValue = firstIrregularVerb.Comment.TryNormalizeRegister();
            SwitchStartAndStopButtons();
            FocusAnswerTextBox();
            SetDictationProgressBar();
        }
        private void CheckAnswerForIrregularVerbDictionary()
        {
            if (!isDictationStarted || this.irregularVerbDictationManager is null)
                return;

            switch (currentIrregularVerbForm)
            {
                case IrregularVerbForm.FirstForm:
                    bool firstFormIsCorrect = this.irregularVerbDictationManager.IsFirstFormAnswerCorrect(this.AnswerValue);
                    if (firstFormIsCorrect)
                    {
                        ShowFirstFormCorrectAnswerIcon();
                        IncreaseDictationProgressBarCurrentValue();
                        this.IrregularVerbDictationFirstFormFixedAnswerValue = StringHelper.NormalizeRegister(this.AnswerValue);
                        this.currentIrregularVerbForm = IrregularVerbForm.SecondForm;
                        SetDefaultAnswerValue();
                    }
                    else
                    {
                        ShowFirstFormWrongAnswerIcon();
                    }
                    break;
                case IrregularVerbForm.SecondForm:
                    bool secondFormIsCorrect = this.irregularVerbDictationManager.IsSecondFormAnswerCorrect(this.AnswerValue);
                    if (secondFormIsCorrect)
                    {
                        ShowSecondFormCorrectAnswerIcon();
                        IncreaseDictationProgressBarCurrentValue();
                        this.IrregularVerbDictationSecondFormFixedAnswerValue = StringHelper.NormalizeRegister(this.AnswerValue);
                        this.currentIrregularVerbForm = IrregularVerbForm.ThirdForm;
                        SetDefaultAnswerValue();
                    }
                    else
                    {
                        ShowSecondFormWrongAnswerIcon();
                    }
                    break;
                case IrregularVerbForm.ThirdForm:
                    bool thirdFormIsCorrect = this.irregularVerbDictationManager.IsThirdFormAnswerCorrect(this.AnswerValue);
                    if (thirdFormIsCorrect)
                    {
                        ShowThirdFormCorrectAnswerIcon();
                        IncreaseDictationProgressBarCurrentValue();
                        SwitchCheckAnswerAndNextButtons();
                        this.IrregularVerbDictationThirdFormFixedAnswerValue = StringHelper.NormalizeRegister(this.AnswerValue);
                        SetDefaultAnswerValue();
                        this.currentIrregularVerbForm = IrregularVerbForm.FirstForm;
                    }
                    else
                    {
                        ShowThirdFormWrongAnswerIcon();
                    }
                    break;
            }
        }
        private void TryGoNextForIrregularVerbDictionary()
        {
            if (!this.isDictationStarted || this.irregularVerbDictationManager is null)
                return;
            if (this.irregularVerbDictationManager.GoNext())
            {
                this.MainIrregularVerbDictationDisplayValue = irregularVerbDictationManager.CurrentIrregularVerb.RussianUnit.Value.NormalizeRegister();
                this.CommentIrregularVerbDictationDisplayValue = irregularVerbDictationManager.CurrentIrregularVerb.Comment.TryNormalizeRegister();
                SetDefaultAnswerValue();
                ShowIrregularVerbSectionGrayIcons();
                SwitchCheckAnswerAndNextButtons();
                SetDefaultIrrefularVerbDictationFixedValues();
            }
            else
            {
                StopDictation();
            }
        }
        #endregion

        #region Other private methods
        private void SetCurrentUserId()
        {
            int? currentUserId = userRepository.TryGetCurrentUser()?.Id;
            if (!currentUserId.HasValue)
                throw new Exception("Не удалось получить из базы текущего пользователя");
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
                    this.loadedCommonDictionary = commonDictionaryRepository.GetCommonDictionary(selectedDictionaryId);
                    break;
                case DictionaryType.VerbPrepositionDictionary:
                    this.loadedVerbPrepositionDictionary = verbPrepositionDictionaryRepository.GetVerbPrepositionDictionary(selectedDictionaryId);
                    break;
            }
        }
        #endregion

        #region Event handling
        protected override void InitEvents()
        {
            DictationPage.EnterClick += OnEnterClick;
            DictationPage.PromtMouseEnter += OnCommonDictationPromtMouseEnter;
            DictationPage.PromtMouseLeave += OnCommonDictationPromtMouseLeave;
        }
        private void OnEnterClick()
        {
            if (!this.isDictationStarted)
                return;
            if (this.CheckAnswerButtonIsVisible)
                CheckAnswer();
            else
                TryGoNext();
        }
        private void OnCommonDictationPromtMouseEnter() => SetCommonDictationPromtValue();
        private void OnCommonDictationPromtMouseLeave() => SetMysteriousCommonDictationPromt();
        #endregion

        #region Display actions
        private void SetDefaultPageState()
        {
            this.isDictationStarted = false;
            this.currentIrregularVerbForm = IrregularVerbForm.FirstForm;
            this.CommonDictationTypeChipIsVisible = false;
            this.AvailableRelationsSectionIsVisible = false;
            this.wrongAnswers = 0;
            SetDefaultMainDisplayValue();
            SetDefaultCommentDisplayValue();
            SetDefaultAnswerValue();
            SetDefaultVerbPrepositionTranslationDisplayValue();
            ShowStartButton();
            ShowCheckAnswerButton();
            ResetDictationProgressBarCurrentValue();
            HideAllCorrectAndWrongAnswerIcons();
            SetDefaultIrrefularVerbDictationFixedValues();
        }
        private void ShowCheckAnswerButton()
        {
            this.CheckAnswerButtonIsVisible = true;
            this.NextButtonIsVisible = false;
        }
        private void ShowNextButton()
        {
            this.CheckAnswerButtonIsVisible = false;
            this.NextButtonIsVisible = true;
        }
        private void SwitchCheckAnswerAndNextButtons()
        {
            this.CheckAnswerButtonIsVisible = !this.CheckAnswerButtonIsVisible;
            this.NextButtonIsVisible = !this.NextButtonIsVisible;
        }
        private void ShowStartButton()
        {
            this.StartButtonIsVisible = true;
            this.StopButtonIsVisible = false;
        }
        private void ShowStopButton()
        {
            this.StopButtonIsVisible = true;
            this.StartButtonIsVisible = false;
        }
        private void SwitchStartAndStopButtons()
        {
            this.StartButtonIsVisible = !this.StartButtonIsVisible;
            this.StopButtonIsVisible = !this.StopButtonIsVisible;
        }
        private void SetDefaultAnswerValue() => this.AnswerValue = String.Empty;
        private void SetDefaultMainDisplayValue()
        {
            string easyLearn = "EasyLearn";
            this.MainCommonDictationDisplayValue = easyLearn;
            this.MainVerbPrepositionDictationDisplayValue = easyLearn;
            this.MainIrregularVerbDictationDisplayValue = easyLearn;
        }
        private void SetDefaultCommentDisplayValue()
        {
            this.CommentCommonDictationDisplayValue = String.Empty;
            this.CommentVerbPrepositionDictationDisplayValue = String.Empty;
            this.CommentIrregularVerbDictationDisplayValue = String.Empty;
        }
        private void ShowCommonDictationWrongAnswerIcon()
        {
            this.CommonDictationWrongAnswerIconIsVisible = true;
            this.CommonDictationCorrectAnswerIconIsVisible = false;
        }
        private void ShowCommonDictationCorrectAnswerIcon()
        {
            this.CommonDictationCorrectAnswerIconIsVisible = true;
            this.CommonDictationWrongAnswerIconIsVisible = false;
        }
        private void HideCommonDictationCorrectAndWrongAnswerIcons()
        {
            this.CommonDictationCorrectAnswerIconIsVisible = false;
            this.CommonDictationWrongAnswerIconIsVisible = false;
        }
        private void ShowVerbPrepositionDictationWrongAnswerIcon()
        {
            this.VerbPrepositionDictationWrongAnswerIconIsVisible = true;
            this.VerbPrepositionDictationCorrectAnswerIconIsVisible = false;
        }
        private void ShowVerbPrepositionDictationCorrectAnswerIcon()
        {
            this.VerbPrepositionDictationCorrectAnswerIconIsVisible = true;
            this.VerbPrepositionDictationWrongAnswerIconIsVisible = false;
        }
        private void HideVerbPrepositionDictationCorrectAndWrongAnswerIcons()
        {
            this.VerbPrepositionDictationCorrectAnswerIconIsVisible = false;
            this.VerbPrepositionDictationWrongAnswerIconIsVisible = false;
        }
        private void HideAllCorrectAndWrongAnswerIcons()
        {
            HideCommonDictationCorrectAndWrongAnswerIcons();
            HideVerbPrepositionDictationCorrectAndWrongAnswerIcons();
            ShowIrregularVerbSectionGrayIcons();
        }
        private void SwitchCommonDictationCorrectAndWrongAnswerIcons()
        {
            this.CommonDictationCorrectAnswerIconIsVisible = !this.CommonDictationCorrectAnswerIconIsVisible;
            this.CommonDictationWrongAnswerIconIsVisible = !this.CommonDictationWrongAnswerIconIsVisible;
        }
        private void FocusAnswerTextBox() => App.GetService<DictationPage>().dictationTextBox.Focus();
        private void SetDictationProgressBar()
        {
            SetDictationProgressBarMaxValue();
            ResetDictationProgressBarCurrentValue();
        }
        private void SetDictationProgressBarMaxValue()
        {
            DictionaryType currentDictionaryType = SelectedDictionaryComboBoxItem.DictionaryType;
            if (currentDictionaryType == DictionaryType.IrregularVerbDictionary)
                this.DictationProgressBarMaxValue = this.DictationLengthSliderCurrentValue * 3;
            else
                this.DictationProgressBarMaxValue = this.DictationLengthSliderCurrentValue;
        }
        private void IncreaseDictationProgressBarCurrentValue() => this.DictationProgressBarCurrentValue++;
        private void ResetDictationProgressBarCurrentValue() => this.DictationProgressBarCurrentValue = 0;
        private void SetCurrentDictationSection()
        {
            DictionaryType currentDictionaryType = SelectedDictionaryComboBoxItem.DictionaryType;
            switch (currentDictionaryType)
            {
                case DictionaryType.CommonDictionary:
                    ShowCommonSection();
                    break;
                case DictionaryType.VerbPrepositionDictionary:
                    ShowVerbPrepositionSection();
                    break;
                case DictionaryType.IrregularVerbDictionary:
                    ShowIrregularVerbSection();
                    break;
            }
        }
        private void ShowCommonSection()
        {
            this.IsCommonDictationSectionVisible = true;
            this.IsVerbPrepositionDictationSectionVisible = false;
            this.IsIrregularVerbDictationSectionVisible = false;
        }
        private void ShowVerbPrepositionSection()
        {
            this.IsCommonDictationSectionVisible = false;
            this.IsVerbPrepositionDictationSectionVisible = true;
            this.IsIrregularVerbDictationSectionVisible = false;
        }
        private void ShowIrregularVerbSection()
        {
            this.IsCommonDictationSectionVisible = false;
            this.IsVerbPrepositionDictationSectionVisible = false;
            this.IsIrregularVerbDictationSectionVisible = true;
        }
        private void SetDefaultIrrefularVerbDictationFixedValues()
        {
            this.IrregularVerbDictationFirstFormFixedAnswerValue = "????";
            this.IrregularVerbDictationSecondFormFixedAnswerValue = "????";
            this.IrregularVerbDictationThirdFormFixedAnswerValue = "????";
        }
        private void SetDefaultVerbPrepositionTranslationDisplayValue() => this.TranslationVerbPrepositionDictationDisplayValue = String.Empty;
        private void ShowFirstFormCorrectAnswerIcon()
        {
            this.IrregularVerbDictationFirstFormGrayIconIsVisible = false;
            this.IrregularVerbDictationFirstFormCorrectAnswerIconIsVisible = true;
            this.IrregularVerbDictationFirstFormWrongAnswerIconIsVisible = false;
        }
        private void ShowFirstFormWrongAnswerIcon()
        {
            this.IrregularVerbDictationFirstFormGrayIconIsVisible = false;
            this.IrregularVerbDictationFirstFormCorrectAnswerIconIsVisible = false;
            this.IrregularVerbDictationFirstFormWrongAnswerIconIsVisible = true;
        }
        private void ShowSecondFormCorrectAnswerIcon()
        {
            this.IrregularVerbDictationSecondFormGrayIconIsVisible = false;
            this.IrregularVerbDictationSecondFormCorrectAnswerIconIsVisible = true;
            this.IrregularVerbDictationSecondFormWrongAnswerIconIsVisible = false;
        }
        private void ShowSecondFormWrongAnswerIcon()
        {
            this.IrregularVerbDictationSecondFormGrayIconIsVisible = false;
            this.IrregularVerbDictationSecondFormCorrectAnswerIconIsVisible = false;
            this.IrregularVerbDictationSecondFormWrongAnswerIconIsVisible = true;
        }
        private void ShowThirdFormCorrectAnswerIcon()
        {
            this.IrregularVerbDictationThirdFormGrayIconIsVisible = false;
            this.IrregularVerbDictationThirdFormCorrectAnswerIconIsVisible = true;
            this.IrregularVerbDictationThirdFormWrongAnswerIconIsVisible = false;
        }
        private void ShowThirdFormWrongAnswerIcon()
        {
            this.IrregularVerbDictationThirdFormGrayIconIsVisible = false;
            this.IrregularVerbDictationThirdFormCorrectAnswerIconIsVisible = false;
            this.IrregularVerbDictationThirdFormWrongAnswerIconIsVisible = true;
        }
        private void ShowIrregularVerbSectionGrayIcons()
        {
            HideAllIrregularVerbSectionAnswerIcons();
            this.IrregularVerbDictationFirstFormGrayIconIsVisible = true;
            this.IrregularVerbDictationSecondFormGrayIconIsVisible = true;
            this.IrregularVerbDictationThirdFormGrayIconIsVisible = true;
        }
        private void HideAllIrregularVerbSectionAnswerIcons()
        {
            this.IrregularVerbDictationFirstFormGrayIconIsVisible = false;
            this.IrregularVerbDictationSecondFormGrayIconIsVisible = false;
            this.IrregularVerbDictationThirdFormGrayIconIsVisible = false;
            this.IrregularVerbDictationFirstFormCorrectAnswerIconIsVisible = false;
            this.IrregularVerbDictationSecondFormCorrectAnswerIconIsVisible = false;
            this.IrregularVerbDictationThirdFormCorrectAnswerIconIsVisible = false;
            this.IrregularVerbDictationFirstFormWrongAnswerIconIsVisible = false;
            this.IrregularVerbDictationSecondFormWrongAnswerIconIsVisible = false;
            this.IrregularVerbDictationThirdFormWrongAnswerIconIsVisible = false;
        }
        private void ShowAvailableRelationsSection(IEnumerable<CommonRelation> commonRelations, string asnwerValue)
        {
            this.AvailableRelationsSectionIsVisible = true;
            IEnumerable<AvailableRelationView> availableRelationViews = commonRelations
                .Where(relation => !StringHelper.Equals(relation.EnglishUnit.Value, asnwerValue))
                .Select(relation => AvailableRelationView.Create(relation));
            this.AvailableRelationViews = new ObservableCollection<AvailableRelationView>(availableRelationViews);
        }
        private void HideAvailableRelationsSection()
        {
            this.AvailableRelationsSectionIsVisible = false;
            if (this.AvailableRelationViews is not null)
                this.AvailableRelationViews.Clear();
        }
        private void ShowCommonDictationPromt()
        {
            SetMysteriousCommonDictationPromt();
            this.PromtCommonDictationDisplayIsVisible = true;
        }
        private void HideCommonDictationPromt() => this.PromtCommonDictationDisplayIsVisible = false;
        private void SetMysteriousCommonDictationPromt()
        {
            if (this.commonDictationManager is null)
                return;
            int symbolsCount = this.commonDictationManager.CurrentRelation.EnglishUnit.Value.Length;
            string mysteriousString = new string('?', symbolsCount);
            this.PromtCommonDictationDisplayValue = $"({mysteriousString})";
        }
        private void SetCommonDictationPromtValue()
        {
            if (this.commonDictationManager is null)
                return;
            this.PromtCommonDictationDisplayValue = $"({this.commonDictationManager.CurrentRelation.EnglishUnit.Value})";
        }
        #endregion
    }
}
