using EasyLearn.Data.Models;
using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.ExpandedElements;
using EasyLearn.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLearn.Data.Enums;
using EasyLearn.Infrastructure.Dictation;
using MaterialDesignThemes.Wpf;
using EasyLearn.Data.Helpers;
using EasyLearn.UI.Pages;

namespace EasyLearn.VM.ViewModels.Pages
{
    public class DictationPageVM : ViewModel
    {
        #region Private fields
        private bool isDictationStarted;
        private bool isFirstAnswer;
        private int currentUserId;

        private IEasyLearnUserRepository userRepository;
        private ICommonDictionaryRepository commonDictionaryRepository;
        private IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository;

        private CommonDictionary selectedCommonDictionary;
        private VerbPrepositionDictionnary selectedVerbPrepositionDictionnary;

        private CommonDictationManager commonDictationManager;
        #endregion

        #region Private props
        private int ItemsInSelectedDictionary
        {
            get
            {
                DictionaryType selectedDictionaryType = SelectedDictionaryComboBoxItem.DictionaryType;
                switch (selectedDictionaryType)
                {
                    case DictionaryType.CommonDictionary:
                        return selectedCommonDictionary.Relations.Count;
                    case DictionaryType.VerbPrepositionDictionary:
                        return selectedVerbPrepositionDictionnary.VerbPrepositions.Count;
                    default:
                        return 120;
                }
            }
        }
        #endregion

        #region Props for binding
        public ObservableCollection<DictionaryComboBoxItem> DictionaryComboBoxItems { get; set; }
        private DictionaryComboBoxItem selectedDictionaryComboBoxItem;
        public DictionaryComboBoxItem SelectedDictionaryComboBoxItem
        {
            get { return selectedDictionaryComboBoxItem; }
            set
            {
                selectedDictionaryComboBoxItem = value;
                LoadSelectedDictionary();
                RefreshSlider();
            }
        }
        public int SliderMaxValue { get; set; }
        public int SliderMinValue { get; set; }
        public int SliderCurrentValue { get; set; }
        public string DisplayValue { get; set; }
        public string DisplayCommentValue { get; set; }
        public string AnswerValue { get; set; }
        public bool CorrectAnswerIconIsVisible { get; set; }
        public bool WrongAnswerIconIsVisible { get; set; }
        public bool IsCheckWordButtonVisible { get; set; }
        public bool IsNextWordButtonVisible { get; set; }
        public int ProgressBarMaxValue { get; set; }
        public int ProgressBarCurrentValue { get; set; }
        public bool IsStartButtonVisible { get; set; }
        public bool IsStartButtonEnabled { get; set; } = true;
        public bool IsEndButtonVisible { get; set; }
        #endregion

        public DictationPageVM(IEasyLearnUserRepository userRepository, ICommonDictionaryRepository commonDictionaryRepository, IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository)
        {
            this.userRepository = userRepository;
            this.commonDictionaryRepository = commonDictionaryRepository;
            this.verbPrepositionDictionaryRepository = verbPrepositionDictionaryRepository;
            UpdateView();
            SetDefaultPageState();
        }

        #region Commands 
        public DelegateCommand RefreshSliderCommand { get; private set; }
        public DelegateCommand StartDictationCommand { get; private set; }
        public DelegateCommand CheckAnswerCommand { get; private set; }
        public DelegateCommand TryGoNextCommand { get; private set; }
        public DelegateCommand EndDictationCommand { get; private set; }
        public DelegateCommand PressEnterCommand { get; private set; }
        protected override void InitCommands()
        {
            this.RefreshSliderCommand = new DelegateCommand(arg => RefreshSlider());
            this.StartDictationCommand = new DelegateCommand(arg => StartCommonDictation());
            this.CheckAnswerCommand = new DelegateCommand(arg => CheckAnswer());
            this.TryGoNextCommand = new DelegateCommand(arg => TryGoNext());
            this.EndDictationCommand = new DelegateCommand(arg => StopDictation());
            this.PressEnterCommand = new DelegateCommand(arg => OnPressEnter());
        }
        #endregion

        public void UpdateView()
        {
            UpdateCurrentUserId();
            RefreshDictionaries();
            LoadSelectedDictionary();
            RefreshSlider();
        }
        private void RefreshDictionaries()
        {
            IEnumerable<CommonDictionary> commonDictionaries = App.GetService<ICommonDictionaryRepository>().GetUsersCommonDictionaries(currentUserId);
            IEnumerable<VerbPrepositionDictionnary> verbPrepositionDictionnaries = App.GetService<IVerbPrepositionDictionaryRepository>().GetUsersVerbPreposotionDictionaries(currentUserId);

            IEnumerable<DictionaryComboBoxItem> commonDictionaryViews = commonDictionaries.Select(dictionary => new DictionaryComboBoxItem(dictionary.Name, dictionary.Id, DictionaryType.CommonDictionary));
            IEnumerable<DictionaryComboBoxItem> verbPrepositionDictionnaryViews = verbPrepositionDictionnaries.Select(dictionary => new DictionaryComboBoxItem(dictionary.Name, dictionary.Id, DictionaryType.VerbPrepositionDictionary));
            DictionaryComboBoxItem irregularVerbDictionaryView = new DictionaryComboBoxItem("Неправильные глаголы", int.MinValue, DictionaryType.IrregularVerbDictionary);

            List<DictionaryComboBoxItem> dictionaries = commonDictionaryViews.Union(verbPrepositionDictionnaryViews).ToList();
            dictionaries.Add(irregularVerbDictionaryView);

            this.DictionaryComboBoxItems = new ObservableCollection<DictionaryComboBoxItem>(dictionaries);
            this.SelectedDictionaryComboBoxItem = this.DictionaryComboBoxItems[0];
        }
        
        private void LoadSelectedDictionary()
        {
            int selectedDictionaryId = this.SelectedDictionaryComboBoxItem.DictionaryId;
            DictionaryType selectedDictionaryType = this.SelectedDictionaryComboBoxItem.DictionaryType;
            switch (selectedDictionaryType)
            {
                case DictionaryType.CommonDictionary:
                    this.selectedCommonDictionary = commonDictionaryRepository.GetCommonDictionary(selectedDictionaryId);
                    break;
                case DictionaryType.VerbPrepositionDictionary:
                    this.selectedVerbPrepositionDictionnary = verbPrepositionDictionaryRepository.GetVerbPrepositionDictionary(selectedDictionaryId);
                    break;
            }
        }
        private void UpdateCurrentUserId()
        {
            this.currentUserId = userRepository.TryGetCurrentUser().Id;
        }
        private void StartCommonDictation()
        {
            SetDefaultPageState();
            IEnumerable<CommonRelation> commonRelations = ShuffleCommonRelation(this.selectedCommonDictionary.Relations.Take(this.SliderCurrentValue));
            this.commonDictationManager = new CommonDictationManager(commonRelations.ToList());
            this.isDictationStarted = true;
            CommonRelation commonRelation = commonDictationManager.Start();
            this.DisplayValue = commonRelation.RussianUnit.Value.NormalizeRegister();
            this.DisplayCommentValue = commonRelation.Comment.TryNormalizeRegister();
            SwitchStartAndEndButtons();
            FocusAnswerTextBox();
            SetProgressBarLength();
        }
        private void CheckAnswer()
        {
            if (!this.isDictationStarted)
                return;
            bool isTrue = this.commonDictationManager.IsAnswerCorrect(this.AnswerValue);
            if (isTrue && isFirstAnswer)
            {
                ShowCorrectAnswerIcon();
                IncreaseProgressBarValue();
                SwitchCheckAndNextButtons();
            }
            else if (!isTrue && isFirstAnswer)
            {
                ShowWrongAnswerIcon();
            }
            else if (isTrue)
            {
                SwitchCorrectAndWrongAnswerIcons();
                IncreaseProgressBarValue();
                SwitchCheckAndNextButtons();
            }
        }
        private void StopDictation()
        {
            SetDefaultPageState();
        }
        private void TryGoNext()
        {
            if (!this.isDictationStarted)
                return;
            if (this.commonDictationManager.GoNext())
            {
                this.DisplayValue = commonDictationManager.CurrentRelation.RussianUnit.Value.NormalizeRegister();
                this.DisplayCommentValue = commonDictationManager.CurrentRelation.Comment.TryNormalizeRegister();
                ClearAnswerValue();
                HideCorrectAndWrongAnswerIcons();
                SwitchCheckAndNextButtons();
            }
            else
            {
                StopDictation();
            }
        }
        private void OnPressEnter()
        {
            if (!this.isDictationStarted)
                return;
            if (this.IsCheckWordButtonVisible)
                CheckAnswer();
            else
                TryGoNext();
        }
        private void SetDefaultPageState()
        {
            this.isDictationStarted = false;
            this.isFirstAnswer = true;
            SetDefaultDisplayValue();
            SetDefaultDisplayCommentValue();
            ClearAnswerValue();
            ShowStartButton();
            ShowCheckButton();
            ResetProgressBarValue();
            HideCorrectAndWrongAnswerIcons();
        }


        #region Display actions
        private void RefreshSlider()
        {
            this.SliderMinValue = this.ItemsInSelectedDictionary > 0 ? 1 : 0;
            this.SliderMaxValue = this.ItemsInSelectedDictionary;
            this.SliderCurrentValue = this.SliderMaxValue;
        }
        private void ShowCheckButton()
        {
            this.IsCheckWordButtonVisible = true;
            this.IsNextWordButtonVisible = false;
        }
        private void ShowNextButton()
        {
            this.IsCheckWordButtonVisible = false;
            this.IsNextWordButtonVisible = true;
        }
        private void SwitchCheckAndNextButtons()
        {
            this.IsCheckWordButtonVisible = !this.IsCheckWordButtonVisible;
            this.IsNextWordButtonVisible = !this.IsNextWordButtonVisible;
        }
        private void ShowStartButton()
        {
            this.IsStartButtonVisible = true;
            this.IsEndButtonVisible = false;
        }
        private void ShowEndButton()
        {
            this.IsEndButtonVisible = true;
            this.IsStartButtonVisible = false;
        }
        private void SwitchStartAndEndButtons()
        {
            this.IsStartButtonVisible = !this.IsStartButtonVisible;
            this.IsEndButtonVisible= !this.IsEndButtonVisible;
        }
        private void ClearAnswerValue() => this.AnswerValue = string.Empty;
        private void SetDefaultDisplayValue() => this.DisplayValue = "EasyLearn";
        private void SetDefaultDisplayCommentValue() => this.DisplayCommentValue = string.Empty;
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
        private void SetProgressBarLength() => this.ProgressBarMaxValue = this.SliderCurrentValue;
        private void IncreaseProgressBarValue() => this.ProgressBarCurrentValue++;
        private void ResetProgressBarValue() => this.ProgressBarCurrentValue = 0;
        #endregion

        #region Helpers
        private IEnumerable<CommonRelation> ShuffleCommonRelation(IEnumerable<CommonRelation> relations)
        {
            Random random = new Random();
            return relations.OrderBy(relation => random.Next());
        }
        #endregion
    }
}
