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
        private bool awExampleIsInvalid = true;
        private bool uwExampleIsInvalid = true;
        private bool topMenuIsOpened = false;
        private int exampleIdCounter;
        private int pageCurrentDictionaryId;
        private VerbPreposition currentVerbPrepositionForUpdate;
        private Guid verbPrepositionExistValidationRuleId;
        #endregion

        #region Private helper props
        private bool VerbPrepositionExist
        {
            set => ValidationPool.Set(ValidationRulesGroup.AddVerbPreposition, verbPrepositionExistValidationRuleId, !value);
        }
        #endregion

        #region Binding props
        public ObservableCollection<UserControl> VerbPrepositionViews { get; set; } = new ObservableCollection<UserControl>();
        public ObservableCollection<ExampleView> AwExampleViews { get; set; } = new ObservableCollection<ExampleView>();
        public ObservableCollection<ExampleView> UwExampleViews { get; set; } = new ObservableCollection<ExampleView>();
        public string SearchStringValue { get; set; }
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
        public bool UwConfirmButtonIsEnabled { get; set; } = true;
        public bool UwAddExampleButtonIsEnabled { get; set; }
        #endregion

#pragma warning disable CS8618
        public EditVerbPrepositionDictionaryPageVM(IVerbPrepositionRepository verbPrepositionRepository, IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository)
        {
            this.verbPrepositionRepository = verbPrepositionRepository;
            this.verbPrepositionDictionaryRepository = verbPrepositionDictionaryRepository;
            this.verbPrepositionExistValidationRuleId = ValidationPool.Register(ValidationRulesGroup.AddVerbPreposition);
        }
#pragma warning restore CS8618

        #region Commands 
        public Command GoBackCommand { get; private set; }
        public Command<int> SetDictionaryCommand { get; private set; }
        public Command CreateVerbPrepositionCommand { get; private set; }
        public Command UpdateVerbPrepositionCommand { get; private set; }
        public Command DeleteVerbPrepositionCommand { get; private set; }
        public Command DeleteAllVerbPrepositionsCommand { get; private set; }
        public Command<int> RemoveExampleViewCommand { get; private set; }
        public Command SearchVerbPrepositionsCommand { get; private set; }

        public Command AwOpenWindowCommand { get; private set; }
        public Command AwClearCommand { get; private set; }
        public Command AwUpdateConfirmButtonAvailabilityCommand { get; private set; }
        public Command AwCheckVerbPrepositionForExistingCommand { get; private set; }
        public Command AwAddExampleViewCommand { get; private set; }
        public Command AwClearExampleSectionCommand { get; private set; }
        public Command AwValidateExampleSectionCommand { get; private set; }
        public Command AwFocusExampleRussianValueTextBoxCommand { get; private set; }
        public Command AwCheckExampleTextBoxesMaxLengthVisibilityCommand { get; private set; }

        public Command<int> UwOpenWindowCommand { get; private set; }
        public Command UwClearCommand { get; private set; }
        public Command UwUpdateConfirmButtonAvailabilityCommand { get; private set; }
        public Command UwAddExampleViewCommand { get; private set; }
        public Command UwClearExampleSectionCommand { get; private set; }
        public Command UwValidateExampleSectionCommand { get; private set; }
        public Command UwFocusExampleRussianValueTextBoxCommand { get; private set; }
        public Command UwCheckExampleTextBoxesMaxLengthVisibilityCommand { get; private set; }

        protected override void InitCommands()
        {
            GoBackCommand = new Command(GoBack);
            SetDictionaryCommand = new Command<int>(async verbPrepositionDictionaryId => await SetDictionary(verbPrepositionDictionaryId));
            CreateVerbPrepositionCommand = new Command(async () => await CreateVerbPreposition());
            UpdateVerbPrepositionCommand = new Command(async () => await UpdateVerbPreposition());
            DeleteVerbPrepositionCommand = new Command(async () => await DeleteVerbPreposition());
            DeleteAllVerbPrepositionsCommand = new Command(async () => await DeleteAllVerbPrepositions());
            RemoveExampleViewCommand = new Command<int>(RemoveExampleView);
            SearchVerbPrepositionsCommand = new Command(SearchVerbPrepositions);

            AwOpenWindowCommand = new Command(AwOpenWindow);
            AwClearCommand = new Command(AwClear);
            AwUpdateConfirmButtonAvailabilityCommand = new Command(AwUpdateConfirmButtonAvailability);
            AwCheckVerbPrepositionForExistingCommand = new Command(AwCheckVerbPrepositionForExisting);
            AwAddExampleViewCommand = new Command(AwAddExampleView);
            AwClearExampleSectionCommand = new Command(AwClearExampleSection);
            AwValidateExampleSectionCommand = new Command(AwValidateExampleSection);
            AwCheckExampleTextBoxesMaxLengthVisibilityCommand = new Command(AwCheckExampleTextBoxesMaxLengthVisibility);
            AwFocusExampleRussianValueTextBoxCommand = new Command(AwFocusExampleRussianValueTextBox);

            UwOpenWindowCommand = new Command<int>(UwOpenWindow);
            UwClearCommand = new Command(UwClear);
            UwUpdateConfirmButtonAvailabilityCommand = new Command(UwUpdateConfirmButtonAvailability);
            UwAddExampleViewCommand = new Command(UwAddExampleView);
            UwClearExampleSectionCommand = new Command(UwClearExampleSection);
            UwValidateExampleSectionCommand = new Command(UwValidateExampleSection);
            UwCheckExampleTextBoxesMaxLengthVisibilityCommand = new Command(UwCheckExampleTextBoxesMaxLengthVisibility);
            UwFocusExampleRussianValueTextBoxCommand = new Command(UwFocusExampleRussianValueTextBox);
        }
        private void GoBack() => App.GetService<AppWindowVM>().OpenDictionariesPageCommand.Execute();
        private async Task SetDictionary(int verbPrepositionDictionaryId)
        {
            VerbPrepositionDictionnary verbPrepositionDictionary = await verbPrepositionDictionaryRepository.GetVerbPrepositionDictionaryAsync(verbPrepositionDictionaryId);
            pageCurrentDictionaryId = verbPrepositionDictionaryId;
            VerbPrepositionViews.Clear();
            AddShadowVerbPrepositionViewToUI();
            AddVerbPrepositionViewsToUIKeepingOrder(CreateOrderedVerbPrepositionViews(verbPrepositionDictionary.VerbPrepositions));
        }
        private async Task CreateVerbPreposition()
        {
            string prepositionValue = AwPrepositionValue;
            string verbValue = AwVerbValue;
            string translation = AwTranslationValue;
            string? firstExampleRussianValue = AwGetFirstExampleRussianValue();
            string? firstExampleEnglishValue = AwGetFirstExampleEnglishValue();
            string? secondExampleRussianValue = AwGetSecondExampleRussianValue();
            string? secondExampleEnglishValue = AwGetSecondExampleEnglishValue();
            int verbPrepositionDictionaryId = pageCurrentDictionaryId;
            VerbPreposition newVerbPreposition = await verbPrepositionRepository.CreateVerbPreposition(
                verbValue,
                prepositionValue,
                verbPrepositionDictionaryId,
                translation,
                firstExampleRussianValue,
                firstExampleEnglishValue,
                secondExampleRussianValue,
                secondExampleEnglishValue);
            AddVerbPrepositionViewToUI(newVerbPreposition);
        }
        private async Task UpdateVerbPreposition()
        {
            int verbPrepositionId = currentVerbPrepositionForUpdate.Id;
            string translation = UwTranslationValue;
            string? firstExampleRussianValue = UwGetFirstExampleRussianValue();
            string? firstExampleEnglishValue = UwGetFirstExampleEnglishValue();
            string? secondExampleRussianValue = UwGetSecondExampleRussianValue();
            string? secondExampleEnglishValue = UwGetSecondExampleEnglishValue();
            VerbPreposition updatedVerbPreposition = await verbPrepositionRepository.UpdateVerbPreposition(
                verbPrepositionId,
                translation,
                firstExampleRussianValue,
                firstExampleEnglishValue,
                secondExampleRussianValue,
                secondExampleEnglishValue);
            UpdateVerbPrepositionViewOnUI(updatedVerbPreposition);
        }
        private async Task DeleteVerbPreposition()
        {
            VerbPrepositionView verbPrepositionView = FindVerbPrepositionViewOnUI(currentVerbPrepositionForUpdate.Id);
            VerbPrepositionViews.Remove(verbPrepositionView);
            await verbPrepositionRepository.DeleteVerbPreposition(currentVerbPrepositionForUpdate.Id);
        }
        private async Task DeleteAllVerbPrepositions()
        {
            VerbPrepositionViews.Clear();
            AddShadowVerbPrepositionViewToUI();
            await verbPrepositionRepository.DeleteAllDictionaryVerbPrepositions(pageCurrentDictionaryId);
        }
        private void RemoveExampleView(int exampleId)
        {
            ExampleView? awExampleView = AwTryFindExampleView(exampleId);
            ExampleView? uwExampleView = UwTryFindExampleView(exampleId);
            if (awExampleView is not null)
                AwExampleViews.Remove(awExampleView);
            if (uwExampleView is not null)
                UwExampleViews.Remove(uwExampleView);
            AwValidateExampleSection();
            UwValidateExampleSection();
        }
        private void SearchVerbPrepositions()
        {
            foreach (UserControl userControl in VerbPrepositionViews)
            {
                VerbPrepositionView? verbPrepositionView = userControl as VerbPrepositionView;
                if (verbPrepositionView is null)
                    continue;
                bool isMatch = verbPrepositionView.VerbValue.Contains(SearchStringValue) || verbPrepositionView.PrepositionValue.Contains(SearchStringValue);
                if (isMatch)
                    verbPrepositionView.Show();
                else
                    verbPrepositionView.Collapse();
            }
        }
        private void AwOpenWindow() => AwOpenWindowButtonSoftClick();
        private void AwClear()
        {
            AwVerbValue = string.Empty;
            AwPrepositionValue = string.Empty;
            AwTranslationValue = string.Empty;
            AwExampleViews.Clear();
            AwAddExampleButtonIsEnabled = false;
        }
        private void AwUpdateConfirmButtonAvailability() => AwConfirmButtonIsEnabled = ValidationPool.IsValid(ValidationRulesGroup.AddVerbPreposition);
        private void AwCheckVerbPrepositionForExisting()
        {
            string verbValue = AwVerbValue;
            string prepositionValue = AwPrepositionValue;
            VerbPrepositionExist = verbPrepositionRepository.IsVerbPrepositionExist(verbValue, prepositionValue, pageCurrentDictionaryId);
        }
        private void AwAddExampleView()
        {
            if (awExampleIsInvalid)
                return;
            AwExampleViews.Add(ExampleView.Create(AwExampleRussianValue, AwExampleEnglishValue, ++exampleIdCounter));
        }
        private void AwClearExampleSection()
        {
            AwExampleRussianValue = string.Empty;
            AwExampleEnglishValue = string.Empty;
        }
        private void AwValidateExampleSection()
        {
            bool anyTextBoxIsEmpty = string.IsNullOrWhiteSpace(AwExampleEnglishValue) || string.IsNullOrWhiteSpace(AwExampleRussianValue);
            if (anyTextBoxIsEmpty || AwExampleViews.Count >= ModelConstants.MaxExamplesCount)
            {
                awExampleIsInvalid = true;
                AwAddExampleButtonIsEnabled = false;
            }
            else
            {
                awExampleIsInvalid = false;
                AwAddExampleButtonIsEnabled = true;
            }
        }
        private void AwFocusExampleRussianValueTextBox() => App.GetService<EditVerbPrepositionDictionaryPage>().awExampleRussianValueTextBox.Focus();
        private void AwCheckExampleTextBoxesMaxLengthVisibility()
        {
            bool anyTextBoxIsInRange = (!string.IsNullOrWhiteSpace(AwExampleEnglishValue) && AwExampleEnglishValue.Length > 20)
                                    || (!string.IsNullOrWhiteSpace(AwExampleRussianValue) && AwExampleRussianValue.Length > 20);
            if (anyTextBoxIsInRange)
                AwShowExampleTextBoxesMaxLength();
            else
                AwHideExampleTextBoxesMaxLength();
        }
        private void UwOpenWindow(int verbPrepositionId)
        {
            VerbPreposition verbPreposition = verbPrepositionRepository.GetVerbPreposition(verbPrepositionId);
            SetVerbPrepositionForUpdating(verbPreposition);
            UwOpenWindowButtonSoftClick();
        }
        private void UwClear()
        {
            UwTranslationValue = string.Empty;
            UwExampleRussianValue = string.Empty;
            UwExampleEnglishValue = string.Empty;
            UwExampleViews.Clear();
            UwAddExampleButtonIsEnabled = false;
        }
        private void UwUpdateConfirmButtonAvailability() => UwConfirmButtonIsEnabled = ValidationPool.IsValid(ValidationRulesGroup.UpdateVerbPreposition);
        private void UwAddExampleView()
        {
            if (uwExampleIsInvalid || UwExampleViews.Count >= ModelConstants.MaxExamplesCount)
                return;
            UwExampleViews.Add(ExampleView.Create(UwExampleRussianValue, UwExampleEnglishValue, ++exampleIdCounter));
        }
        private void UwClearExampleSection()
        {
            UwExampleRussianValue = string.Empty;
            UwExampleEnglishValue = string.Empty;
        }
        private void UwValidateExampleSection()
        {
            bool anyTextBoxIsEmpty = string.IsNullOrWhiteSpace(UwExampleEnglishValue) || string.IsNullOrWhiteSpace(UwExampleRussianValue);
            if (anyTextBoxIsEmpty || UwExampleViews.Count >= ModelConstants.MaxExamplesCount)
            {
                uwExampleIsInvalid = true;
                UwAddExampleButtonIsEnabled = false;
            }
            else
            {
                uwExampleIsInvalid = false;
                UwAddExampleButtonIsEnabled = true;
            }
        }
        private void UwCheckExampleTextBoxesMaxLengthVisibility()
        {
            bool anyTextBoxIsInRange = (!string.IsNullOrWhiteSpace(UwExampleEnglishValue) && UwExampleEnglishValue.Length > 20)
                                    || (!string.IsNullOrWhiteSpace(UwExampleRussianValue) && UwExampleRussianValue.Length > 20);
            if (anyTextBoxIsInRange)
                UwShowExampleTextBoxesMaxLength();
            else
                UwHideExampleTextBoxesMaxLength();
        }
        #endregion

        #region Event handling
        protected override void InitEvents()
        {
            EditVerbPrepositionDictionaryPage.AddingWindowVerbValueTextBoxEnterDown += OnAddingWindowVerbValueTextBoxEnterDown;
            EditVerbPrepositionDictionaryPage.AddingWindowPrepositionValueTextBoxEnterDown += OnAddingWindowPrepositionValueTextBoxEnterDown;
            EditVerbPrepositionDictionaryPage.AddingWindowTranslationValueTextBoxEnterDown += OnAddingWindowTranslationValueTextBoxEnterDown;
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
        private void OnAddingWindowVerbValueTextBoxEnterDown() => AwFocusPrepositionValueTextBox();
        private void OnAddingWindowPrepositionValueTextBoxEnterDown() => AwFocusTranslationValueTextBox();
        private void OnAddingWindowTranslationValueTextBoxEnterDown()
        {
            if (ValidationPool.IsValid(ValidationRulesGroup.AddVerbPreposition))
                AwConfirmButtonSoftClick();
        }
        private void OnAddingWindowExampleRussianValueTextBoxEnterDown() => AwFocusExampleEnglishValueTextBox();
        private void OnAddingWindowExampleEnglishValueTextBoxEnterDown()
        {
            if (awExampleIsInvalid)
                return;
            AwAddExampleButtonSoftClick();
        }
        private void OnUpdateWindowExampleRussianValueTextBoxEnterDown() => UwFocusExampleEnglishValueTextBox();
        private void OnUpdateWindowExampleEnglishValueTextBoxEnterDown()
        {
            if (uwExampleIsInvalid)
                return;
            UwAddExampleButtonSoftClick();
        }
        private void OnWindowCtrlNDown()
        {
            if (App.GetService<AppWindowVM>().CurrentPage == Page.EditVerbPrepositionDictionaryPage)
            {
                AwFocusVerbValueTextBox();
                AwOpenWindowButtonSoftClick();
            }
        }
        private void OnWindowEscDown()
        {
            if (App.GetService<AppWindowVM>().CurrentPage == Page.EditVerbPrepositionDictionaryPage)
                AwCancelButtonSoftClick();
        }
        private void OnGoBackButtonClick()
        {
            App.GetService<AppWindowVM>().HideGoBackButtonCommand.Execute();
            GoBack();
        }
        private void OnDrawerButtonClick() => App.GetService<AppWindowVM>().HideGoBackButtonCommand.Execute();
        private void OnOpenMenuButtonClick()
        {
            if (topMenuIsOpened)
            {
                CloseTopMenuButtonSoftClick();
                topMenuIsOpened = false;
            }
            else
            {
                OpenTopMenuButtonSoftClick();
                topMenuIsOpened = true;
            }
        }
        #endregion

        #region Private page halpers
        private IEnumerable<VerbPrepositionView> CreateOrderedVerbPrepositionViews(IEnumerable<VerbPreposition> verbPrepositions)
        {
            return verbPrepositions
                .Select(verbPreposition => VerbPrepositionView.Create(verbPreposition))
                .OrderBy(verbPrepositiinView => verbPrepositiinView.Order);
        }
        #endregion

        #region Private adding window helpers
        private string? AwGetFirstExampleRussianValue() => AwExampleViews.Count == 0 ? null : AwExampleViews.ToArray()[0].RussianValue;
        private string? AwGetFirstExampleEnglishValue() => AwExampleViews.Count == 0 ? null : AwExampleViews.ToArray()[0].EnglishValue;
        private string? AwGetSecondExampleRussianValue() => AwExampleViews.Count < 2 ? null : AwExampleViews.ToArray()[1].RussianValue;
        private string? AwGetSecondExampleEnglishValue() => AwExampleViews.Count < 2 ? null : AwExampleViews.ToArray()[1].RussianValue;
        #endregion

        #region Private update window helpers
        private string? UwGetFirstExampleRussianValue() => UwExampleViews.Count == 0 ? null : UwExampleViews.ToArray()[0].RussianValue;
        private string? UwGetFirstExampleEnglishValue() => UwExampleViews.Count == 0 ? null : UwExampleViews.ToArray()[0].EnglishValue;
        private string? UwGetSecondExampleRussianValue() => UwExampleViews.Count < 2 ? null : UwExampleViews.ToArray()[1].RussianValue;
        private string? UwGetSecondExampleEnglishValue() => UwExampleViews.Count < 2 ? null : UwExampleViews.ToArray()[1].RussianValue;
        #endregion

        #region Private UI methods (page)
        private void AddVerbPrepositionViewsToUIKeepingOrder(IEnumerable<VerbPrepositionView> verbPrepositionViews)
        {
            foreach (VerbPrepositionView verbPrepositionView in verbPrepositionViews)
                VerbPrepositionViews.Add(verbPrepositionView);
        }
        private void AddShadowVerbPrepositionViewToUI() => VerbPrepositionViews.Add(ShadowVerbPrepositionView.Create());
        private void AddVerbPrepositionViewToUI(VerbPreposition verbPreposition) => VerbPrepositionViews.Add(VerbPrepositionView.Create(verbPreposition));
        private void UpdateVerbPrepositionViewOnUI(VerbPreposition updatedVerbPreposition)
        {
            VerbPrepositionView verbPrepositionView = FindVerbPrepositionViewOnUI(updatedVerbPreposition.Id);
            verbPrepositionView.Update(updatedVerbPreposition);
        }
        private VerbPrepositionView FindVerbPrepositionViewOnUI(int verbPrepositionId) => (VerbPrepositionView)VerbPrepositionViews.First(userControl =>
        {
            VerbPrepositionView? verbPrepositionView = userControl as VerbPrepositionView;
            return verbPrepositionView is null ? false : verbPrepositionView.Id == verbPrepositionId;
        });
        #endregion

        #region Private UI methods (top menu)
        private void OpenTopMenuButtonSoftClick() => App.GetService<EditVerbPrepositionDictionaryPage>().openTopMenuButton.SoftClick();
        private void CloseTopMenuButtonSoftClick() => App.GetService<EditVerbPrepositionDictionaryPage>().closeTopMenuButton.SoftClick();
        #endregion

        #region Private UI methods (adding window)
        private ExampleView? AwTryFindExampleView(int exampleId) => AwExampleViews.FirstOrDefault(exampleView => exampleView.Id == exampleId);
        private void AwOpenWindowButtonSoftClick() => App.GetService<EditVerbPrepositionDictionaryPage>().awOpenWindowButton.SoftClick();
        private void AwAddExampleButtonSoftClick() => App.GetService<EditVerbPrepositionDictionaryPage>().awAddExampleButton.SoftClick();
        private void AwCancelButtonSoftClick() => App.GetService<EditVerbPrepositionDictionaryPage>().awCancelButton.SoftClick();
        private void AwConfirmButtonSoftClick() => App.GetService<EditVerbPrepositionDictionaryPage>().awConfirmButton.SoftClick();
        private void AwFocusExampleEnglishValueTextBox() => App.GetService<EditVerbPrepositionDictionaryPage>().awExampleEnglishValueTextBox.Focus();
        private void AwFocusTranslationValueTextBox() => App.GetService<EditVerbPrepositionDictionaryPage>().awTranslationValueTextBox.Focus();
        private void AwFocusVerbValueTextBox() => App.GetService<EditVerbPrepositionDictionaryPage>().awVerbValueTextBox.Focus();
        private void AwFocusPrepositionValueTextBox() => App.GetService<EditVerbPrepositionDictionaryPage>().awPrepositionValueTextBox.Focus();
        private void AwShowExampleTextBoxesMaxLength()
        {
            TextBox awExampleRussianValueTextBox = App.GetService<EditVerbPrepositionDictionaryPage>().awExampleRussianValueTextBox;
            TextBox awExampleEnglishValueTextBox = App.GetService<EditVerbPrepositionDictionaryPage>().awExampleEnglishValueTextBox;
            awExampleRussianValueTextBox.MaxLength = ModelConstants.ExampleValueMaxLength;
            awExampleEnglishValueTextBox.MaxLength = ModelConstants.ExampleValueMaxLength;
            awExampleRussianValueTextBox.Margin = new Thickness(0, 0, 7, 7);
            awExampleEnglishValueTextBox.Margin = new Thickness(7, 0, 0, 7);
        }
        private void AwHideExampleTextBoxesMaxLength()
        {
            TextBox awExampleRussianValueTextBox = App.GetService<EditVerbPrepositionDictionaryPage>().awExampleRussianValueTextBox;
            TextBox awExampleEnglishValueTextBox = App.GetService<EditVerbPrepositionDictionaryPage>().awExampleEnglishValueTextBox;
            awExampleRussianValueTextBox.MaxLength = 0;
            awExampleEnglishValueTextBox.MaxLength = 0;
            awExampleRussianValueTextBox.Margin = new Thickness(0, 0, 7, 0);
            awExampleEnglishValueTextBox.Margin = new Thickness(7, 0, 0, 0);
        }
        #endregion

        #region Private UI methods (update window)
        private ExampleView? UwTryFindExampleView(int exampleId) => UwExampleViews.FirstOrDefault(exampleView => exampleView.Id == exampleId);
        private void UwOpenWindowButtonSoftClick() => App.GetService<EditVerbPrepositionDictionaryPage>().uwOpenWindowButton.SoftClick();
        private void UwAddExampleButtonSoftClick() => App.GetService<EditVerbPrepositionDictionaryPage>().uwAddExampleButton.SoftClick();
        private void UwFocusExampleEnglishValueTextBox() => App.GetService<EditVerbPrepositionDictionaryPage>().uwExampleEnglishValueTextBox.Focus();
        private void UwFocusExampleRussianValueTextBox() => App.GetService<EditVerbPrepositionDictionaryPage>().uwExampleRussianValueTextBox.Focus();
        private void UwShowExampleTextBoxesMaxLength()
        {
            TextBox awExampleRussianValueTextBox = App.GetService<EditVerbPrepositionDictionaryPage>().uwExampleRussianValueTextBox;
            TextBox awExampleEnglishValueTextBox = App.GetService<EditVerbPrepositionDictionaryPage>().uwExampleEnglishValueTextBox;
            awExampleRussianValueTextBox.MaxLength = ModelConstants.ExampleValueMaxLength;
            awExampleEnglishValueTextBox.MaxLength = ModelConstants.ExampleValueMaxLength;
            awExampleRussianValueTextBox.Margin = new Thickness(0, 0, 7, 7);
            awExampleEnglishValueTextBox.Margin = new Thickness(7, 0, 0, 7);
        }
        private void UwHideExampleTextBoxesMaxLength()
        {
            TextBox awExampleRussianValueTextBox = App.GetService<EditVerbPrepositionDictionaryPage>().uwExampleRussianValueTextBox;
            TextBox awExampleEnglishValueTextBox = App.GetService<EditVerbPrepositionDictionaryPage>().uwExampleEnglishValueTextBox;
            awExampleRussianValueTextBox.MaxLength = 0;
            awExampleEnglishValueTextBox.MaxLength = 0;
            awExampleRussianValueTextBox.Margin = new Thickness(0, 0, 7, 0);
            awExampleEnglishValueTextBox.Margin = new Thickness(7, 0, 0, 0);
        }
        private void SetVerbPrepositionForUpdating(VerbPreposition verbPreposition)
        {
            currentVerbPrepositionForUpdate = verbPreposition;
            UwTranslationValue = StringHelper.EmptyIfNull(verbPreposition.Translation);
            UwExampleViews.Clear();
            if (verbPreposition.IsFirstExampleExist)
                UwExampleViews.Add(ExampleView.Create(verbPreposition.FirstExampleRussianValue.EmptyIfNull(), verbPreposition.FirstExampleEnglishValue.TryNormalizeRegister().EmptyIfNull(), ++exampleIdCounter));
            if (verbPreposition.IsSecondExampleExist)
                UwExampleViews.Add(ExampleView.Create(verbPreposition.SecondExampleRussianValue.EmptyIfNull(), verbPreposition.SecondExampleEnglishValue.TryNormalizeRegister().EmptyIfNull(), ++exampleIdCounter));
        }
        #endregion
    }
}
