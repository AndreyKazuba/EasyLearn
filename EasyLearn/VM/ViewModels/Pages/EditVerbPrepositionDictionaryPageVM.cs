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
using EasyLearn.Data.Helpers;

namespace EasyLearn.VM.ViewModels.Pages
{
    public class EditVerbPrepositionDictionaryPageVM : ViewModel
    {
        #region Repositories
        private readonly IVerbPrepositionRepository verbPrepositionRepository;
        private readonly IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository;
        #endregion

        #region Private fields
        private bool awExampleInvalid = true;
        private bool uwExampleInvalid = true;
        private bool topMenuIsOpened = false;
        private int exampleIdsCount;
        private int dictionaryId;
        private VerbPreposition currentVerbPrepositionForUpdate;
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
        public ObservableCollection<ExampleView> AwExampleViews { get; set; } = new ObservableCollection<ExampleView>();
        public ObservableCollection<ExampleView> UwExampleViews { get; set; } = new ObservableCollection<ExampleView>();
        public string AwVerbValue { get; set; }
        public string AwPrepositionValue { get; set; }
        public string AwTranslationValue { get; set; }
        public string AwExampleRussianValue { get; set; }
        public string AwExampleEnglishValue { get; set; }
        public bool AwConfirmButtonIsEnabled { get; set; }
        public bool AwAddExampleButtonIsEnabled { get; set; }
        public string UwTranslationValue { get; set; }
        public string UwExampleRussianValue { get; set; }
        public string UwExampleEnglishValue { get; set; }
        public bool UwConfirmButtonIsEnabled { get; set; }
        public bool UwAddExampleButtonIsEnabled { get; set; }
        #endregion

        #region Events
        protected override void InitEvents()
        {
            EditVerbPrepositionDictionaryPage.VerbValueTextBoxEnterDown += OnVerbValueTextBoxEnterDown;
            EditVerbPrepositionDictionaryPage.PrepositionValueTextBoxEnterDown += OnPrepositionValueTextBoxEnterDown;
            EditVerbPrepositionDictionaryPage.TranslationValueTextBoxEnterDown += OnTranslationValueTextBoxEnterDown;
            EditVerbPrepositionDictionaryPage.AddingWindowExampleRussianValueTextBoxEnterDown += OnAddingWindowExampleRussianValueTextBoxEnterDown;
            EditVerbPrepositionDictionaryPage.AddingWindowExampleEnglishValueTextBoxEnterDown += OnAddingWindowExampleEnglishValueTextBoxEnterDown;
            EditVerbPrepositionDictionaryPage.UpdateWindowExampleRussianValueTextBoxEnterDown += OnUpdateWindowExampleRussianValueTextBoxEnterDown;
            EditVerbPrepositionDictionaryPage.UpdateWindowExampleEnglishValueTextBoxEnterDown += OnUpdateWindowExampleEnglishValueTextBoxEnterDown;
            AppWindow.WindowCtrlNDown += OnWindowCtrlNDown;
            AppWindow.WindowEscDown += OnWindowEscDown;
            AppWindow.GoBackButtonClick += OnGoBackButtonClick;
            AppWindow.DrawerButtonClick += OnDrawerButtonClick;
            AppWindow.OpenMenuButtonClick += OnOpenMenuButtonClick;
        }
        private void OnVerbValueTextBoxEnterDown() => FocusPrepositionValueTextBox();
        private void OnPrepositionValueTextBoxEnterDown() => FocusTranslationValueTextBox();
        private void OnTranslationValueTextBoxEnterDown()
        {
            if (ValidationPool.IsValid(ValidationRulesGroup.AddVerbPreposition))
                AddingNewVerbPrepositionButtonSoftClick();
        }
        private void OnUpdateWindowExampleRussianValueTextBoxEnterDown() => UwFocusExampleEnglishValueField();
        private void OnUpdateWindowExampleEnglishValueTextBoxEnterDown()
        {
            if (this.uwExampleInvalid)
                return;
            UwAddExampleButtonSoftClick();
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
        private void OnAddingWindowExampleRussianValueTextBoxEnterDown() => FocusExampleEnglishValueField();
        private void OnAddingWindowExampleEnglishValueTextBoxEnterDown()
        {
            if (this.awExampleInvalid)
                return;
            AddExampleButtonSoftClick();
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
        #endregion

        #region Commands 
        public Command GoBackCommand { get; private set; }
        public Command CreateVerbPrepositionCommand { get; private set; }
        public Command<int> SetDictionaryAsCurrentCommand { get; private set; }
        public Command<int> RemoveExampleViewCommand { get; private set; }
        public Command CheckVerbPrepositionForExistingCommand { get; private set; }
        public Command AwUpdateConfirmButtonAvailabilityCommand { get; private set; }
        public Command AwClearCommand { get; private set; }
        public Command AwOpenWindowCommand { get; private set; }
        public Command AwAddExampleViewCommand { get; private set; }
        public Command AwClearExampleSectionCommand { get; private set; }
        public Command AwFocusExampleRussianValueFieldCommand { get; private set; }
        public Command AwValidateExampleSectionCommand { get; private set; }
        public Command AwCheckExampleFieldsMaxLengthVisibilityCommand { get; private set; }
        public Command<int> UwOpenWindowCommand { get; private set; }
        public Command UwAddExampleViewCommand { get; private set; }
        public Command UwClearCommand { get; private set; }
        public Command UwUpdateConfirmButtonAvailabilityCommand { get; private set; }
        public Command UwClearExampleAddingSectionCommand { get; private set; }
        public Command UwFocusExampleRussianValueFieldCommand { get; private set; }
        public Command UwValidateExampleSectionCommand { get; private set; }
        public Command UwCheckExampleFieldsMaxLengthVisibilityCommand { get; private set; }
        public Command UpdateVerbPrepositionCommand { get; private set; }
        protected override void InitCommands()
        {
            this.GoBackCommand = new Command(GoBack);
            this.CreateVerbPrepositionCommand = new Command(async () => await CreateVerbPreposition());
            this.AwClearCommand = new Command(AwClear);
            this.SetDictionaryAsCurrentCommand = new Command<int>(async verbPrepositionDictionaryId => await SetDictionaryAsCurrent(verbPrepositionDictionaryId));
            this.CheckVerbPrepositionForExistingCommand = new Command(CheckVerbPrepositionForExisting);
            this.AwUpdateConfirmButtonAvailabilityCommand = new Command(AwUpdateConfirmButtonAvailability);
            this.AwOpenWindowCommand = new Command(AwOpenWindow);
            this.AwAddExampleViewCommand = new Command(AwAddExampleView);
            this.RemoveExampleViewCommand = new Command<int>(RemoveExampleView);
            this.AwClearExampleSectionCommand = new Command(AwClearExampleSection);
            this.AwFocusExampleRussianValueFieldCommand = new Command(AwFocusExampleRussianValueField);
            this.AwValidateExampleSectionCommand = new Command(AwValidateExampleSection);
            this.AwCheckExampleFieldsMaxLengthVisibilityCommand = new Command(AwCheckExampleFieldsMaxLengthVisibility);
            this.UwOpenWindowCommand = new Command<int>(UwOpenWindow);
            this.UpdateVerbPrepositionCommand = new Command(async () => await UpdateVerbPreposition());
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
        private async Task CreateVerbPreposition()
        {
            string prepositionValue = this.AwPrepositionValue;
            string verbValue = this.AwVerbValue;
            string translation = this.AwTranslationValue;
            int verbPrepositionDictionaryId = this.dictionaryId;
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
        private void AwClear()
        {
            this.AwVerbValue = String.Empty;
            this.AwPrepositionValue = String.Empty;
            this.AwTranslationValue = String.Empty;
            this.AwExampleViews.Clear();
            this.AwAddExampleButtonIsEnabled = false;
        }
        private async Task SetDictionaryAsCurrent(int verbPrepositionDictionaryId)
        {
            VerbPrepositionDictionnary verbPrepositionDictionary = await verbPrepositionDictionaryRepository.GetVerbPrepositionDictionaryAsync(verbPrepositionDictionaryId);
            this.dictionaryId = verbPrepositionDictionaryId;
            IEnumerable<VerbPrepositionView> verbPrepositionViews = verbPrepositionDictionary.VerbPrepositions
                .Select(verbPreposition => VerbPrepositionView.Create(verbPreposition))
                .OrderBy(verbPrepositiinView => verbPrepositiinView.Order);
            this.VerbPrepositionViews = new ObservableCollection<UserControl>();
            AddShadowVerbPreposition();
            foreach (VerbPrepositionView verbPrepositionView in verbPrepositionViews)
                this.VerbPrepositionViews.Add(verbPrepositionView);
        }
        private void CheckVerbPrepositionForExisting()
        {
            string verbValue = this.AwVerbValue;
            string prepositionValue = this.AwPrepositionValue;
            this.VerbPrepositionExist = this.verbPrepositionRepository.IsVerbPrepositionExist(verbValue, prepositionValue, dictionaryId);
        }
        private void AwUpdateConfirmButtonAvailability() => AwConfirmButtonIsEnabled = ValidationPool.IsValid(ValidationRulesGroup.AddVerbPreposition);
        private void AwOpenWindow() => OpenNewVerbPrepositionAddingWindowButtonSoftClick();
        private void AwAddExampleView()
        {
            if (this.awExampleInvalid)
                return;
            this.AwExampleViews.Add(ExampleView.Create(this.AwExampleRussianValue, this.AwExampleEnglishValue, ++exampleIdsCount, true));
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
        private void AwClearExampleSection()
        {
            this.AwExampleRussianValue = String.Empty;
            this.AwExampleEnglishValue = String.Empty;
        }
        private void AwFocusExampleRussianValueField() => App.GetService<EditVerbPrepositionDictionaryPage>().awExampleRussianValueField.Focus();
        private void UwFocusExampleRussianValueField() => App.GetService<EditVerbPrepositionDictionaryPage>().uwExampleRussianValueField.Focus();
        private void AwValidateExampleSection()
        {
            bool empty = String.IsNullOrWhiteSpace(this.AwExampleEnglishValue) || String.IsNullOrWhiteSpace(this.AwExampleRussianValue);

            if (empty)
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
        private void UwOpenWindow(int verbPrepositionId)
        {
            VerbPreposition verbPreposition = verbPrepositionRepository.GetVerbPreposition(verbPrepositionId);
            this.currentVerbPrepositionForUpdate = verbPreposition;
            this.UwTranslationValue = StringHelper.EmptyIfNull(verbPreposition.Translation);
            UwExampleViews.Clear();
            if (verbPreposition.IsFirstExampleExist)
                this.UwExampleViews.Add(ExampleView.Create(verbPreposition.FirstExampleRussianValue.EmptyIfNull(), verbPreposition.FirstExampleEnglishValue.EmptyIfNull(), ++exampleIdsCount, true));
            if (verbPreposition.IsSecondExampleExist)
                this.UwExampleViews.Add(ExampleView.Create(verbPreposition.SecondExampleRussianValue.EmptyIfNull(), verbPreposition.SecondExampleEnglishValue.EmptyIfNull(), ++exampleIdsCount, true));
            OpenVerbPrepositionSettingsWindowButtonSoftClick();
        }
        private void UwAddExampleView()
        {
            if (this.uwExampleInvalid || this.UwExampleViews.Count >= ModelConstants.MaxExamplesCount)
                return;
            this.UwExampleViews.Add(ExampleView.Create(this.UwExampleRussianValue, this.UwExampleEnglishValue, ++exampleIdsCount, false));
        }
        private void UwClear()
        {
            this.UwTranslationValue = String.Empty;
            this.UwExampleRussianValue = String.Empty;
            this.UwExampleEnglishValue = String.Empty;
            this.UwExampleViews.Clear();
            this.UwAddExampleButtonIsEnabled = false;
        }
        private void UwUpdateConfirmButtonAvailability() => UwConfirmButtonIsEnabled = ValidationPool.IsValid(ValidationRulesGroup.UpdateVerbPrepsotion);
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
        private async Task UpdateVerbPreposition()
        {
            int id = currentVerbPrepositionForUpdate.Id;
            string translation = this.UwTranslationValue;
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

            VerbPreposition updatedVerbPreposition = await verbPrepositionRepository.UpdateVerbPreposition(
                id,
                translation,
                firstExampleRussianValue,
                firstExampleEnglishValue,
                secondExampleRussianValue,
                secondExampleEnglishValue);
            UpdateVerbPrepositionOnUI(updatedVerbPreposition);
        }
        #endregion

        #region Other private methods
        private void AwShowExampleFieldsMaxLength()
        {
            TextBox awExampleRussianValueField = App.GetService<EditVerbPrepositionDictionaryPage>().awExampleRussianValueField;
            TextBox awExampleEnglishValueField = App.GetService<EditVerbPrepositionDictionaryPage>().awExampleEnglishValueField;
            awExampleRussianValueField.MaxLength = ModelConstants.ExampleValueMaxLength;
            awExampleEnglishValueField.MaxLength = ModelConstants.ExampleValueMaxLength;
            awExampleRussianValueField.Margin = new Thickness(0, 0, 7, 7);
            awExampleEnglishValueField.Margin = new Thickness(7, 0, 0, 7);
        }
        private void AwHideExampleFieldsMaxLength()
        {
            TextBox awExampleRussianValueField = App.GetService<EditVerbPrepositionDictionaryPage>().awExampleRussianValueField;
            TextBox awExampleEnglishValueField = App.GetService<EditVerbPrepositionDictionaryPage>().awExampleEnglishValueField;
            awExampleRussianValueField.MaxLength = 0;
            awExampleEnglishValueField.MaxLength = 0;
            awExampleRussianValueField.Margin = new Thickness(0, 0, 7, 0);
            awExampleEnglishValueField.Margin = new Thickness(7, 0, 0, 0);
        }
        private void UwShowExampleFieldsMaxLength()
        {
            TextBox awExampleRussianValueField = App.GetService<EditVerbPrepositionDictionaryPage>().uwExampleRussianValueField;
            TextBox awExampleEnglishValueField = App.GetService<EditVerbPrepositionDictionaryPage>().uwExampleEnglishValueField;
            awExampleRussianValueField.MaxLength = ModelConstants.ExampleValueMaxLength;
            awExampleEnglishValueField.MaxLength = ModelConstants.ExampleValueMaxLength;
            awExampleRussianValueField.Margin = new Thickness(0, 0, 7, 7);
            awExampleEnglishValueField.Margin = new Thickness(7, 0, 0, 7);
        }
        private void UwHideExampleFieldsMaxLength()
        {
            TextBox awExampleRussianValueField = App.GetService<EditVerbPrepositionDictionaryPage>().uwExampleRussianValueField;
            TextBox awExampleEnglishValueField = App.GetService<EditVerbPrepositionDictionaryPage>().uwExampleEnglishValueField;
            awExampleRussianValueField.MaxLength = 0;
            awExampleEnglishValueField.MaxLength = 0;
            awExampleRussianValueField.Margin = new Thickness(0, 0, 7, 0);
            awExampleEnglishValueField.Margin = new Thickness(7, 0, 0, 0);
        }
        //private ExampleView FindExampleView(int exampleId) => this.AwExampleViews.First(exampleView => exampleView.Id == exampleId);
        private ExampleView? AwTryFindExampleView(int exampleId) => this.AwExampleViews.FirstOrDefault(exampleView => exampleView.Id == exampleId);
        private ExampleView? UwTryFindExampleView(int exampleId) => this.UwExampleViews.FirstOrDefault(exampleView => exampleView.Id == exampleId);
        private void AddShadowVerbPreposition() => this.VerbPrepositionViews.Add(ShadowVerbPrepositionView.Create());
        private void AddVerbPrepositionToUI(VerbPreposition verbPreposition) => this.VerbPrepositionViews.Add(VerbPrepositionView.Create(verbPreposition));
        private void UpdateVerbPrepositionOnUI(VerbPreposition updatedVerbPreposition)
        {
            VerbPrepositionView verbPrepositionView = FindVerbPrepositionView(updatedVerbPreposition.Id);
            verbPrepositionView.UpdateView(updatedVerbPreposition);
        }
        private VerbPrepositionView FindVerbPrepositionView(int verbPrepositionId) => (VerbPrepositionView)this.VerbPrepositionViews.First(verbPrepositionView =>
        {
            VerbPrepositionView? vpView = verbPrepositionView as VerbPrepositionView;
            if (vpView is null)
                return false;
            else
                return ((VerbPrepositionView)verbPrepositionView).Id == verbPrepositionId;
        });
        private void FocusVerbValueTextBox() => App.GetService<EditVerbPrepositionDictionaryPage>().newVerbPrepositionVerbValueTextBox.Focus();
        private void FocusPrepositionValueTextBox() => App.GetService<EditVerbPrepositionDictionaryPage>().newVerbPrepositionPrepositionValueTextBox.Focus();
        private void FocusTranslationValueTextBox() => App.GetService<EditVerbPrepositionDictionaryPage>().newVerbPrepositionTranslationValueTextBox.Focus();
        private void AddingNewVerbPrepositionButtonSoftClick() => App.GetService<EditVerbPrepositionDictionaryPage>().newVerbPrepositionAddingButton.SoftClick();
        private void OpenNewVerbPrepositionAddingWindowButtonSoftClick() => App.GetService<EditVerbPrepositionDictionaryPage>().openNewVerbPrepositionAddingWindowButton.SoftClick();
        private void FocusExampleEnglishValueField() => App.GetService<EditVerbPrepositionDictionaryPage>().awExampleEnglishValueField.Focus();
        private void AddExampleButtonSoftClick() => App.GetService<EditVerbPrepositionDictionaryPage>().addExampleButton.SoftClick();
        private void NewVerbPrepositionAddingWindowCancelButtonSoftClick() => App.GetService<EditVerbPrepositionDictionaryPage>().newVerbPrepositionAddingWindowCancelButton.SoftClick();
        private void OpenMenuButtonSoftClick() => App.GetService<EditVerbPrepositionDictionaryPage>().openMenuButton.SoftClick();
        private void CloseMenuButtonSoftClick() => App.GetService<EditVerbPrepositionDictionaryPage>().closeMenuButton.SoftClick();
        private void OpenVerbPrepositionSettingsWindowButtonSoftClick() => App.GetService<EditVerbPrepositionDictionaryPage>().openVerbPrepositionSettingsWindowButton.SoftClick();
        private void UwFocusExampleEnglishValueField() => App.GetService<EditVerbPrepositionDictionaryPage>().uwExampleEnglishValueField.Focus();
        private void UwAddExampleButtonSoftClick() => App.GetService<EditVerbPrepositionDictionaryPage>().uwAddExampleButton.SoftClick();
        #endregion
    }
}
