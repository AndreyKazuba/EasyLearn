using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EasyLearn.VM.Core;
using EasyLearn.VM.Windows;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Data.Models;
using EasyLearn.Data.Enums;
using EasyLearn.UI.CustomControls;
using EasyLearn.VM.ViewModels.ExpandedElements;
using EasyLearn.Data.Constants;
using EasyLearn.Data.Helpers;
using EasyLearn.UI.Pages;
using EasyLearn.Infrastructure.Helpers;
using EasyLearn.UI;
using EasyLearn.Infrastructure.Validation;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using Page = EasyLearn.Infrastructure.Enums.Page;

namespace EasyLearn.VM.ViewModels.Pages
{
    public class EditCommonDictionaryPageVM : ViewModel
    {
        #region Repositories
        private readonly ICommonDictionaryRepository commonDictionaryRepository;
        private readonly ICommonRelationRepository commonRelationRepository;
        #endregion

        #region Private fields
        private bool awExampleInvalid = true;
        private bool uwExampleInvalid = true;
        private int exampleIdsCount;
        private int dictionaryId;
        private bool topMenuIsOpened = false;
        private Guid relationExistValidationRuleId;
        private CommonRelation currentRelationForUpdate;
        private List<CommonRelation> allCommonRelations;
        #endregion

        private bool CommonRelationExist
        {
            set
            {
                ValidationPool.Set(ValidationRulesGroup.AddCommonRelation, relationExistValidationRuleId, !value);
                this.AwCommonRelationHasExistLableIsVisible = value;
            }
        }

#pragma warning disable CS8618
        public EditCommonDictionaryPageVM(ICommonDictionaryRepository commonDictionaryRepository, ICommonRelationRepository commonRelationRepository)
        {
            this.commonDictionaryRepository = commonDictionaryRepository;
            this.commonRelationRepository = commonRelationRepository;
            SetAddingWindowRussianUnitTypes();
            SetAddingWidnowEnglishUnitTypes();
            this.relationExistValidationRuleId = ValidationPool.Register(ValidationRulesGroup.AddCommonRelation);
        }
#pragma warning restore CS8618

        #region Props for binding
        public ObservableCollection<UserControl> CommonRelationViews { get; set; }
        public ObservableCollection<ExampleView> AwExampleViews { get; set; } = new ObservableCollection<ExampleView>();
        public ObservableCollection<ExampleView> UwExampleViews { get; set; } = new ObservableCollection<ExampleView>();
        public SolidColorBrush HorisontalSeporatorColor => this.AwCommonRelationHasExistLableIsVisible ? new BrushConverter().ConvertFrom("#FFA70404") as SolidColorBrush ?? Brushes.Red : Brushes.LightGray;
        public string DictionaryName { get; set; }
        public string DictionaryDescription { get; set; }
        public string SearchStringValue { get; set; }
        public string AwEnglishValue { get; set; }
        public string AwRussianValue { get; set; }
        public string AwCommentValue { get; set; }
        public string AwExampleRussianValue { get; set; }
        public string AwExampleEnglishValue { get; set; }
        public string UwCommentValue { get; set; }
        public string UwExampleRussianValue { get; set; }
        public string UwExampleEnglishValue { get; set; }
        public bool AwConfirmButtonIsEnabled { get; set; }
        public bool AwCommonRelationHasExistLableIsVisible { get; set; }
        public bool AwAddExampleButtonIsEnabled { get; set; }
        public bool UwConfirmButtonIsEnabled { get; set; }
        public bool UwAddExampleButtonIsEnabled { get; set; }
        public ObservableCollection<UnitTypeComboBoxItem> AwRussianUnitTypes { get; set; }
        public ObservableCollection<UnitTypeComboBoxItem> AwEnglishUnitTypes { get; set; }
        public UnitTypeComboBoxItem AwSelectedRussianUnitType { get; set; }
        public UnitTypeComboBoxItem AwSelectedEnglishUnitType { get; set; }
        #endregion

        #region Events
        protected override void InitEvents()
        {
            EditCommonDictionaryPage.RussianValueTextBoxEnterDown += OnRussianValueTextBoxEnterDown;
            EditCommonDictionaryPage.EnglishValueTextBoxEnterDown += OnEnglishValueTextBoxEnterDown;
            EditCommonDictionaryPage.RussianUnitTypeComboBoxEnterDown += OnRussianUnitTypeComboBoxEnterDown;
            EditCommonDictionaryPage.EnglishUnitTypeComboBoxEnterDown += OnEnglishUnitTypeComboBoxEnterDown;
            EditCommonDictionaryPage.CommentValueTextBoxEnterDown += OnCommentValueTextBoxEnterDown;
            EditCommonDictionaryPage.AddingWindowExampleRussianValueTextBoxEnterDown += OnAddingWindowExampleRussianValueTextBoxEnterDown;
            EditCommonDictionaryPage.AddingWindowExampleEnglishValueTextBoxEnterDown += OnAddingWindowExampleEnglishValueTextBoxEnterDown;
            EditCommonDictionaryPage.UpdateWindowExampleRussianValueTextBoxEnterDown += OnUpdateWindowExampleRussianValueTextBoxEnterDown;
            EditCommonDictionaryPage.UpdateWindowExampleEnglishValueTextBoxEnterDown += OnUpdateWindowExampleEnglishValueTextBoxEnterDown;
            AppWindow.WindowCtrlNDown += OnWindowCtrlNDown;
            AppWindow.WindowEscDown += OnWindowEscDown;
            AppWindow.GoBackButtonClick += OnGoBackButtonClick;
            AppWindow.OpenMenuButtonClick += OnOpenMenuButtonClick;
            AppWindow.DrawerButtonClick += OnDrawerButtonClick;
        }
        private void OnRussianValueTextBoxEnterDown() => FocusEnglishValueTextBox();
        private void OnEnglishValueTextBoxEnterDown() => FocusRussianUnitTypeComboBox();
        private void OnRussianUnitTypeComboBoxEnterDown() => FocusEnglishUnitTypeComboBox();
        private void OnEnglishUnitTypeComboBoxEnterDown() => FocusCommentValueTextBox();
        private void OnAddingWindowExampleRussianValueTextBoxEnterDown() => AwFocusExampleEnglishValueField();
        private void OnAddingWindowExampleEnglishValueTextBoxEnterDown()
        {
            if (this.awExampleInvalid)
                return;
            AwAddExampleButtonSoftClick();
        }
        private void OnUpdateWindowExampleRussianValueTextBoxEnterDown() => UwFocusExampleEnglishValueField();
        private void OnUpdateWindowExampleEnglishValueTextBoxEnterDown()
        {
            if (this.uwExampleInvalid)
                return;
            UwAddExampleButtonSoftClick();
        }
        private void OnCommentValueTextBoxEnterDown()
        {
            if (ValidationPool.IsValid(ValidationRulesGroup.AddCommonRelation))
                AddingNewCommonRelationButtonSoftClick();
        }
        private void OnWindowCtrlNDown()
        {
            if (App.GetService<AppWindowVM>().CurrentPage == Page.EditCommonWordListPage)
            {
                FocusRussianValueTextBox();
                OpenCommonRelationAddingWindowButtonSoftClick();
            }
        }
        private void OnWindowEscDown()
        {
            if (App.GetService<AppWindowVM>().CurrentPage == Page.EditCommonWordListPage)
                NewCommonRelationAddingWindowCancelButtonSoftClick();
        }
        private void OnGoBackButtonClick()
        {
            App.GetService<AppWindowVM>().HideGoBackButtonCommand.Execute();
            GoBack();
        }
        private void OnOpenMenuButtonClick()
        {
            if (topMenuIsOpened)
            {
                CloseMenuButtonSoftClick();
                topMenuIsOpened = false;
            }
            else
            {
                OpenMenuButtonSoftClick();
                topMenuIsOpened = true;
            }
        }
        private void OnDrawerButtonClick() => App.GetService<AppWindowVM>().HideGoBackButtonCommand.Execute();
        #endregion

        #region Commands
        public Command GoBackCommand { get; private set; }
        public Command CreateCommonRelationCommand { get; private set; }
        public Command DeleteCommonRelationCommand { get; private set; }
        public Command DeleteAllCommonRelationsCommand { get; private set; }
        public Command<int> SetDictionaryAsCurrentCommand { get; private set; }
        public Command AwCheckCommonRelationForExistingCommand { get; private set; }
        public Command SearchCommonRelationsCommand { get; private set; }
        public Command UpdateCommonRelationCommand { get; private set; }
        public Command<int> RemoveExampleViewCommand { get; private set; }
        public Command AwClearCommand { get; private set; }
        public Command AwAddExampleViewCommand { get; private set; }
        public Command AwUpdateConfirmButtonAvailabilityCommand { get; private set; }
        public Command AwClearExampleAddingSectionCommand { get; private set; }
        public Command AwFocusExampleRussianValueFieldCommand { get; private set; }
        public Command AwValidateExampleSectionCommand { get; private set; }
        public Command AwCheckExampleFieldsMaxLengthVisibilityCommand { get; private set; }
        public Command AwOpenWindowCommand { get; private set; }
        public Command<int> UwOpenWindowCommand { get; private set; }
        public Command UwAddExampleViewCommand { get; private set; }
        public Command UwClearCommand { get; private set; }
        public Command UwUpdateConfirmButtonAvailabilityCommand { get; private set; }
        public Command UwClearExampleAddingSectionCommand { get; private set; }
        public Command UwFocusExampleRussianValueFieldCommand { get; private set; }
        public Command UwValidateExampleSectionCommand { get; private set; }
        public Command UwCheckExampleFieldsMaxLengthVisibilityCommand { get; private set; }
        protected override void InitCommands()
        {
            this.GoBackCommand = new Command(GoBack);
            this.CreateCommonRelationCommand = new Command(async () => await CreateCommonRelation());
            this.DeleteCommonRelationCommand = new Command(async () => await DeleteCommonRelation());
            this.DeleteAllCommonRelationsCommand = new Command(async () => await DeleteAllCommonRelations());
            this.AwClearCommand = new Command(AwClear);
            this.SetDictionaryAsCurrentCommand = new Command<int>(async commonDictionaryId => await SetDictionaryAsCurrent(commonDictionaryId));
            this.AwUpdateConfirmButtonAvailabilityCommand = new Command(AwUpdateConfirmButtonAvailability);
            this.AwCheckCommonRelationForExistingCommand = new Command(AwCheckCommonRelationForExisting);
            this.SearchCommonRelationsCommand = new Command(SearchCommonRelations);
            this.AwAddExampleViewCommand = new Command(AwAddExampleView);
            this.RemoveExampleViewCommand = new Command<int>(RemoveExampleView);
            this.AwClearExampleAddingSectionCommand = new Command(AwClearExampleAddingSection);
            this.AwFocusExampleRussianValueFieldCommand = new Command(AwFocusExampleRussianValueField);
            this.AwValidateExampleSectionCommand = new Command(AwValidateExampleSection);
            this.AwCheckExampleFieldsMaxLengthVisibilityCommand = new Command(AwCheckExampleFieldsMaxLengthVisibility);
            this.AwOpenWindowCommand = new Command(AwOpenWindow);
            this.UwOpenWindowCommand = new Command<int>(UwOpenWindow);
            this.UpdateCommonRelationCommand = new Command(async () => await UpdateCommonRelation());
            this.UwAddExampleViewCommand = new Command(UwAddExampleView);
            this.UwAddExampleViewCommand = new Command(UwAddExampleView);
            this.UwClearCommand = new Command(UwClear);
            this.UwUpdateConfirmButtonAvailabilityCommand = new Command(UwUpdateConfirmButtonAvailability);
            this.UwClearExampleAddingSectionCommand = new Command(UwClearExampleAddingSection);
            this.UwFocusExampleRussianValueFieldCommand = new Command(UwFocusExampleRussianValueField);
            this.UwValidateExampleSectionCommand = new Command(UwValidateExampleSection);
            this.UwCheckExampleFieldsMaxLengthVisibilityCommand = new Command(UwCheckExampleFieldsMaxLengthVisibility);
        }
        #endregion

        #region Command logic methods
        private void GoBack() => App.GetService<AppWindowVM>().OpenDictionariesPageCommand.Execute();
        private async Task CreateCommonRelation()
        {
            string russianUnitValue = this.AwRussianValue;
            string englishUnitValue = this.AwEnglishValue;
            UnitType englishUnitType = this.AwSelectedEnglishUnitType.UnitType;
            UnitType russianUnitType = this.AwSelectedRussianUnitType.UnitType;
            string? comment = StringHelper.NullIfEmptyOrWhiteSpace(this.AwCommentValue);
            int commonDictionaryId = this.dictionaryId;
            string? firstExampleRussianValue = null;
            string? firstExampleEnglishValue = null;
            string? secondExampleRussianValue = null;
            string? secondExampleEnglishValue = null;

            // remove 
            bool firstExampleExist = false;
            bool secondExampleExist = false;

            if (this.AwExampleViews.Count > 0)
                firstExampleExist = true;
            if (this.AwExampleViews.Count > 1)
                secondExampleExist = true;

            if (firstExampleExist)
            {
                firstExampleRussianValue = this.AwExampleViews.ToArray()[0].RussianValue;
                firstExampleEnglishValue = this.AwExampleViews.ToArray()[0].EnglishValue;
            }
            if (secondExampleExist)
            {
                secondExampleRussianValue = this.AwExampleViews.ToArray()[1].RussianValue;
                secondExampleEnglishValue = this.AwExampleViews.ToArray()[1].EnglishValue;
            }
            //

            CommonRelation newCommonRelation = await commonRelationRepository.CreateCommonRelation(
                russianUnitValue,
                russianUnitType,
                englishUnitValue,
                englishUnitType,
                commonDictionaryId,
                comment,
                firstExampleRussianValue,
                firstExampleEnglishValue,
                secondExampleRussianValue,
                secondExampleEnglishValue);
            AddCommonRelationToUI(newCommonRelation);
        }
        private async Task DeleteCommonRelation()
        {
            CommonRelationView commonRelationView = FindCommonRelationView(currentRelationForUpdate.Id);
            this.CommonRelationViews.Remove(commonRelationView);
            await commonRelationRepository.DeleteCommonRelation(currentRelationForUpdate.Id);
        }
        private async Task DeleteAllCommonRelations()
        {
            this.CommonRelationViews.Clear();
            await commonRelationRepository.DeleteAllDictionaryRelations(this.dictionaryId);
        }
        private void AwClear()
        {
            this.AwEnglishValue = String.Empty;
            this.AwRussianValue = String.Empty;
            this.AwCommentValue = String.Empty;
            this.AwExampleRussianValue = String.Empty;
            this.AwExampleEnglishValue = String.Empty;
            this.AwSelectedRussianUnitType = AwRussianUnitTypes[0];
            this.AwSelectedEnglishUnitType = AwEnglishUnitTypes[0];
            this.AwExampleViews.Clear();
            this.AwAddExampleButtonIsEnabled = false;
        }
        private async Task SetDictionaryAsCurrent(int commonDictionaryId)
        {
            CommonDictionary commonDictionary = await commonDictionaryRepository.GetCommonDictionaryAsync(commonDictionaryId);
            this.dictionaryId = commonDictionaryId;
            this.DictionaryName = commonDictionary.Name;
            this.DictionaryDescription = StringHelper.EmptyIfNull(commonDictionary.Description);
            this.allCommonRelations = commonDictionary.Relations;
            IEnumerable<CommonRelationView> commonRelationViews = commonDictionary.Relations
                .Select(commonRelation => CommonRelationView.Create(commonRelation))
                .OrderBy(commonRelationView => commonRelationView.Order);
            this.CommonRelationViews = new ObservableCollection<UserControl>();
            AddShadowRelationView();
            foreach (CommonRelationView commonRelationView in commonRelationViews)
                this.CommonRelationViews.Add(commonRelationView);
        }
        private void AwUpdateConfirmButtonAvailability() => AwConfirmButtonIsEnabled = ValidationPool.IsValid(ValidationRulesGroup.AddCommonRelation);
        private void AwCheckCommonRelationForExisting()
        {
            string englishValue = this.AwEnglishValue;
            string russianValue = this.AwRussianValue;
            UnitType englishUnitType = this.AwSelectedEnglishUnitType.UnitType;
            UnitType russianUnitType = this.AwSelectedRussianUnitType.UnitType;
            this.CommonRelationExist = commonRelationRepository.IsCommonRelationExist(russianValue, russianUnitType, englishValue, englishUnitType, this.dictionaryId);
        }
        private void SearchCommonRelations()
        {
            string searchingString = this.SearchStringValue;
            foreach(UserControl userControl in this.CommonRelationViews)
            {
                CommonRelationView? commonRelationView = userControl as CommonRelationView;
                if (commonRelationView is null)
                    continue;
                bool isMatch = commonRelationView.RussianValue.Contains(searchingString) || commonRelationView.EnglishValue.Contains(searchingString);
                if (isMatch)
                    commonRelationView.Show();
                else
                    commonRelationView.Collapse();
            }
        }
        private void AwAddExampleView()
        {
            if (this.awExampleInvalid || this.AwExampleViews.Count >= ModelConstants.MaxExamplesCount)
                return;
            this.AwExampleViews.Add(ExampleView.Create(this.AwExampleRussianValue, this.AwExampleEnglishValue, ++exampleIdsCount, false));
        }
        private void RemoveExampleView(int exampleId)
        {
            ExampleView? awExampleView = AwTryFindExampleView(exampleId);
            ExampleView? uwExampleView = UwTryFindExampleView(exampleId);
            if (awExampleView is not null)
                this.AwExampleViews.Remove(awExampleView);
            if (uwExampleView is not null)
                this.UwExampleViews.Remove(uwExampleView);
            AwValidateExampleSection();
            UwValidateExampleSection();
        }
        private void AwClearExampleAddingSection()
        {
            this.AwExampleRussianValue = String.Empty;
            this.AwExampleEnglishValue = String.Empty;
        }
        private void AwFocusExampleRussianValueField() => App.GetService<EditCommonDictionaryPage>().awExampleRussianValueField.Focus();
        private void UwFocusExampleRussianValueField() => App.GetService<EditCommonDictionaryPage>().uwExampleRussianValueField.Focus();

        private void AwValidateExampleSection()
        {
            bool empty = String.IsNullOrWhiteSpace(this.AwExampleEnglishValue) || String.IsNullOrWhiteSpace(this.AwExampleRussianValue);

            if (empty || this.AwExampleViews.Count >= ModelConstants.MaxExamplesCount)
            {
                this.awExampleInvalid = true;
                this.AwAddExampleButtonIsEnabled = false;
                return;
            }
            this.awExampleInvalid = false;
            this.AwAddExampleButtonIsEnabled = true;
        }
        private void AwCheckExampleFieldsMaxLengthVisibility()
        {
            bool inRange = (!String.IsNullOrWhiteSpace(this.AwExampleEnglishValue) && this.AwExampleEnglishValue.Length > 20)
                        || (!String.IsNullOrWhiteSpace(this.AwExampleRussianValue) && this.AwExampleRussianValue.Length > 20);
            if (inRange)
                AwShowExampleFieldsMaxLength();
            else
                AwHideExampleFieldsMaxLength();
        }
        private void AwOpenWindow()
        {
            OpenCommonRelationAddingWindowButtonSoftClick();
        }
        private void UwOpenWindow(int commonRelationId)
        {
            CommonRelation commonRelation = commonRelationRepository.GetCommonRelation(commonRelationId);
            this.currentRelationForUpdate = commonRelation;
            this.UwCommentValue = StringHelper.EmptyIfNull(commonRelation.Comment);
            UwExampleViews.Clear();
            if (commonRelation.IsFirstExampleExist)
                this.UwExampleViews.Add(ExampleView.Create(commonRelation.FirstExampleRussianValue.EmptyIfNull(), commonRelation.FirstExampleEnglishValue.EmptyIfNull(), ++exampleIdsCount, false));
            if (commonRelation.IsSecondExampleExist)
                this.UwExampleViews.Add(ExampleView.Create(commonRelation.SecondExampleRussianValue.EmptyIfNull(), commonRelation.SecondExampleEnglishValue.EmptyIfNull(), ++exampleIdsCount, false));
            OpenCommonRelationUpdateWindowButtonSoftClick();
        }
        private async Task UpdateCommonRelation()
        {
            int id = currentRelationForUpdate.Id;
            string? comment = StringHelper.NullIfEmptyOrWhiteSpace(this.UwCommentValue);
            string? firstExampleRussianValue = null;
            string? firstExampleEnglishValue = null;
            string? secondExampleRussianValue = null;
            string? secondExampleEnglishValue = null;

            // remove 
            bool firstExampleExist = false;
            bool secondExampleExist = false;

            if (this.UwExampleViews.Count > 0)
                firstExampleExist = true;
            if (this.UwExampleViews.Count > 1)
                secondExampleExist = true;

            if (firstExampleExist)
            {
                firstExampleRussianValue = this.UwExampleViews.ToArray()[0].RussianValue;
                firstExampleEnglishValue = this.UwExampleViews.ToArray()[0].EnglishValue;
            }
            if (secondExampleExist)
            {
                secondExampleRussianValue = this.UwExampleViews.ToArray()[1].RussianValue;
                secondExampleEnglishValue = this.UwExampleViews.ToArray()[1].EnglishValue;
            }
            //

            CommonRelation updatedCommonRelation = await commonRelationRepository.UpdateCommonRelation(
                id,
                comment,
                firstExampleRussianValue,
                firstExampleEnglishValue,
                secondExampleRussianValue,
                secondExampleEnglishValue);
            UpdateCommonRelationOnUI(updatedCommonRelation);
        }
        private void UwAddExampleView()
        {
            if (this.uwExampleInvalid || this.UwExampleViews.Count >= ModelConstants.MaxExamplesCount)
                return;
            this.UwExampleViews.Add(ExampleView.Create(this.UwExampleRussianValue, this.UwExampleEnglishValue, ++exampleIdsCount, false));
        }
        private void UwClear()
        {
            this.UwCommentValue = String.Empty;
            this.UwExampleRussianValue = String.Empty;
            this.UwExampleEnglishValue = String.Empty;
            this.UwExampleViews.Clear();
            this.UwAddExampleButtonIsEnabled = false;
        }
        private void UwUpdateConfirmButtonAvailability() => UwConfirmButtonIsEnabled = ValidationPool.IsValid(ValidationRulesGroup.UpdateCommonRelation);
        private void UwClearExampleAddingSection()
        {
            this.UwExampleRussianValue = String.Empty;
            this.UwExampleEnglishValue = String.Empty;
        }
        private void UwValidateExampleSection()
        {
            bool empty = String.IsNullOrWhiteSpace(this.UwExampleEnglishValue) || String.IsNullOrWhiteSpace(this.UwExampleRussianValue);

            if (empty || this.UwExampleViews.Count >= ModelConstants.MaxExamplesCount)
            {
                this.uwExampleInvalid = true;
                this.UwAddExampleButtonIsEnabled = false;
                return;
            }
            this.uwExampleInvalid = false;
            this.UwAddExampleButtonIsEnabled = true;
        }
        private void UwCheckExampleFieldsMaxLengthVisibility()
        {
            bool inRange = (!String.IsNullOrWhiteSpace(this.UwExampleEnglishValue) && this.UwExampleEnglishValue.Length > 20)
                        || (!String.IsNullOrWhiteSpace(this.UwExampleRussianValue) && this.UwExampleRussianValue.Length > 20);
            if (inRange)
                UwShowExampleFieldsMaxLength();
            else
                UwHideExampleFieldsMaxLength();
        }
        #endregion

        #region Other private members
        private ExampleView? AwTryFindExampleView(int exampleId) => this.AwExampleViews.FirstOrDefault(exampleView => exampleView.Id == exampleId);
        private ExampleView? UwTryFindExampleView(int exampleId) => this.UwExampleViews.FirstOrDefault(exampleView => exampleView.Id == exampleId);
        private CommonRelationView FindCommonRelationView(int commonRelationId) => (CommonRelationView)this.CommonRelationViews.First(commonRelationView =>
        {
            CommonRelationView? commonView = commonRelationView as CommonRelationView;
            if (commonView is null)
                return false;
            else
                return ((CommonRelationView)commonRelationView).Id == commonRelationId;
        });

        private void AddCommonRelationToUI(CommonRelation commonRelation)
        {
            this.CommonRelationViews.Add(CommonRelationView.Create(commonRelation));
        }
        private void UpdateCommonRelationOnUI(CommonRelation updatedCommonRelation)
        {
            CommonRelationView relationView = FindCommonRelationView(updatedCommonRelation.Id);
            relationView.UpdateView(updatedCommonRelation);
        }
        private void SetAddingWindowRussianUnitTypes()
        {
            ObservableCollection<UnitTypeComboBoxItem> russianUnitTypes = new ObservableCollection<UnitTypeComboBoxItem>(
                new[]
                {
                    new UnitTypeComboBoxItem(UnitTypeRussianNames.Noun, UnitType.Noun),
                    new UnitTypeComboBoxItem(UnitTypeRussianNames.Verb, UnitType.Verb),
                    new UnitTypeComboBoxItem(UnitTypeRussianNames.Adjective, UnitType.Adjective),
                    new UnitTypeComboBoxItem(UnitTypeRussianNames.Preposition, UnitType.Preposition),
                    new UnitTypeComboBoxItem(UnitTypeRussianNames.Sentence, UnitType.Sentence),
                    new UnitTypeComboBoxItem(UnitTypeRussianNames.Phrase, UnitType.Phrase),
                    new UnitTypeComboBoxItem(UnitTypeRussianNames.CombinationOfWords, UnitType.CombinationOfWords),
                    new UnitTypeComboBoxItem(UnitTypeRussianNames.Pronoun, UnitType.Pronoun),
                    new UnitTypeComboBoxItem(UnitTypeRussianNames.Numeral, UnitType.Numeral),
                    new UnitTypeComboBoxItem(UnitTypeRussianNames.Adverb, UnitType.Adverb),
                });
            UnitTypeComboBoxItem selectedRussianUnitType = russianUnitTypes[0];
            this.AwRussianUnitTypes = russianUnitTypes;
            this.AwSelectedRussianUnitType = selectedRussianUnitType;
        }
        private void SetAddingWidnowEnglishUnitTypes()
        {
            ObservableCollection<UnitTypeComboBoxItem> englishUnitTypes = new ObservableCollection<UnitTypeComboBoxItem>(
                new[]
                {
                    new UnitTypeComboBoxItem(UnitTypeEnglishNames.Noun, UnitType.Noun),
                    new UnitTypeComboBoxItem(UnitTypeEnglishNames.Verb, UnitType.Verb),
                    new UnitTypeComboBoxItem(UnitTypeEnglishNames.Adjective, UnitType.Adjective),
                    new UnitTypeComboBoxItem(UnitTypeEnglishNames.ModalVerb, UnitType.ModalVerb),
                    new UnitTypeComboBoxItem(UnitTypeEnglishNames.Preposition, UnitType.Preposition),
                    new UnitTypeComboBoxItem(UnitTypeEnglishNames.Sentence, UnitType.Sentence),
                    new UnitTypeComboBoxItem(UnitTypeEnglishNames.Phrase, UnitType.Phrase),
                    new UnitTypeComboBoxItem(UnitTypeEnglishNames.CombinationOfWords, UnitType.CombinationOfWords),
                    new UnitTypeComboBoxItem(UnitTypeEnglishNames.Pronoun, UnitType.Pronoun),
                    new UnitTypeComboBoxItem(UnitTypeEnglishNames.Numeral, UnitType.Numeral),
                    new UnitTypeComboBoxItem(UnitTypeEnglishNames.Adverb, UnitType.Adverb),
                });
            UnitTypeComboBoxItem selectedEnglishUnitType = englishUnitTypes[0];
            this.AwEnglishUnitTypes = englishUnitTypes;
            this.AwSelectedEnglishUnitType = selectedEnglishUnitType;
        }

        #endregion
        private void AwShowExampleFieldsMaxLength()
        {
            TextBox exampleRussianValueField = App.GetService<EditCommonDictionaryPage>().awExampleRussianValueField;
            TextBox exampleEnglishValueField = App.GetService<EditCommonDictionaryPage>().awExampleEnglishValueField;
            exampleRussianValueField.MaxLength = ModelConstants.ExampleValueMaxLength;
            exampleEnglishValueField.MaxLength = ModelConstants.ExampleValueMaxLength;
            exampleRussianValueField.Margin = new Thickness(0, 0, 7, 7);
            exampleEnglishValueField.Margin = new Thickness(7, 0, 0, 7);
        }
        private void AwHideExampleFieldsMaxLength()
        {
            TextBox exampleRussianValueField = App.GetService<EditCommonDictionaryPage>().awExampleRussianValueField;
            TextBox exampleEnglishValueField = App.GetService<EditCommonDictionaryPage>().awExampleEnglishValueField;
            exampleRussianValueField.MaxLength = 0;
            exampleEnglishValueField.MaxLength = 0;
            exampleRussianValueField.Margin = new Thickness(0, 0, 7, 0);
            exampleEnglishValueField.Margin = new Thickness(7, 0, 0, 0);
        }
        private void UwShowExampleFieldsMaxLength()
        {
            TextBox uwExampleRussianValueField = App.GetService<EditCommonDictionaryPage>().uwExampleRussianValueField;
            TextBox uwExampleEnglishValueField = App.GetService<EditCommonDictionaryPage>().uwExampleEnglishValueField;
            uwExampleRussianValueField.MaxLength = ModelConstants.ExampleValueMaxLength;
            uwExampleEnglishValueField.MaxLength = ModelConstants.ExampleValueMaxLength;
            uwExampleRussianValueField.Margin = new Thickness(0, 0, 7, 7);
            uwExampleEnglishValueField.Margin = new Thickness(7, 0, 0, 7);
        }
        private void UwHideExampleFieldsMaxLength()
        {
            TextBox uwExampleRussianValueField = App.GetService<EditCommonDictionaryPage>().uwExampleRussianValueField;
            TextBox uwExampleEnglishValueField = App.GetService<EditCommonDictionaryPage>().uwExampleEnglishValueField;
            uwExampleRussianValueField.MaxLength = 0;
            uwExampleEnglishValueField.MaxLength = 0;
            uwExampleRussianValueField.Margin = new Thickness(0, 0, 7, 0);
            uwExampleEnglishValueField.Margin = new Thickness(7, 0, 0, 0);
        }
        private void FocusRussianValueTextBox() => App.GetService<EditCommonDictionaryPage>().newCommonRelationRussianValueTextBox.Focus();
        private void FocusEnglishValueTextBox() => App.GetService<EditCommonDictionaryPage>().newCommonRelationEnglishValueTextBox.Focus();
        private void FocusRussianUnitTypeComboBox() => App.GetService<EditCommonDictionaryPage>().newCommonRelationRussianUnitTypeComboBox.Focus();
        private void FocusEnglishUnitTypeComboBox() => App.GetService<EditCommonDictionaryPage>().newCommonRelationEnglishUnitTypeComboBox.Focus();
        private void FocusCommentValueTextBox() => App.GetService<EditCommonDictionaryPage>().newCommonRelationCommentValueTextBox.Focus();
        private void AwFocusExampleEnglishValueField() => App.GetService<EditCommonDictionaryPage>().awExampleEnglishValueField.Focus();
        private void UwFocusExampleEnglishValueField() => App.GetService<EditCommonDictionaryPage>().uwExampleEnglishValueField.Focus();

        private void AddingNewCommonRelationButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().newCommonRelationAddingButton.SoftClick();
        private void OpenCommonRelationAddingWindowButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().openNewCommonRelationAddingWindowButton.SoftClick();
        private void OpenCommonRelationUpdateWindowButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().openCommonRelationSettingsWindowButton.SoftClick();
        private void NewCommonRelationAddingWindowCancelButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().newCommonRelationAddingWindowCancelButton.SoftClick();
        private void AwAddExampleButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().awAddExampleButton.SoftClick();
        private void UwAddExampleButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().uwAddExampleButton.SoftClick();
        private void AddShadowRelationView() => this.CommonRelationViews.Add(ShadowCommonRelationView.Create());
        private void OpenMenuButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().openMenuButton.SoftClick();
        private void CloseMenuButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().closeMenuButton.SoftClick();
    }
}
