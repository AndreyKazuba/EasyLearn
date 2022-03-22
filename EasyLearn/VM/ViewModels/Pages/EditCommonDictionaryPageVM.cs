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
        #endregion

#pragma warning disable CS8618
        public EditCommonDictionaryPageVM(ICommonDictionaryRepository commonDictionaryRepository, ICommonRelationRepository commonRelationRepository)
        {
            this.commonDictionaryRepository = commonDictionaryRepository;
            this.commonRelationRepository = commonRelationRepository;
            SetAddingWindowRussianUnitTypes();
            SetAddingWidnowEnglishUnitTypes();
        }
#pragma warning restore CS8618

        #region Props for binding
        public ObservableCollection<CommonRelationView> CommonRelationViews { get; set; }
        public string DictionaryName { get; set; }
        public string DictionaryDescription { get; set; }
        public string AddingWindowEnglishValue { get; set; }
        public string AddingWindowRussianValue { get; set; }
        public string AddingWindowCommentValue { get; set; }
        public ObservableCollection<UnitTypeComboBoxItem> AddingWindowRussianUnitTypes { get; set; }
        public ObservableCollection<UnitTypeComboBoxItem> AddingWidnowEnglishUnitTypes { get; set; }
        public UnitTypeComboBoxItem AddingWindowSelectedRussianUnitType { get; set; }
        public UnitTypeComboBoxItem AddingWindowSelectedEnglishUnitType { get; set; }
        #endregion

        #region Commands
        public DelegateCommand GoBackCommand { get; private set; }
        public DelegateCommand CreateCommonRelationCommand { get; private set; }
        public DelegateCommand<int> DeleteCommonRelationCommand { get; private set; }
        public DelegateCommand DeleteAllCommonRelationsCommand { get; private set; }
        public DelegateCommand ClearAddingWindowCommand { get; private set; }
        public DelegateCommand<int> SetDictionaryAsCurrentCommand { get; private set; }
        protected override void InitCommands()
        {
            this.GoBackCommand = new DelegateCommand(arg => GoBack());
            this.CreateCommonRelationCommand = new DelegateCommand(async arg => await CreateCommonRelation());
            this.DeleteCommonRelationCommand = new DelegateCommand<int>(async commonRelationId => await DeleteCommonRelation(commonRelationId));
            this.DeleteAllCommonRelationsCommand = new DelegateCommand(async arg => await DeleteAllCommonRelations());
            this.ClearAddingWindowCommand = new DelegateCommand(arg => ClearAddingWindow());
            this.SetDictionaryAsCurrentCommand = new DelegateCommand<int>(async commonDictionaryId => await SetDictionaryAsCurrent(commonDictionaryId));
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
            this.CommonRelationViews.Add(CommonRelationView.Create(newCommonRelation));
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
        #endregion

        #region Other private members
        private CommonRelationView FindCommonRelationView(int commonRelationId) => this.CommonRelationViews.First(commonRelationView => commonRelationView.ViewModel.Id == commonRelationId);
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
    }
}
