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
        private bool exampleInvalid = true;
        private int exampleIdsCount;
        private int dictionaryId;
        private bool bottomMenuIsOpened = false;
        private Guid relationExistValidationRuleId;
        private CommonRelation currentRelationForUpdate;
        private List<CommonRelation> allCommonRelations;
        #endregion

        private bool CommonRelationExist
        {
            set
            {
                ValidationPool.Set(ValidationRulesGroup.AddCommonRelation, relationExistValidationRuleId, !value);
                this.CommonRelationHasExistLableIsVisible = value;
                this.AddingWidnowTopVerticalExpanderMargin = value ? new Thickness(7, 0, 7, 12) : new Thickness(7, 0, 7, 0);
            }
        }
        private bool WindowFieldsEqualsCurrentRelation => currentRelationForUpdate.RussianUnit.Value == AddingWindowRussianValue
                    && currentRelationForUpdate.EnglishUnit.Value == AddingWindowEnglishValue
                    && currentRelationForUpdate.RussianUnit.Type == AddingWindowSelectedRussianUnitType.UnitType
                    && currentRelationForUpdate.EnglishUnit.Type == AddingWindowSelectedEnglishUnitType.UnitType;

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
        public ObservableCollection<ExampleView> ExampleViews { get; set; } = new ObservableCollection<ExampleView>();
        public SolidColorBrush HorisontalSeporatorColor => this.CommonRelationHasExistLableIsVisible ? new BrushConverter().ConvertFrom("#FFA70404") as SolidColorBrush ?? Brushes.Red : Brushes.LightGray;
        public string DictionaryName { get; set; }
        public string DictionaryDescription { get; set; }
        public string AddingWindowEnglishValue { get; set; }
        public string AddingWindowRussianValue { get; set; }
        public string AddingWindowCommentValue { get; set; }
        public string SearchStringValue { get; set; }
        public string ExampleRussianValue { get; set; }
        public string ExampleEnglishValue { get; set; }
        public bool IsConfirmCommonRelationAddingButtonEnabled { get; set; }
        public bool CommonRelationHasExistLableIsVisible { get; set; }
        public bool AddExampleButtonIsVisible { get; set; }
        public bool ExamleWarningIconIsVisible { get; set; }
        public Thickness AddingWidnowTopVerticalExpanderMargin { get; set; } = new Thickness(7, 0, 7, 0);
        public ObservableCollection<UnitTypeComboBoxItem> AddingWindowRussianUnitTypes { get; set; }
        public ObservableCollection<UnitTypeComboBoxItem> AddingWidnowEnglishUnitTypes { get; set; }
        public UnitTypeComboBoxItem AddingWindowSelectedRussianUnitType { get; set; }
        public UnitTypeComboBoxItem AddingWindowSelectedEnglishUnitType { get; set; }
        #endregion

        #region Events
        protected override void InitEvents()
        {
            EditCommonDictionaryPage.RussianValueTextBoxEnterDown += OnRussianValueTextBoxEnterDown;
            EditCommonDictionaryPage.EnglishValueTextBoxEnterDown += OnEnglishValueTextBoxEnterDown;
            EditCommonDictionaryPage.RussianUnitTypeComboBoxEnterDown += OnRussianUnitTypeComboBoxEnterDown;
            EditCommonDictionaryPage.EnglishUnitTypeComboBoxEnterDown += OnEnglishUnitTypeComboBoxEnterDown;
            EditCommonDictionaryPage.CommentValueTextBoxEnterDown += OnCommentValueTextBoxEnterDown;
            EditCommonDictionaryPage.ExampleRussianValueTextBoxEnterDown += OnExampleRussianValueTextBoxEnterDown;
            EditCommonDictionaryPage.ExampleEnglishValueTextBoxEnterDown += OnExampleEnglishValueTextBoxEnterDown;
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
        private void OnExampleRussianValueTextBoxEnterDown() => FocusExampleEnglishValueField();
        private void OnExampleEnglishValueTextBoxEnterDown()
        {
            if (this.exampleInvalid)
                return;
            AddExampleButtonSoftClick();
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
            if (bottomMenuIsOpened)
            {
                CloseMenuButtonSoftClick();
                bottomMenuIsOpened = false;
            }
            else
            {
                OpenMenuButtonSoftClick();
                bottomMenuIsOpened = true;
            }
        }
        private void OnDrawerButtonClick() => App.GetService<AppWindowVM>().HideGoBackButtonCommand.Execute();
        #endregion

        #region Commands
        public Command GoBackCommand { get; private set; }
        public Command CreateCommonRelationCommand { get; private set; }
        public Command DeleteCommonRelationCommand { get; private set; }
        public Command DeleteAllCommonRelationsCommand { get; private set; }
        public Command ClearAddingWindowCommand { get; private set; }
        public Command<int> SetDictionaryAsCurrentCommand { get; private set; }
        public Command UpdateConfirmCommonRelationAddingButtonAvailabilityCommand { get; private set; }
        public Command CheckCommonRelationForExistingCommand { get; private set; }
        public Command SearchCommonRelationsCommand { get; private set; }
        public Command AddExampleViewCommand { get; private set; }
        public Command<int> RemoveExampleViewCommand { get; private set; }
        public Command ClearExampleAddingFieldsCommand { get; private set; }
        public Command FocusExampleRussianValueFieldCommand { get; private set; }
        public Command ValidateExampleSectionCommand { get; private set; }
        public Command CheckExampleFieldsMaxLengthVisibilityCommand { get; private set; }
        public Command OpenCommonRelationAddingWindowCommand { get; private set; }
        public Command<int> OpenCommonRelationSettingsWindowCommand { get; private set; }
        //public Command<int> SetCommonRelationForUpdatingCommand { get; private set; }
        public Command UpdateCommonRelationCommand { get; private set; }
        protected override void InitCommands()
        {
            this.GoBackCommand = new Command(GoBack);
            this.CreateCommonRelationCommand = new Command(async () => await CreateCommonRelation());
            this.DeleteCommonRelationCommand = new Command(async() => await DeleteCommonRelation());
            this.DeleteAllCommonRelationsCommand = new Command(async () => await DeleteAllCommonRelations());
            this.ClearAddingWindowCommand = new Command(ClearAddingWindow);
            this.SetDictionaryAsCurrentCommand = new Command<int>(async commonDictionaryId => await SetDictionaryAsCurrent(commonDictionaryId));
            this.UpdateConfirmCommonRelationAddingButtonAvailabilityCommand = new Command(UpdateConfirmCommonRelationAddingButtonAvailability);
            this.CheckCommonRelationForExistingCommand = new Command(CheckCommonRelationForExisting);
            this.SearchCommonRelationsCommand = new Command(SearchCommonRelations);
            this.AddExampleViewCommand = new Command(AddExampleView);
            this.RemoveExampleViewCommand = new Command<int>(RemoveExampleView);
            this.ClearExampleAddingFieldsCommand = new Command(ClearExampleAddingFields);
            this.FocusExampleRussianValueFieldCommand = new Command(FocusExampleRussianValueField);
            this.ValidateExampleSectionCommand = new Command(ValidateExampleSection);
            this.CheckExampleFieldsMaxLengthVisibilityCommand = new Command(CheckExampleFieldsMaxLengthVisibility);
            this.OpenCommonRelationAddingWindowCommand = new Command(OpenCommonRelationAddingWindow);
            this.OpenCommonRelationSettingsWindowCommand = new Command<int>(OpenCommonRelationSettingsWindow);
            //this.SetCommonRelationForUpdatingCommand = new Command<int>(SetCommonRelationForUpdating);
            this.UpdateCommonRelationCommand = new Command(async () => await UpdateCommonRelation());
        }
        #endregion

        #region Command logic methods
        private void GoBack() => App.GetService<AppWindowVM>().OpenDictionariesPageCommand.Execute();
        private async Task CreateCommonRelation()
        {
            string russianUnitValue = this.AddingWindowRussianValue;
            string englishUnitValue = this.AddingWindowEnglishValue;
            UnitType englishUnitType = this.AddingWindowSelectedEnglishUnitType.UnitType;
            UnitType russianUnitType = this.AddingWindowSelectedRussianUnitType.UnitType;
            string? comment = StringHelper.NullIfEmptyOrWhiteSpace(this.AddingWindowCommentValue);
            int commonDictionaryId = this.dictionaryId;
            string? firstExampleRussianValue = null;
            string? firstExampleEnglishValue = null;
            string? secondExampleRussianValue = null;
            string? secondExampleEnglishValue = null;

            // remove 
            bool firstExampleExist = false;
            bool secondExampleExist = false;

            if (this.ExampleViews.Count > 0)
                firstExampleExist = true;
            if (this.ExampleViews.Count > 1)
                secondExampleExist = true;

            if (firstExampleExist)
            {
                firstExampleRussianValue = this.ExampleViews.ToArray()[0].RussianValue;
                firstExampleEnglishValue = this.ExampleViews.ToArray()[0].EnglishValue;
            }
            if (secondExampleExist)
            {
                secondExampleRussianValue = this.ExampleViews.ToArray()[1].RussianValue;
                secondExampleEnglishValue = this.ExampleViews.ToArray()[1].EnglishValue;
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
        private void ClearAddingWindow()
        {
            this.AddingWindowEnglishValue = String.Empty;
            this.AddingWindowRussianValue = String.Empty;
            this.AddingWindowCommentValue = String.Empty;
            this.ExampleRussianValue = String.Empty;
            this.ExampleEnglishValue = String.Empty;
            this.AddingWindowSelectedRussianUnitType = AddingWindowRussianUnitTypes[0];
            this.AddingWindowSelectedEnglishUnitType = AddingWidnowEnglishUnitTypes[0];
            this.ExampleViews.Clear();
            ShowExampleWarningIcon();
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
        private void UpdateConfirmCommonRelationAddingButtonAvailability() => IsConfirmCommonRelationAddingButtonEnabled = ValidationPool.IsValid(ValidationRulesGroup.AddCommonRelation);
        private void CheckCommonRelationForExisting()
        {
            string englishValue = this.AddingWindowEnglishValue;
            string russianValue = this.AddingWindowRussianValue;
            UnitType englishUnitType = this.AddingWindowSelectedEnglishUnitType.UnitType;
            UnitType russianUnitType = this.AddingWindowSelectedRussianUnitType.UnitType;
            this.CommonRelationExist = commonRelationRepository.IsCommonRelationExist(russianValue, russianUnitType, englishValue, englishUnitType, this.dictionaryId);
        }
        private void SearchCommonRelations()
        {
            string searchingString = this.SearchStringValue;
            if (searchingString is null || StringHelper.IsEmptyOrWhiteSpace(searchingString))
            {
                this.CommonRelationViews = new ObservableCollection<UserControl>(allCommonRelations.Select(relation => CommonRelationView.Create(relation)));
                return;
            }
            IEnumerable<CommonRelation> selectedRelations = this.allCommonRelations
                .Where(relation => $"{relation.RussianUnit.Value.Prepare()}{relation.EnglishUnit.Value.Prepare()}".Contains(searchingString.Prepare()));
            IEnumerable<CommonRelationView> commonRelationViews = selectedRelations.Select(selectedRelation => CommonRelationView.Create(selectedRelation));
            this.CommonRelationViews = new ObservableCollection<UserControl>(commonRelationViews);
            AddShadowRelationView();
        }
        private void AddExampleView()
        {
            if (this.exampleInvalid)
                return;
            this.ExampleViews.Add(ExampleView.Create(this.ExampleRussianValue, this.ExampleEnglishValue, ++exampleIdsCount, false));
        }
        private void RemoveExampleView(int exampleId) => this.ExampleViews.Remove(FindExampleView(exampleId));
        private void ClearExampleAddingFields()
        {
            this.ExampleRussianValue = String.Empty;
            this.ExampleEnglishValue = String.Empty;
        }
        private void FocusExampleRussianValueField() => App.GetService<EditCommonDictionaryPage>().exampleRussianValueField.Focus();
        private void ValidateExampleSection()
        {
            bool empty = String.IsNullOrWhiteSpace(this.ExampleEnglishValue) || String.IsNullOrWhiteSpace(this.ExampleRussianValue);

            if (empty)
            {
                this.exampleInvalid = true;
                ShowExampleWarningIcon();
                return;
            }
            this.exampleInvalid = false;
            ShowAddExampleButton();
        }
        private void CheckExampleFieldsMaxLengthVisibility()
        {
            bool inRange = (!String.IsNullOrWhiteSpace(this.ExampleEnglishValue) && this.ExampleEnglishValue.Length > 20)
                        || (!String.IsNullOrWhiteSpace(this.ExampleRussianValue) && this.ExampleRussianValue.Length > 20);
            if (inRange)
                ShowExampleFieldsMaxLength();
            else
                HideExampleFieldsMaxLength();
        }
        private void OpenCommonRelationAddingWindow()
        {
            OpenCommonRelationAddingWindowButtonSoftClick();
        }
        private void OpenCommonRelationSettingsWindow(int commonRelationId)
        {
            CommonRelation commonRelation = commonRelationRepository.GetCommonRelation(commonRelationId);
            this.currentRelationForUpdate = commonRelation;
            this.AddingWindowCommentValue = StringHelper.EmptyIfNull(commonRelation.Comment);
            ExampleViews.Clear();
            if (commonRelation.IsFirstExampleExist)
                this.ExampleViews.Add(ExampleView.Create(commonRelation.FirstExampleRussianValue.EmptyIfNull(), commonRelation.FirstExampleEnglishValue.EmptyIfNull(), ++exampleIdsCount, false));
            if (commonRelation.IsSecondExampleExist)
                this.ExampleViews.Add(ExampleView.Create(commonRelation.SecondExampleRussianValue.EmptyIfNull(), commonRelation.SecondExampleEnglishValue.EmptyIfNull(), ++exampleIdsCount, false));
            OpenCommonRelationSettingsWindowButtonSoftClick();
        }
        private async Task UpdateCommonRelation()
        {
            int id = currentRelationForUpdate.Id;
            string? comment = StringHelper.NullIfEmptyOrWhiteSpace(this.AddingWindowCommentValue);
            int commonDictionaryId = this.dictionaryId;
            string? firstExampleRussianValue = null;
            string? firstExampleEnglishValue = null;
            string? secondExampleRussianValue = null;
            string? secondExampleEnglishValue = null;

            // remove 
            bool firstExampleExist = false;
            bool secondExampleExist = false;

            if (this.ExampleViews.Count > 0)
                firstExampleExist = true;
            if (this.ExampleViews.Count > 1)
                secondExampleExist = true;

            if (firstExampleExist)
            {
                firstExampleRussianValue = this.ExampleViews.ToArray()[0].RussianValue;
                firstExampleEnglishValue = this.ExampleViews.ToArray()[0].EnglishValue;
            }
            if (secondExampleExist)
            {
                secondExampleRussianValue = this.ExampleViews.ToArray()[1].RussianValue;
                secondExampleEnglishValue = this.ExampleViews.ToArray()[1].EnglishValue;
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
        #endregion

        #region Other private members
        private ExampleView FindExampleView(int exampleId) => this.ExampleViews.First(exampleView => exampleView.Id == exampleId);
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
            this.AddingWindowRussianUnitTypes = russianUnitTypes;
            this.AddingWindowSelectedRussianUnitType = selectedRussianUnitType;
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
            this.AddingWidnowEnglishUnitTypes = englishUnitTypes;
            this.AddingWindowSelectedEnglishUnitType = selectedEnglishUnitType;
        }

        #endregion
        private void ShowAddExampleButton()
        {
            this.AddExampleButtonIsVisible = true;
            this.ExamleWarningIconIsVisible = false;
        }
        private void ShowExampleWarningIcon()
        {
            this.AddExampleButtonIsVisible = false;
            this.ExamleWarningIconIsVisible = true;
        }
        private void ShowExampleFieldsMaxLength()
        {
            TextBox exampleRussianValueField = App.GetService<EditVerbPrepositionDictionaryPage>().exampleRussianValueField;
            TextBox exampleEnglishValueField = App.GetService<EditVerbPrepositionDictionaryPage>().exampleEnglishValueField;
            exampleRussianValueField.MaxLength = ModelConstants.ExampleValueMaxLength;
            exampleEnglishValueField.MaxLength = ModelConstants.ExampleValueMaxLength;
            exampleRussianValueField.Margin = new Thickness(0, 0, 7, 7);
            exampleEnglishValueField.Margin = new Thickness(7, 0, 0, 7);
        }
        private void HideExampleFieldsMaxLength()
        {
            TextBox exampleRussianValueField = App.GetService<EditVerbPrepositionDictionaryPage>().exampleRussianValueField;
            TextBox exampleEnglishValueField = App.GetService<EditVerbPrepositionDictionaryPage>().exampleEnglishValueField;
            exampleRussianValueField.MaxLength = 0;
            exampleEnglishValueField.MaxLength = 0;
            exampleRussianValueField.Margin = new Thickness(0, 0, 7, 0);
            exampleEnglishValueField.Margin = new Thickness(7, 0, 0, 0);
        }
        private UnitTypeComboBoxItem FindRussianUnitTypeView(UnitType unitType) => this.AddingWindowRussianUnitTypes.First(unitTypeView => unitTypeView.UnitType == unitType);
        private UnitTypeComboBoxItem FindEnglishUnitTypeView(UnitType unitType) => this.AddingWidnowEnglishUnitTypes.First(unitTypeView => unitTypeView.UnitType == unitType);
        private void FocusRussianValueTextBox() => App.GetService<EditCommonDictionaryPage>().newCommonRelationRussianValueTextBox.Focus();
        private void FocusEnglishValueTextBox() => App.GetService<EditCommonDictionaryPage>().newCommonRelationEnglishValueTextBox.Focus();
        private void FocusRussianUnitTypeComboBox() => App.GetService<EditCommonDictionaryPage>().newCommonRelationRussianUnitTypeComboBox.Focus();
        private void FocusEnglishUnitTypeComboBox() => App.GetService<EditCommonDictionaryPage>().newCommonRelationEnglishUnitTypeComboBox.Focus();
        private void FocusCommentValueTextBox() => App.GetService<EditCommonDictionaryPage>().newCommonRelationCommentValueTextBox.Focus();
        private void FocusExampleEnglishValueField() => App.GetService<EditCommonDictionaryPage>().exampleEnglishValueField.Focus();
        private void AddingNewCommonRelationButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().newCommonRelationAddingButton.SoftClick();
        private void OpenCommonRelationAddingWindowButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().openNewCommonRelationAddingWindowButton.SoftClick();
        private void OpenCommonRelationSettingsWindowButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().openCommonRelationSettingsWindowButton.SoftClick();

        private void NewCommonRelationAddingWindowCancelButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().newCommonRelationAddingWindowCancelButton.SoftClick();
        private void AddExampleButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().addExampleButton.SoftClick();
        private void AddShadowRelationView() => this.CommonRelationViews.Add(ShadowCommonRelationView.Create());
        private void OpenMenuButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().openMenuButton.SoftClick();
        private void CloseMenuButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().closeMenuButton.SoftClick();
    }
}
