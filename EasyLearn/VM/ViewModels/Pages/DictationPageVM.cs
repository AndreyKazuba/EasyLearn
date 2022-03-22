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

namespace EasyLearn.VM.ViewModels.Pages
{
    public class DictationPageVM : ViewModel
    {
        #region Repositories
        private readonly IEasyLearnUserRepository userRepository;
        private readonly ICommonDictionaryRepository commonDictionaryRepository;
        private readonly IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository;
        #endregion

        #region Private fields
        private bool isDictationStarted;
        private int currentUserId;
        private CommonDictionary loadedCommonDictionary;
        private VerbPrepositionDictionnary loadedVerbPrepositionDictionary;
        private DictionaryComboBoxItem selectedDictionaryComboBoxItem;
        private CommonDictationManager? commonDictationManager;
        private VerbPrepositionDictationManager? verbPrepositionDictationManager;
        #endregion

#pragma warning disable CS8618
        public DictationPageVM(IEasyLearnUserRepository userRepository, ICommonDictionaryRepository commonDictionaryRepository, IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository)
        {
            this.userRepository = userRepository;
            this.commonDictionaryRepository = commonDictionaryRepository;
            this.verbPrepositionDictionaryRepository = verbPrepositionDictionaryRepository;
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
                    default:
                        return 120;
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
            }
        }
        public int DictationLengthSliderMaxValue { get; set; }
        public int DictationLengthSliderMinValue { get; set; }
        public int DictationLengthSliderCurrentValue { get; set; }
        public int DictationProgressBarMaxValue { get; set; }
        public int DictationProgressBarCurrentValue { get; set; }
        public string MainDisplayValue { get; set; }
        public string CommentDisplayValue { get; set; }
        public string AnswerValue { get; set; }
        public string UnitTypeDisplayValue { get; set; }
        public bool CorrectAnswerIconIsVisible { get; set; }
        public bool WrongAnswerIconIsVisible { get; set; }
        public bool CheckAnswerButtonIsVisible { get; set; }
        public bool NextButtonIsVisible { get; set; }
        public bool StartButtonIsVisible { get; set; }
        public bool StartButtonIsEnabled { get; set; } = true;
        public bool StopButtonIsVisible { get; set; }
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
            }
        }
        private void StopDictation()
        {
            SetDefaultPageState();
            this.commonDictationManager = null;
            this.verbPrepositionDictationManager = null;
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
            int countOfRelations = this.DictationLengthSliderCurrentValue;
            List<CommonRelation> commonRelations = ShuffleCommonRelation(this.loadedCommonDictionary.Relations).Take(countOfRelations).ToList();
            this.commonDictationManager = new CommonDictationManager(commonRelations);
            CommonRelation firstCommonRelation = commonDictationManager.Start();
            this.MainDisplayValue = firstCommonRelation.RussianUnit.Value.NormalizeRegister();
            this.CommentDisplayValue = firstCommonRelation.Comment.TryNormalizeRegister();
            this.UnitTypeDisplayValue = firstCommonRelation.RussianUnit.Type.ToString().NormalizeRegister();
            SwitchStartAndStopButtons();
            FocusAnswerTextBox();
            SetDictationProgressBar();
        }
        private void CheckAnswerForCommonDictionary()
        {
            if (!isDictationStarted || this.commonDictationManager is null)
                return;
            bool answerIsCorrect = this.commonDictationManager.IsAnswerCorrect(this.AnswerValue);
            if (answerIsCorrect)
            {
                ShowCorrectAnswerIcon();
                IncreaseDictationProgressBarCurrentValue();
                SwitchCheckAnswerAndNextButtons();
            }
            else
            {
                ShowWrongAnswerIcon();
            }
        }
        private void TryGoNextForCommonDictionary()
        {
            if (!this.isDictationStarted || this.commonDictationManager is null)
                return;
            if (this.commonDictationManager.GoNext())
            {
                this.MainDisplayValue = commonDictationManager.CurrentCommonRelation.RussianUnit.Value.NormalizeRegister();
                this.CommentDisplayValue = commonDictationManager.CurrentCommonRelation.Comment.TryNormalizeRegister();
                this.UnitTypeDisplayValue = commonDictationManager.CurrentCommonRelation.RussianUnit.Type.ToString().NormalizeRegister();
                SetDefaultAnswerValue();
                HideCorrectAndWrongAnswerIcons();
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
            List<VerbPreposition> verbPrepositions = ShuffleVerbPrepositions(this.loadedVerbPrepositionDictionary.VerbPrepositions).Take(countOfVerbPrepositions).ToList();
            this.verbPrepositionDictationManager = new VerbPrepositionDictationManager(verbPrepositions);
            VerbPreposition firstVerbPreposition = verbPrepositionDictationManager.Start();
            this.MainDisplayValue = firstVerbPreposition.Verb.Value.NormalizeRegister();
            this.CommentDisplayValue = firstVerbPreposition.Comment.TryNormalizeRegister();
            this.UnitTypeDisplayValue = firstVerbPreposition.Verb.Type.ToString().NormalizeRegister();
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
                ShowCorrectAnswerIcon();
                IncreaseDictationProgressBarCurrentValue();
                SwitchCheckAnswerAndNextButtons();
            }
            else
            {
                ShowWrongAnswerIcon();
            }
        }
        private void TryGoNextForVerbPrepositionDictionary()
        {
            if (!this.isDictationStarted || this.verbPrepositionDictationManager is null)
                return;
            if (this.verbPrepositionDictationManager.GoNext())
            {
                this.MainDisplayValue = verbPrepositionDictationManager.CurrentVerbPreposition.Verb.Value.NormalizeRegister();
                this.CommentDisplayValue = verbPrepositionDictationManager.CurrentVerbPreposition.Comment.TryNormalizeRegister();
                this.UnitTypeDisplayValue = verbPrepositionDictationManager.CurrentVerbPreposition.Verb.Type.ToString().NormalizeRegister();
                SetDefaultAnswerValue();
                HideCorrectAndWrongAnswerIcons();
                SwitchCheckAnswerAndNextButtons();
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
            DictationPage.EnterClick += this.OnEnterClick;
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
        #endregion

        #region Display actions
        private void SetDefaultPageState()
        {
            this.isDictationStarted = false;
            SetDefaultMainDisplayValue();
            SetDefaultCommentDisplayValue();
            SetDefaultAnswerValue();
            ShowStartButton();
            ShowCheckAnswerButton();
            ResetDictationProgressBarCurrentValue();
            HideCorrectAndWrongAnswerIcons();
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
        private void SetDefaultMainDisplayValue() => this.MainDisplayValue = "EasyLearn";
        private void SetDefaultCommentDisplayValue() => this.CommentDisplayValue = String.Empty;
        private void ShowWrongAnswerIcon()
        {
            this.WrongAnswerIconIsVisible = true;
            this.CorrectAnswerIconIsVisible = false;
        }
        private void ShowCorrectAnswerIcon()
        {
            this.CorrectAnswerIconIsVisible = true;
            this.WrongAnswerIconIsVisible = false;
        }
        private void HideCorrectAndWrongAnswerIcons()
        {
            this.CorrectAnswerIconIsVisible = false;
            this.WrongAnswerIconIsVisible = false;
        }
        private void SwitchCorrectAndWrongAnswerIcons()
        {
            this.CorrectAnswerIconIsVisible = !this.CorrectAnswerIconIsVisible;
            this.WrongAnswerIconIsVisible = !this.WrongAnswerIconIsVisible;
        }
        private void FocusAnswerTextBox() => App.GetService<DictationPage>().dictationTextBox.Focus();
        private void SetDictationProgressBar()
        {
            SetDictationProgressBarMaxValue();
            ResetDictationProgressBarCurrentValue();
        }
        private void SetDictationProgressBarMaxValue() => this.DictationProgressBarMaxValue = this.DictationLengthSliderCurrentValue;
        private void IncreaseDictationProgressBarCurrentValue() => this.DictationProgressBarCurrentValue++;
        private void ResetDictationProgressBarCurrentValue() => this.DictationProgressBarCurrentValue = 0;
        #endregion

        #region Helpers
        private IEnumerable<CommonRelation> ShuffleCommonRelation(IEnumerable<CommonRelation> relations)
        {
            Random random = new Random();
            return relations.OrderBy(relation => random.Next());
        }
        private IEnumerable<VerbPreposition> ShuffleVerbPrepositions(IEnumerable<VerbPreposition> verbPrepositions)
        {
            Random random = new Random();
            return verbPrepositions.OrderBy(verbPrepositions => random.Next());
        }
        #endregion
    }
}
