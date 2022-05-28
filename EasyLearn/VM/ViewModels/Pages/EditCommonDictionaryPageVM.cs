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
using EasyLearn.Infrastructure.UIConstants;

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
        private int exampleIdCounter;
        private int pageCurrentDictionaryId;
        private bool topMenuIsOpened = false;
        private Guid relationExistValidationRuleId;
        private CommonRelation currentRelationForUpdate;
        #endregion

        #region Private helper props
        private bool CommonRelationExist
        {
            set
            {
                ValidationPool.Set(ValidationRulesGroup.AddCommonRelation, relationExistValidationRuleId, !value);
                AwCommonRelationHasExistLableIsVisible = value;
            }
        }
        #endregion

        #region Binding props
        public ObservableCollection<UserControl> CommonRelationViews { get; set; } = new ObservableCollection<UserControl>();
        public string SearchStringValue { get; set; }
        public SolidColorBrush HorisontalSeporatorColor => AwCommonRelationHasExistLableIsVisible ? ColorCodes.EasyRed.GetBrushByHex() : ColorCodes.EasyGray.GetBrushByHex();

        public ObservableCollection<ExampleView> AwExampleViews { get; set; } = new ObservableCollection<ExampleView>();
        public string AwEnglishValue { get; set; }
        public string AwRussianValue { get; set; }
        public string AwCommentValue { get; set; }
        public string AwExampleRussianValue { get; set; }
        public string AwExampleEnglishValue { get; set; }
        public bool AwConfirmButtonIsEnabled { get; set; }
        public bool AwCommonRelationHasExistLableIsVisible { get; set; }
        public bool AwAddExampleButtonIsEnabled { get; set; }
        public ObservableCollection<UnitTypeComboBoxItem> AwRussianUnitTypes { get; set; }
        public ObservableCollection<UnitTypeComboBoxItem> AwEnglishUnitTypes { get; set; }
        public UnitTypeComboBoxItem AwSelectedRussianUnitType { get; set; }
        public UnitTypeComboBoxItem AwSelectedEnglishUnitType { get; set; }

        public ObservableCollection<ExampleView> UwExampleViews { get; set; } = new ObservableCollection<ExampleView>();
        public string UwCommentValue { get; set; }
        public string UwExampleRussianValue { get; set; }
        public string UwExampleEnglishValue { get; set; }
        public bool UwConfirmButtonIsEnabled { get; set; } = true;
        public bool UwAddExampleButtonIsEnabled { get; set; }
        #endregion

#pragma warning disable CS8618
        public EditCommonDictionaryPageVM(ICommonDictionaryRepository commonDictionaryRepository, ICommonRelationRepository commonRelationRepository)
        {
            this.commonDictionaryRepository = commonDictionaryRepository;
            this.commonRelationRepository = commonRelationRepository;
            AwSetRussianUnitTypes();
            AwSetEnglishUnitTypes();
            this.relationExistValidationRuleId = ValidationPool.Register(ValidationRulesGroup.AddCommonRelation);
        }
#pragma warning restore CS8618

        #region Commands
        public Command GoBackCommand { get; private set; }
        public Command CreateCommonRelationCommand { get; private set; }
        public Command UpdateCommonRelationCommand { get; private set; }
        public Command DeleteCommonRelationCommand { get; private set; }
        public Command DeleteAllCommonRelationsCommand { get; private set; }
        public Command<int> SetDictionaryCommand { get; private set; }
        public Command SearchCommonRelationsCommand { get; private set; }
        public Command<int> RemoveExampleViewCommand { get; private set; }

        public Command AwOpenWindowCommand { get; private set; }
        public Command AwClearCommand { get; private set; }
        public Command AwUpdateConfirmButtonAvailabilityCommand { get; private set; }
        public Command AwAddExampleViewCommand { get; private set; }
        public Command AwCheckCommonRelationForExistingCommand { get; private set; }
        public Command AwClearExampleSectionCommand { get; private set; }
        public Command AwValidateExampleSectionCommand { get; private set; }
        public Command AwCheckExampleTextBoxesMaxLengthVisibilityCommand { get; private set; }
        public Command AwFocusExampleRussianValueTextBoxCommand { get; private set; }

        public Command<int> UwOpenWindowCommand { get; private set; }
        public Command UwClearCommand { get; private set; }
        public Command UwUpdateConfirmButtonAvailabilityCommand { get; private set; }
        public Command UwAddExampleViewCommand { get; private set; }
        public Command UwClearExampleSectionCommand { get; private set; }
        public Command UwValidateExampleSectionCommand { get; private set; }
        public Command UwCheckExampleTextBoxesMaxLengthVisibilityCommand { get; private set; }
        public Command UwFocusExampleRussianValueTextBoxCommand { get; private set; }
        protected override void InitCommands()
        {
            GoBackCommand = new Command(GoBack);
            CreateCommonRelationCommand = new Command(async () => await CreateCommonRelation());
            UpdateCommonRelationCommand = new Command(async () => await UpdateCommonRelation());
            DeleteCommonRelationCommand = new Command(async () => await DeleteCommonRelation());
            DeleteAllCommonRelationsCommand = new Command(async () => await DeleteAllCommonRelations());
            SetDictionaryCommand = new Command<int>(async commonDictionaryId => await SetDictionary(commonDictionaryId));
            SearchCommonRelationsCommand = new Command(SearchCommonRelations);
            RemoveExampleViewCommand = new Command<int>(RemoveExampleView);

            AwOpenWindowCommand = new Command(AwOpenWindow);
            AwClearCommand = new Command(AwClear);
            AwUpdateConfirmButtonAvailabilityCommand = new Command(AwUpdateConfirmButtonAvailability);
            AwAddExampleViewCommand = new Command(AwAddExampleView);
            AwCheckCommonRelationForExistingCommand = new Command(AwCheckCommonRelationForExisting);
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
        private async Task CreateCommonRelation()
        {
            string russianUnitValue = AwRussianValue;
            string englishUnitValue = AwEnglishValue;
            UnitType englishUnitType = AwSelectedEnglishUnitType.UnitType;
            UnitType russianUnitType = AwSelectedRussianUnitType.UnitType;
            string? commentValue = StringHelper.NullIfEmptyOrWhiteSpace(AwCommentValue);
            int commonDictionaryId = pageCurrentDictionaryId;
            string? firstExampleRussianValue = AwGetFirstExampleRussianValue();
            string? firstExampleEnglishValue = AwGetFirstExampleEnglishValue();
            string? secondExampleRussianValue = AwGetSecondExampleRussianValue();
            string? secondExampleEnglishValue = AwGetSecondExampleEnglishValue();
            CommonRelation newCommonRelation = await commonRelationRepository.CreateCommonRelation(
                russianUnitValue,
                russianUnitType,
                englishUnitValue,
                englishUnitType,
                commonDictionaryId,
                commentValue,
                firstExampleRussianValue,
                firstExampleEnglishValue,
                secondExampleRussianValue,
                secondExampleEnglishValue);
            AddCommonRelationViewToUI(newCommonRelation);
        }
        private async Task UpdateCommonRelation()
        {
            int commonRelationId = currentRelationForUpdate.Id;
            string? comment = StringHelper.NullIfEmptyOrWhiteSpace(UwCommentValue);
            string? firstExampleRussianValue = UwGetFirstExampleRussianValue();
            string? firstExampleEnglishValue = UwGetFirstExampleEnglishValue();
            string? secondExampleRussianValue = UwGetSecondExampleRussianValue();
            string? secondExampleEnglishValue = UwGetSecondExampleEnglishValue();
            CommonRelation updatedCommonRelation = await commonRelationRepository.UpdateCommonRelation(
                commonRelationId,
                comment,
                firstExampleRussianValue,
                firstExampleEnglishValue,
                secondExampleRussianValue,
                secondExampleEnglishValue);
            UpdateCommonRelationOnUI(updatedCommonRelation);
        }
        private async Task DeleteCommonRelation()
        {
            CommonRelationView commonRelationView = FindCommonRelationViewOnUI(currentRelationForUpdate.Id);
            CommonRelationViews.Remove(commonRelationView);
            await commonRelationRepository.DeleteCommonRelation(currentRelationForUpdate.Id);
        }
        private async Task DeleteAllCommonRelations()
        {
            CommonRelationViews.Clear();
            AddShadowRelationViewToUI();
            await commonRelationRepository.DeleteAllDictionaryRelations(pageCurrentDictionaryId);
        }
        private async Task SetDictionary(int commonDictionaryId)
        {
            CommonDictionary commonDictionary = await commonDictionaryRepository.GetCommonDictionaryAsync(commonDictionaryId);
            pageCurrentDictionaryId = commonDictionaryId;
            CommonRelationViews.Clear();
            AddShadowRelationViewToUI();
            AddCommonRelationViewsToUIKeepingOrder(CreateOrderedCommonRelationViews(commonDictionary.Relations));
        }
        private void SearchCommonRelations()
        {
            foreach (UserControl userControl in CommonRelationViews)
            {
                CommonRelationView? commonRelationView = userControl as CommonRelationView;
                if (commonRelationView is null)
                    continue;
                bool isMatch = commonRelationView.RussianValue.Contains(SearchStringValue) || commonRelationView.EnglishValue.Contains(SearchStringValue);
                if (isMatch)
                    commonRelationView.Show();
                else
                    commonRelationView.Collapse();
            }
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
        private void AwOpenWindow() => AwOpenWindowButtonSoftClick();
        private void AwClear()
        {
            AwEnglishValue = string.Empty;
            AwRussianValue = string.Empty;
            AwCommentValue = string.Empty;
            AwExampleRussianValue = string.Empty;
            AwExampleEnglishValue = string.Empty;
            AwSelectedRussianUnitType = AwRussianUnitTypes[0];
            AwSelectedEnglishUnitType = AwEnglishUnitTypes[0];
            AwExampleViews.Clear();
            AwAddExampleButtonIsEnabled = false;
        }
        private void AwUpdateConfirmButtonAvailability() => AwConfirmButtonIsEnabled = ValidationPool.IsValid(ValidationRulesGroup.AddCommonRelation);
        private void AwAddExampleView()
        {
            if (awExampleInvalid || AwExampleViews.Count >= ModelConstants.MaxExamplesCount)
                return;
            AwExampleViews.Add(ExampleView.Create(StringHelper.Prepare(AwExampleRussianValue), StringHelper.Prepare(AwExampleEnglishValue), ++exampleIdCounter));
        }
        private void AwCheckCommonRelationForExisting()
        {
            UnitType englishUnitType = AwSelectedEnglishUnitType.UnitType;
            UnitType russianUnitType = AwSelectedRussianUnitType.UnitType;
            CommonRelationExist = commonRelationRepository.IsCommonRelationExist(AwRussianValue, russianUnitType, AwEnglishValue, englishUnitType, pageCurrentDictionaryId);
        }
        private void AwClearExampleSection()
        {
            AwExampleRussianValue = string.Empty;
            AwExampleEnglishValue = string.Empty;
        }
        private void AwValidateExampleSection()
        {
            bool anyTextBoxIsEmpty = string.IsNullOrWhiteSpace(AwExampleEnglishValue)
                                  || string.IsNullOrWhiteSpace(AwExampleRussianValue);
            if (anyTextBoxIsEmpty || AwExampleViews.Count >= ModelConstants.MaxExamplesCount)
            {
                awExampleInvalid = true;
                AwAddExampleButtonIsEnabled = false;
            }
            else
            {
                awExampleInvalid = false;
                AwAddExampleButtonIsEnabled = true;
            }
        }
        private void AwCheckExampleTextBoxesMaxLengthVisibility()
        {
            bool anyTextBoxIsInRange = (!string.IsNullOrWhiteSpace(AwExampleEnglishValue) && AwExampleEnglishValue.Length > 20)
                                    || (!string.IsNullOrWhiteSpace(AwExampleRussianValue) && AwExampleRussianValue.Length > 20);
            if (anyTextBoxIsInRange)
                AwShowExampleTextBoxesMaxLength();
            else
                AwHideExampleTextBoxesMaxLength();
        }
        private void UwOpenWindow(int commonRelationId)
        {
            CommonRelation commonRelation = commonRelationRepository.GetCommonRelation(commonRelationId);
            SetCommonRelationForUpdating(commonRelation);
            UwOpenWindowButtonSoftClick();
        }
        private void UwClear()
        {
            UwCommentValue = string.Empty;
            UwExampleRussianValue = string.Empty;
            UwExampleEnglishValue = string.Empty;
            UwExampleViews.Clear();
            UwAddExampleButtonIsEnabled = false;
        }
        private void UwUpdateConfirmButtonAvailability() => UwConfirmButtonIsEnabled = ValidationPool.IsValid(ValidationRulesGroup.UpdateCommonRelation);
        private void UwAddExampleView()
        {
            if (uwExampleInvalid || UwExampleViews.Count >= ModelConstants.MaxExamplesCount)
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
            bool anyTextBoxIsEmpty = string.IsNullOrWhiteSpace(UwExampleEnglishValue)
                                  || string.IsNullOrWhiteSpace(UwExampleRussianValue);
            if (anyTextBoxIsEmpty || UwExampleViews.Count >= ModelConstants.MaxExamplesCount)
            {
                uwExampleInvalid = true;
                UwAddExampleButtonIsEnabled = false;
            }
            else
            {
                uwExampleInvalid = false;
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
            EditCommonDictionaryPage.AddingWindowRussianValueTextBoxEnterDown += OnAddingWindowRussianValueTextBoxEnterDown;
            EditCommonDictionaryPage.AddingWindowEnglishValueTextBoxEnterDown += OnAddingWindowEnglishValueTextBoxEnterDown;
            EditCommonDictionaryPage.AddingWindowRussianUnitTypeComboBoxEnterDown += OnAddingWindowRussianUnitTypeComboBoxEnterDown;
            EditCommonDictionaryPage.AddingWindowEnglishUnitTypeComboBoxEnterDown += OnAddingWindowEnglishUnitTypeComboBoxEnterDown;
            EditCommonDictionaryPage.AddingWindowCommentValueTextBoxEnterDown += OnAddingWindowCommentValueTextBoxEnterDown;
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
        private void OnAddingWindowRussianValueTextBoxEnterDown() => AwFocusEnglishValueTextBox();
        private void OnAddingWindowEnglishValueTextBoxEnterDown() => AwFocusRussianUnitTypeComboBox();
        private void OnAddingWindowRussianUnitTypeComboBoxEnterDown() => AwFocusEnglishUnitTypeComboBox();
        private void OnAddingWindowEnglishUnitTypeComboBoxEnterDown() => AwFocusCommentValueTextBox();
        private void OnAddingWindowCommentValueTextBoxEnterDown()
        {
            if (ValidationPool.IsValid(ValidationRulesGroup.AddCommonRelation))
                AwConfirmButtonSoftClick();
        }
        private void OnAddingWindowExampleRussianValueTextBoxEnterDown() => AwFocusExampleEnglishValueTextBox();
        private void OnAddingWindowExampleEnglishValueTextBoxEnterDown()
        {
            if (awExampleInvalid)
                return;
            AwAddExampleButtonSoftClick();
        }
        private void OnUpdateWindowExampleRussianValueTextBoxEnterDown() => UwFocusExampleEnglishValueTextBox();
        private void OnUpdateWindowExampleEnglishValueTextBoxEnterDown()
        {
            if (uwExampleInvalid)
                return;
            UwAddExampleButtonSoftClick();
        }
        private void OnWindowCtrlNDown()
        {
            if (App.GetService<AppWindowVM>().CurrentPage == Page.EditCommonDictionaryPage)
            {
                AwFocusRussianValueTextBox();
                AwOpenWindowButtonSoftClick();
            }
        }
        private void OnWindowEscDown()
        {
            if (App.GetService<AppWindowVM>().CurrentPage == Page.EditCommonDictionaryPage)
                AwCancelButtonSoftClick();
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
                CloseTopMenuButtonSoftClick();
                topMenuIsOpened = false;
            }
            else
            {
                OpenTopMenuButtonSoftClick();
                topMenuIsOpened = true;
            }
        }
        private void OnDrawerButtonClick() => App.GetService<AppWindowVM>().HideGoBackButtonCommand.Execute();
        #endregion

        #region Private page halpers
        private IEnumerable<CommonRelationView> CreateOrderedCommonRelationViews(IEnumerable<CommonRelation> commonRelations)
        {
            return commonRelations
                .Select(commonRelation => CommonRelationView.Create(commonRelation))
                .OrderBy(commonRelationView => commonRelationView.Order);
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
        private void AddCommonRelationViewsToUIKeepingOrder(IEnumerable<CommonRelationView> commonRelationViews)
        {
            foreach (CommonRelationView commonRelationView in commonRelationViews)
                CommonRelationViews.Add(commonRelationView);
        }
        private void AddShadowRelationViewToUI() => CommonRelationViews.Add(ShadowCommonRelationView.Create());
        private void AddCommonRelationViewToUI(CommonRelation commonRelation) => CommonRelationViews.Add(CommonRelationView.Create(commonRelation));
        private void UpdateCommonRelationOnUI(CommonRelation updatedCommonRelation)
        {
            CommonRelationView relationView = FindCommonRelationViewOnUI(updatedCommonRelation.Id);
            relationView.Update(updatedCommonRelation);
        }
        private CommonRelationView FindCommonRelationViewOnUI(int commonRelationId) => (CommonRelationView)CommonRelationViews.First(userControl =>
        {
            CommonRelationView? commonRelationView = userControl as CommonRelationView;
            return commonRelationView is null ? false : commonRelationView.Id == commonRelationId;
        });
        #endregion

        #region Private UI methods (update window)
        private ExampleView? UwTryFindExampleView(int exampleId) => UwExampleViews.FirstOrDefault(exampleView => exampleView.Id == exampleId);
        private void UwOpenWindowButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().uwOpenWindowButton.SoftClick();
        private void UwAddExampleButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().uwAddExampleButton.SoftClick();
        private void UwFocusExampleEnglishValueTextBox() => App.GetService<EditCommonDictionaryPage>().uwExampleEnglishValueTextBox.Focus();
        private void UwFocusExampleRussianValueTextBox() => App.GetService<EditCommonDictionaryPage>().uwExampleRussianValueTextBox.Focus();
        private void UwShowExampleTextBoxesMaxLength()
        {
            TextBox uwExampleRussianValueTextBox = App.GetService<EditCommonDictionaryPage>().uwExampleRussianValueTextBox;
            TextBox uwExampleEnglishValueTextBox = App.GetService<EditCommonDictionaryPage>().uwExampleEnglishValueTextBox;
            uwExampleRussianValueTextBox.MaxLength = ModelConstants.ExampleValueMaxLength;
            uwExampleEnglishValueTextBox.MaxLength = ModelConstants.ExampleValueMaxLength;
            uwExampleRussianValueTextBox.Margin = new Thickness(0, 0, 7, 7);
            uwExampleEnglishValueTextBox.Margin = new Thickness(0, 0, 0, 7);
        }
        private void UwHideExampleTextBoxesMaxLength()
        {
            TextBox uwExampleRussianValueTextBox = App.GetService<EditCommonDictionaryPage>().uwExampleRussianValueTextBox;
            TextBox uwExampleEnglishValueTextBox = App.GetService<EditCommonDictionaryPage>().uwExampleEnglishValueTextBox;
            uwExampleRussianValueTextBox.MaxLength = 0;
            uwExampleEnglishValueTextBox.MaxLength = 0;
            uwExampleRussianValueTextBox.Margin = new Thickness(0, 0, 7, 0);
            uwExampleEnglishValueTextBox.Margin = new Thickness(0, 0, 0, 0);
        }
        private void SetCommonRelationForUpdating(CommonRelation commonRelation)
        {
            currentRelationForUpdate = commonRelation;
            UwCommentValue = StringHelper.EmptyIfNull(commonRelation.Comment);
            UwExampleViews.Clear();
            if (commonRelation.IsFirstExampleExist)
                UwExampleViews.Add(ExampleView.Create(commonRelation.FirstExampleRussianValue.EmptyIfNull(), commonRelation.FirstExampleEnglishValue.TryNormalizeRegister().EmptyIfNull(), ++exampleIdCounter));
            if (commonRelation.IsSecondExampleExist)
                UwExampleViews.Add(ExampleView.Create(commonRelation.SecondExampleRussianValue.EmptyIfNull(), commonRelation.SecondExampleEnglishValue.TryNormalizeRegister().EmptyIfNull(), ++exampleIdCounter));
        }
        #endregion

        #region private UI methods (adding window)
        private ExampleView? AwTryFindExampleView(int exampleId) => AwExampleViews.FirstOrDefault(exampleView => exampleView.Id == exampleId);
        private void AwCancelButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().awCancelButton.SoftClick();
        private void AwAddExampleButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().awAddExampleButton.SoftClick();
        private void AwConfirmButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().awConfirmButton.SoftClick();
        private void AwOpenWindowButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().awOpenWindowButton.SoftClick();
        private void AwFocusRussianValueTextBox() => App.GetService<EditCommonDictionaryPage>().awRussianValueTextBox.Focus();
        private void AwFocusEnglishValueTextBox() => App.GetService<EditCommonDictionaryPage>().awEnglishValueTextBox.Focus();
        private void AwFocusRussianUnitTypeComboBox() => App.GetService<EditCommonDictionaryPage>().awRussianUnitTypeComboBox.Focus();
        private void AwFocusEnglishUnitTypeComboBox() => App.GetService<EditCommonDictionaryPage>().awEnglishUnitTypeComboBox.Focus();
        private void AwFocusCommentValueTextBox() => App.GetService<EditCommonDictionaryPage>().awCommentValueTextBox.Focus();
        private void AwFocusExampleEnglishValueTextBox() => App.GetService<EditCommonDictionaryPage>().awExampleEnglishValueTextBox.Focus();
        private void AwFocusExampleRussianValueTextBox() => App.GetService<EditCommonDictionaryPage>().awExampleRussianValueTextBox.Focus();
        private void AwShowExampleTextBoxesMaxLength()
        {
            TextBox exampleRussianValueTextBox = App.GetService<EditCommonDictionaryPage>().awExampleRussianValueTextBox;
            TextBox exampleEnglishValueTextBox = App.GetService<EditCommonDictionaryPage>().awExampleEnglishValueTextBox;
            exampleRussianValueTextBox.MaxLength = ModelConstants.ExampleValueMaxLength;
            exampleEnglishValueTextBox.MaxLength = ModelConstants.ExampleValueMaxLength;
            exampleRussianValueTextBox.Margin = new Thickness(0, 0, 7, 7);
            exampleEnglishValueTextBox.Margin = new Thickness(7, 0, 0, 7);
        }
        private void AwHideExampleTextBoxesMaxLength()
        {
            TextBox exampleRussianValueTextBox = App.GetService<EditCommonDictionaryPage>().awExampleRussianValueTextBox;
            TextBox exampleEnglishValueTextBox = App.GetService<EditCommonDictionaryPage>().awExampleEnglishValueTextBox;
            exampleRussianValueTextBox.MaxLength = 0;
            exampleEnglishValueTextBox.MaxLength = 0;
            exampleRussianValueTextBox.Margin = new Thickness(0, 0, 7, 0);
            exampleEnglishValueTextBox.Margin = new Thickness(7, 0, 0, 0);
        }
        private void AwSetRussianUnitTypes()
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
            AwRussianUnitTypes = russianUnitTypes;
            AwSelectedRussianUnitType = selectedRussianUnitType;
        }
        private void AwSetEnglishUnitTypes()
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
            AwEnglishUnitTypes = englishUnitTypes;
            AwSelectedEnglishUnitType = selectedEnglishUnitType;
        }
        #endregion

        #region Private UI methods (top menu)
        private void OpenTopMenuButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().openTopMenuButton.SoftClick();
        private void CloseTopMenuButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().closeTopMenuButton.SoftClick();
        #endregion
    }
}
