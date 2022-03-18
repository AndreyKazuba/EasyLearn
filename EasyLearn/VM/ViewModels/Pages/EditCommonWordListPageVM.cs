using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLearn.VM.Core;
using EasyLearn.VM.Windows;
using Microsoft.Extensions.DependencyInjection;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Data.Models;
using EasyLearn.Data.Enums;
using System.Collections.ObjectModel;
using EasyLearn.UI.CustomControls;
using EasyLearn.VM.ViewModels.CustomControls;
using System.Windows.Controls;
using System.Windows;
using EasyLearn.VM.ViewModels.ExpandedElements;
using EasyLearn.Infrastructure.Constants;

namespace EasyLearn.VM.ViewModels.Pages
{
    public class EditCommonWordListPageVM : ViewModel
    {
        private readonly ICommonDictionaryRepository commonWordListsRepository;
        private readonly ICommonRelationsRepository commonRelationsRepository;

        private int currentCommonWordListId;

        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<UnitTypeComboBoxItem> RussianUnitTypes { get; set; }
        public ObservableCollection<UnitTypeComboBoxItem> EnglishUnitTypes { get; set; }
        public UnitTypeComboBoxItem SelectedRussianUnitType { get; set; }
        public UnitTypeComboBoxItem SelectedEnglishUnitType { get; set; }
        public string NewEngUnitValue { get; set; }
        public string NewRusUnitValue { get; set; }
        public string Comment { get; set; }

        public ObservableCollection<CommonRelationView> Relations { get; set; }

        #region Commands

        public DelegateCommand GoBack { get; set; }
        public DelegateCommand CreateNewRelation { get; set; }

        protected override void InitCommands()
        {
            this.GoBack = new DelegateCommand(arg =>
            {
                App.ServiceProvider.GetService<AppWindowVM>().OpenListsPage.Execute(arg);
            });
            this.CreateNewRelation = new DelegateCommand(async arg => await AddNewRelation());
        }

        #endregion

        public EditCommonWordListPageVM(ICommonDictionaryRepository commonWordListsRepository, ICommonRelationsRepository commonRelationsRepository)
        {
            this.commonWordListsRepository = commonWordListsRepository;
            this.commonRelationsRepository = commonRelationsRepository;
            SetRussianUnitTypes();
            SetEnglishUnitTypes();
        }

        public async Task SetCurrentList(int listId)
        {
            this.currentCommonWordListId = listId;
            CommonDictionary currentCommonList = await commonWordListsRepository.GetCommonDictionaryAsync(currentCommonWordListId);
            this.Name = currentCommonList.Name;
            this.Description = currentCommonList.Description;
            this.Relations = new ObservableCollection<CommonRelationView>(currentCommonList.Relations.Select(relation => new CommonRelationView(new CommonRelationVM(relation))));
        }

        private async Task AddNewRelation()
        {
            string rusUnitValue = this.NewRusUnitValue;
            string engUnitValue = this.NewEngUnitValue;
            UnitType engUnitType = this.SelectedEnglishUnitType.UnitType;
            UnitType rusUnitType = this.SelectedRussianUnitType.UnitType;
            string comment = this.Comment;
            CommonRelation newRelation = await commonRelationsRepository.CreateRelation(rusUnitValue, rusUnitType, engUnitValue, engUnitType, this.currentCommonWordListId, comment);
            this.Relations.Add(new CommonRelationView(new CommonRelationVM(newRelation)));
        }

        private void SetRussianUnitTypes()
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
                    new UnitTypeComboBoxItem(UnitTypeRussianNames.Adjective, UnitType.Adverb),
                });

            UnitTypeComboBoxItem selectedItem = russianUnitTypes[0];

            this.RussianUnitTypes = russianUnitTypes;
            this.SelectedRussianUnitType = selectedItem;
        }

        private void SetEnglishUnitTypes()
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
                    new UnitTypeComboBoxItem(UnitTypeRussianNames.Adverb, UnitType.Adverb),
                });

            UnitTypeComboBoxItem selectedItem = englishUnitTypes[0];

            this.EnglishUnitTypes = englishUnitTypes;
            this.SelectedEnglishUnitType = selectedItem;
        }
    }
}
