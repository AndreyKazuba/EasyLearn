using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.UI.CustomControls;
using EasyLearn.VM.Core;
using EasyLearn.VM.Windows;
using EasyLearn.Infrastructure.Validation;
using EasyLearn.UI.Pages;
using EasyLearn.Infrastructure.Helpers;
using EasyLearn.UI;
using System.Windows.Controls;
using Page = EasyLearn.Infrastructure.Enums.Page;
using EasyLearn.Data.Constants;
using System.Windows;

namespace EasyLearn.VM.ViewModels.Pages
{
    public class EditVerbPrepositionDictionaryPageVM : ViewModel
    {
        #region Repositories
        private readonly IVerbPrepositionRepository verbPrepositionRepository;
        private readonly IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository;
        #endregion

        #region Private fields
        private bool exampleInvalid = true;
        private int exampleIdsCount;
        private int dictionaryId;
        private Guid verbPrepositionExistValidationRuleId;
        #endregion

        private bool VerbPrepositionExist
        {
            set
            {
                ValidationPool.Set(ValidationRulesGroup.AddVerbPreposition, verbPrepositionExistValidationRuleId, !value);
            }
        }

#pragma warning disable CS8618
        public EditVerbPrepositionDictionaryPageVM(IVerbPrepositionRepository verbPrepositionRepository, IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository)
        {
            this.verbPrepositionRepository = verbPrepositionRepository;
            this.verbPrepositionDictionaryRepository = verbPrepositionDictionaryRepository;
            this.verbPrepositionExistValidationRuleId = ValidationPool.Register(ValidationRulesGroup.AddVerbPreposition);
        }
#pragma warning restore CS8618

        #region Props for binding
        public ObservableCollection<UserControl> VerbPrepositionViews { get; set; }
        public ObservableCollection<ExampleView> ExampleViews { get; set; } = new ObservableCollection<ExampleView>();
        public string AddingWindowVerbValue { get; set; }
        public string AddingWindowPrepositionValue { get; set; }
        public string AddingWindowTranslationValue { get; set; }
        public bool IsConfirmVerbPrepositionAddingButtonEnabled { get; set; }
        public string ExampleRussianValue { get; set; }
        public string ExampleEnglishValue { get; set; }
        public bool IsConfirmCommonRelationAddingButtonEnabled { get; set; }
        public bool CommonRelationHasExistLableIsVisible { get; set; }
        public bool AddExampleButtonIsVisible { get; set; }
        public bool ExamleWarningIconIsVisible { get; set; }
        #endregion

        #region Events
        protected override void InitEvents()
        {
            EditVerbPrepositionDictionaryPage.VerbValueTextBoxEnterDown += OnVerbValueTextBoxEnterDown;
            EditVerbPrepositionDictionaryPage.PrepositionValueTextBoxEnterDown += OnPrepositionValueTextBoxEnterDown;
            EditVerbPrepositionDictionaryPage.TranslationValueTextBoxEnterDown += OnTranslationValueTextBoxEnterDown;
            AppWindow.WindowCtrlNDown += OnWindowCtrlNDown;
            AppWindow.WindowEscDown += OnWindowEscDown;
            AppWindow.GoBackButtonClick += OnGoBackButtonClick;
            AppWindow.DrawerButtonClick += OnDrawerButtonClick;
        }
        private void OnVerbValueTextBoxEnterDown() => FocusPrepositionValueTextBox();
        private void OnPrepositionValueTextBoxEnterDown() => FocusTranslationValueTextBox();
        private void OnTranslationValueTextBoxEnterDown()
        {
            if (ValidationPool.IsValid(ValidationRulesGroup.AddVerbPreposition))
                AddingNewVerbPrepositionButtonSoftClick();
        }
        private void OnWindowCtrlNDown()
        {
            if (App.GetService<AppWindowVM>().CurrentPage == Page.EditVerbPrepositionListPage)
            {
                FocusVerbValueTextBox();
                OpenNewVerbPrepositionAddingWindowButtonSoftClick();
            }
        }
        private void OnWindowEscDown()
        {
            if (App.GetService<AppWindowVM>().CurrentPage == Page.EditVerbPrepositionListPage)
                NewVerbPrepositionAddingWindowCancelButtonSoftClick();
        }
        private void OnGoBackButtonClick()
        {
            App.GetService<AppWindowVM>().HideGoBackButtonCommand.Execute();
            GoBack();
        }
        private void OnDrawerButtonClick() => App.GetService<AppWindowVM>().HideGoBackButtonCommand.Execute();
        #endregion

        #region Commands 
        public Command GoBackCommand { get; private set; }
        public Command CreateVerbPrepositionCommand { get; private set; }
        public Command ClearAddingWindowCommand { get; private set; }
        public Command<int> SetDictionaryAsCurrentCommand { get; private set; }
        public Command CheckVerbPrepositionForExistingCommand { get; private set; }
        public Command UpdateConfirmVerbPrepositionAddingButtonAvailabilityCommand { get; private set; }
        public Command OpenNewVerbPrepositionAddingWindowCommand { get; private set; }
        public Command AddExampleViewCommand { get; private set; }
        public Command<int> RemoveExampleViewCommand { get; private set; }
        public Command ClearExampleAddingFieldsCommand { get; private set; }
        public Command FocusExampleRussianValueFieldCommand { get; private set; }
        public Command ValidateExampleSectionCommand { get; private set; }
        public Command CheckExampleFieldsMaxLengthVisibilityCommand { get; private set; }
        protected override void InitCommands()
        {
            this.GoBackCommand = new Command(GoBack);
            this.CreateVerbPrepositionCommand = new Command(async () => await CreateVerbPreposition());
            this.ClearAddingWindowCommand = new Command(ClearAddingWindow);
            this.SetDictionaryAsCurrentCommand = new Command<int>(async verbPrepositionDictionaryId => await SetDictionaryAsCurrent(verbPrepositionDictionaryId));
            this.CheckVerbPrepositionForExistingCommand = new Command(CheckVerbPrepositionForExisting);
            this.UpdateConfirmVerbPrepositionAddingButtonAvailabilityCommand = new Command(UpdateConfirmVerbPrepositionAddingButtonAvailability);
            this.OpenNewVerbPrepositionAddingWindowCommand = new Command(OpenNewVerbPrepositionAddingWindow);
            this.AddExampleViewCommand = new Command(AddExampleView);
            this.RemoveExampleViewCommand = new Command<int>(exampleId => RemoveExampleView(exampleId));
            this.ClearExampleAddingFieldsCommand = new Command(ClearExampleAddingFields);
            this.FocusExampleRussianValueFieldCommand = new Command(FocusExampleRussianValueField);
            this.ValidateExampleSectionCommand = new Command(ValidateExampleSection);
            this.CheckExampleFieldsMaxLengthVisibilityCommand = new Command(CheckExampleFieldsMaxLengthVisibility);
        }
        #endregion

        #region Command logic methods
        private void GoBack() => App.GetService<AppWindowVM>().OpenDictionariesPageCommand.Execute();
        private async Task CreateVerbPreposition()
        {
            string prepositionValue = this.AddingWindowPrepositionValue;
            string verbValue = this.AddingWindowVerbValue;
            string translation = this.AddingWindowTranslationValue;
            int verbPrepositionDictionaryId = this.dictionaryId;
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
            VerbPreposition newVerbPreposition = await verbPrepositionRepository.CreateVerbPreposition(
                verbValue,
                prepositionValue,
                verbPrepositionDictionaryId,
                translation,
                firstExampleRussianValue,
                firstExampleEnglishValue, 
                secondExampleRussianValue,
                secondExampleEnglishValue);
            AddVerbPrepositionToUI(newVerbPreposition);
        }
        private void ClearAddingWindow()
        {
            this.AddingWindowVerbValue = String.Empty;
            this.AddingWindowPrepositionValue = String.Empty;
            this.AddingWindowTranslationValue = String.Empty;
        }
        private async Task SetDictionaryAsCurrent(int verbPrepositionDictionaryId)
        {
            VerbPrepositionDictionnary verbPrepositionDictionary = await verbPrepositionDictionaryRepository.GetVerbPrepositionDictionaryAsync(verbPrepositionDictionaryId);
            this.dictionaryId = verbPrepositionDictionaryId;
            IEnumerable<VerbPrepositionView> verbPrepositionViews = verbPrepositionDictionary.VerbPrepositions.Select(verbPreposition => VerbPrepositionView.Create(verbPreposition));
            this.VerbPrepositionViews = new ObservableCollection<UserControl>();
            AddShadowVerbPreposition();
            foreach (VerbPrepositionView verbPrepositionView in verbPrepositionViews)
                this.VerbPrepositionViews.Add(verbPrepositionView);
            
        }
        private void CheckVerbPrepositionForExisting()
        {
            string verbValue = this.AddingWindowVerbValue;
            string prepositionValue = this.AddingWindowPrepositionValue;
            this.VerbPrepositionExist = this.verbPrepositionRepository.IsVerbPrepositionExist(verbValue, prepositionValue, dictionaryId);
        }
        private void UpdateConfirmVerbPrepositionAddingButtonAvailability() => IsConfirmVerbPrepositionAddingButtonEnabled = ValidationPool.IsValid(ValidationRulesGroup.AddVerbPreposition);
        private void OpenNewVerbPrepositionAddingWindow() => OpenNewVerbPrepositionAddingWindowButtonSoftClick();
        private void AddExampleView()
        {
            if (this.exampleInvalid)
                return;
            this.ExampleViews.Add(ExampleView.Create(this.ExampleRussianValue, this.ExampleEnglishValue, ++exampleIdsCount));
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
        #endregion

        #region Other private methods
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
            TextBox exampleRussianValueField = App.GetService<EditCommonDictionaryPage>().exampleRussianValueField;
            TextBox exampleEnglishValueField = App.GetService<EditCommonDictionaryPage>().exampleEnglishValueField;
            exampleRussianValueField.MaxLength = ModelConstants.ExampleValueMaxLength;
            exampleEnglishValueField.MaxLength = ModelConstants.ExampleValueMaxLength;
            exampleRussianValueField.Margin = new Thickness(0, 0, 7, 7);
            exampleEnglishValueField.Margin = new Thickness(7, 0, 0, 7);
        }
        private void HideExampleFieldsMaxLength()
        {
            TextBox exampleRussianValueField = App.GetService<EditCommonDictionaryPage>().exampleRussianValueField;
            TextBox exampleEnglishValueField = App.GetService<EditCommonDictionaryPage>().exampleEnglishValueField;
            exampleRussianValueField.MaxLength = 0;
            exampleEnglishValueField.MaxLength = 0;
            exampleRussianValueField.Margin = new Thickness(0, 0, 7, 0);
            exampleEnglishValueField.Margin = new Thickness(7, 0, 0, 0);
        }
        private ExampleView FindExampleView(int exampleId) => this.ExampleViews.First(exampleView => exampleView.Id == exampleId);
        private void AddShadowVerbPreposition() => this.VerbPrepositionViews.Add(ShadowVerbPrepositionView.Create());
        private void AddVerbPrepositionToUI(VerbPreposition verbPreposition) => this.VerbPrepositionViews.Add(VerbPrepositionView.Create(verbPreposition));
        private void FocusVerbValueTextBox() => App.GetService<EditVerbPrepositionDictionaryPage>().newVerbPrepositionVerbValueTextBox.Focus();
        private void FocusPrepositionValueTextBox() => App.GetService<EditVerbPrepositionDictionaryPage>().newVerbPrepositionPrepositionValueTextBox.Focus();
        private void FocusTranslationValueTextBox() => App.GetService<EditVerbPrepositionDictionaryPage>().newVerbPrepositionTranslationValueTextBox.Focus();
        private void AddingNewVerbPrepositionButtonSoftClick() => App.GetService<EditVerbPrepositionDictionaryPage>().newVerbPrepositionAddingButton.SoftClick();
        private void OpenNewVerbPrepositionAddingWindowButtonSoftClick() => App.GetService<EditVerbPrepositionDictionaryPage>().openNewVerbPrepositionAddingWindowButton.SoftClick();
        private void NewVerbPrepositionAddingWindowCancelButtonSoftClick() => App.GetService<EditVerbPrepositionDictionaryPage>().newVerbPrepositionAddingWindowCancelButton.SoftClick();
        #endregion
    }
}
