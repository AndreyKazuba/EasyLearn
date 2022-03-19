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

namespace EasyLearn.VM.ViewModels.Pages
{
    public class DictationPageVM : ViewModel
    {
        private int currentUserId;
        private IEasyLearnUserRepository userRepository;
        private ICommonDictionaryRepository commonDictionaryRepository;
        private IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository;
        private CommonDictionary selectedCommonDictionary;
        private VerbPrepositionDictionnary selectedVerbPrepositionDictionnary;
        private CommonDictationManager commonDictationManager;

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
        public string AnswerValue { get; set; }
        public bool CorrectAnswerIconIsVisible { get; set; }
        public bool WrongAnswerIconIsVisible { get; set; }
        #endregion

        public DictationPageVM(IEasyLearnUserRepository userRepository, ICommonDictionaryRepository commonDictionaryRepository, IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository)
        {
            this.userRepository = userRepository;
            this.commonDictionaryRepository = commonDictionaryRepository;
            this.verbPrepositionDictionaryRepository = verbPrepositionDictionaryRepository;
            UpdateView();
        }

        #region Commands 
        public DelegateCommand RefreshSliderCommand { get; private set; }
        public DelegateCommand StartDictationCommand { get; private set; }
        public DelegateCommand CheckAnswerCommand { get; private set; }
        protected override void InitCommands()
        {
            this.RefreshSliderCommand = new DelegateCommand(arg => RefreshSlider());
            this.StartDictationCommand = new DelegateCommand(arg => StartCommonDictation());
            this.CheckAnswerCommand = new DelegateCommand(arg => CheckAnswer());
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
        private void RefreshSlider()
        {
            this.SliderMinValue = this.ItemsInSelectedDictionary > 0 ? 1 : 0;
            this.SliderMaxValue = this.ItemsInSelectedDictionary;
            this.SliderCurrentValue = this.SliderMaxValue;
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
            this.commonDictationManager = new CommonDictationManager(this.selectedCommonDictionary.Relations);
            this.DisplayValue = commonDictationManager.Start();
        }
        private void CheckAnswer()
        {
            bool isTrue = this.commonDictationManager.IsAnswerCorrect(this.AnswerValue);
            if (isTrue)
            {
                this.CorrectAnswerIconIsVisible = true;
                this.WrongAnswerIconIsVisible = false;
            }
            else
            {
                this.WrongAnswerIconIsVisible = true;
                this.CorrectAnswerIconIsVisible = false;
            }
        }
        private void TryGoNext()
        {
            if (this.commonDictationManager.GoNext())
            {
                this.DisplayValue = commonDictationManager.CurrentRelation.RussianUnit.Value;
            }
        }
    }
}
