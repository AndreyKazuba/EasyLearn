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
using EasyLearn.Infrastructure.Constants;
using EasyLearn.Data.Helpers;
using EasyLearn.UI.Pages;
using EasyLearn.Infrastructure.Helpers;
using EasyLearn.UI;
using EasyLearn.Infrastructure.Enums;
using EasyLearn.Infrastructure.ValidationRules;

namespace EasyLearn.VM.ViewModels.Pages
{
    public class EditCommonDictionaryPageVM : ViewModel
    {
        #region Repositories
        private readonly ICommonDictionaryRepository commonDictionaryRepository;
        private readonly ICommonRelationRepository commonRelationRepository;
        #endregion

        #region Private fields
        private int dictionaryId;
        private Guid relationAlreadyExistValidationRuleId;
        #endregion

#pragma warning disable CS8618
        public EditCommonDictionaryPageVM(ICommonDictionaryRepository commonDictionaryRepository, ICommonRelationRepository commonRelationRepository)
        {
            this.commonDictionaryRepository = commonDictionaryRepository;
            this.commonRelationRepository = commonRelationRepository;
            SetAddingWindowRussianUnitTypes();
            SetAddingWidnowEnglishUnitTypes();
            RegisterRelationExistValidationRule();
        }
#pragma warning restore CS8618

        #region Props for binding
        public ObservableCollection<CommonRelationView> CommonRelationViews { get; set; }
        public string DictionaryName { get; set; }
        public string DictionaryDescription { get; set; }
        public string AddingWindowEnglishValue { get; set; }
        public string AddingWindowRussianValue { get; set; }
        public string AddingWindowCommentValue { get; set; }
        public bool IsConfirmCommonRelationAddingButtonEnabled { get; set; }
        public bool CommonRelationHasExistLableIsVisible { get; set; }
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
            AppWindow.WindowCtrlNDown += OnWindowCtrlNDown;
            AppWindow.WindowEscDown += OnWindowEscDown;
        }
        private void OnRussianValueTextBoxEnterDown() => FocusEnglishValueTextBox();
        private void OnEnglishValueTextBoxEnterDown() => FocusRussianUnitTypeComboBox();
        private void OnRussianUnitTypeComboBoxEnterDown() => FocusEnglishUnitTypeComboBox();
        private void OnEnglishUnitTypeComboBoxEnterDown() => FocusCommentValueTextBox();
        private void OnCommentValueTextBoxEnterDown()
        {
            if (!ValidationsPool.IsAddingCommonRelationWindowInvalid)
                AddingNewCommonRelationButtonSoftClick();
        }
        private void OnWindowCtrlNDown()
        {
            if (App.GetService<AppWindowVM>().CurrentPage == Page.EditCommonWordListPage)
            {
                FocusRussianValueTextBox();
                OpenNewCommonRelationAddingWindowButtonSoftClick();
            }
        }
        private void OnWindowEscDown()
        {
            if (App.GetService<AppWindowVM>().CurrentPage == Page.EditCommonWordListPage)
                NewCommonRelationAddingWindowCancelButtonSortClick();
        }
        #endregion

        #region Commands
        public Command GoBackCommand { get; private set; }
        public Command CreateCommonRelationCommand { get; private set; }
        public Command<int> DeleteCommonRelationCommand { get; private set; }
        public Command DeleteAllCommonRelationsCommand { get; private set; }
        public Command ClearAddingWindowCommand { get; private set; }
        public Command<int> SetDictionaryAsCurrentCommand { get; private set; }
        public Command UpdateConfirmCommonRelationAddingButtonAvailabilityCommand { get; private set; }
        public Command CheckCommonRelationForExistingCommand { get; private set; }
        protected override void InitCommands()
        {
            this.GoBackCommand = new Command(GoBack);
            this.CreateCommonRelationCommand = new Command(async () => await CreateCommonRelation());
            this.DeleteCommonRelationCommand = new Command<int>(async commonRelationId => await DeleteCommonRelation(commonRelationId));
            this.DeleteAllCommonRelationsCommand = new Command(async () => await DeleteAllCommonRelations());
            this.ClearAddingWindowCommand = new Command(ClearAddingWindow);
            this.SetDictionaryAsCurrentCommand = new Command<int>(async commonDictionaryId => await SetDictionaryAsCurrent(commonDictionaryId));
            this.UpdateConfirmCommonRelationAddingButtonAvailabilityCommand = new Command(UpdateConfirmCommonRelationAddingButtonAvailability);
            this.CheckCommonRelationForExistingCommand = new Command(CheckCommonRelationForExisting);
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
            CommonRelation newCommonRelation = await commonRelationRepository.CreateCommonRelation(russianUnitValue, russianUnitType, englishUnitValue, englishUnitType, commonDictionaryId, comment);
            AddCommonRelationToUI(newCommonRelation);
        }
        private async Task DeleteCommonRelation(int commonRelationId)
        {
            CommonRelationView commonRelationView = FindCommonRelationView(commonRelationId);
            this.CommonRelationViews.Remove(commonRelationView);
            await commonRelationRepository.DeleteCommonRelation(commonRelationId);
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
            this.AddingWindowSelectedRussianUnitType = AddingWindowRussianUnitTypes[0];
            this.AddingWindowSelectedEnglishUnitType = AddingWidnowEnglishUnitTypes[0];
        }
        private async Task SetDictionaryAsCurrent(int commonDictionaryId)
        {
            CommonDictionary commonDictionary = await commonDictionaryRepository.GetCommonDictionaryAsync(commonDictionaryId);
            this.dictionaryId = commonDictionaryId;
            this.DictionaryName = commonDictionary.Name;
            this.DictionaryDescription = StringHelper.EmptyIfNull(commonDictionary.Description);
            IEnumerable<CommonRelationView> commonRelationViews = commonDictionary.Relations.Select(commonRelation => CommonRelationView.Create(commonRelation));
            this.CommonRelationViews = new ObservableCollection<CommonRelationView>(commonRelationViews);
        }
        private void UpdateConfirmCommonRelationAddingButtonAvailability() => IsConfirmCommonRelationAddingButtonEnabled = !ValidationsPool.IsAddingCommonRelationWindowInvalid;
        #endregion

        #region Other private members
        private CommonRelationView FindCommonRelationView(int commonRelationId) => this.CommonRelationViews.First(commonRelationView => commonRelationView.Id == commonRelationId);
        private void AddCommonRelationToUI(CommonRelation commonRelation) => this.CommonRelationViews.Add(CommonRelationView.Create(commonRelation));
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
        private void RegisterRelationExistValidationRule()
        {
            this.relationAlreadyExistValidationRuleId = ValidationsPool.RegisterCommonRelationAddingWindowValidationRule(false);
        }
        private void CheckCommonRelationForExisting()
        {
            string englishValue = this.AddingWindowEnglishValue;
            string russianValue = this.AddingWindowRussianValue;
            UnitType englishUnitType = this.AddingWindowSelectedEnglishUnitType.UnitType;
            UnitType russianUnitType = this.AddingWindowSelectedRussianUnitType.UnitType;
            bool relationExist = commonRelationRepository.IsCommonRelationExist(russianValue, russianUnitType, englishValue, englishUnitType, this.dictionaryId);
            if (relationExist)
            {
                ValidationsPool.SetCommonRelationAddingWindowValidationRule(this.relationAlreadyExistValidationRuleId, false);
                this.CommonRelationHasExistLableIsVisible = true;
            }
            else
            {
                ValidationsPool.SetCommonRelationAddingWindowValidationRule(this.relationAlreadyExistValidationRuleId, true);
                this.CommonRelationHasExistLableIsVisible = false;
            }
        }
        #endregion

        private void FocusRussianValueTextBox() => App.GetService<EditCommonDictionaryPage>().newCommonRelationRussianValueTextBox.Focus();
        private void FocusEnglishValueTextBox() => App.GetService<EditCommonDictionaryPage>().newCommonRelationEnglishValueTextBox.Focus();
        private void FocusRussianUnitTypeComboBox() => App.GetService<EditCommonDictionaryPage>().newCommonRelationRussianUnitTypeComboBox.Focus();
        private void FocusEnglishUnitTypeComboBox() => App.GetService<EditCommonDictionaryPage>().newCommonRelationEnglishUnitTypeComboBox.Focus();
        private void FocusCommentValueTextBox() => App.GetService<EditCommonDictionaryPage>().newCommonRelationCommentValueTextBox.Focus();
        private void AddingNewCommonRelationButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().newCommonRelationAddingButton.SoftClick();
        private void OpenNewCommonRelationAddingWindowButtonSoftClick() => App.GetService<EditCommonDictionaryPage>().openNewCommonRelationAddingWindowButton.SoftClick();
        private void NewCommonRelationAddingWindowCancelButtonSortClick() => App.GetService<EditCommonDictionaryPage>().newCommonRelationAddingWindowCancelButton.SoftClick();
    }
}
