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
    public class EditListPageVM : ViewModel
    {
        private readonly ICommonWordListsRepository commonWordListsRepository;

        private int currentCommonListId;

        public string Name { get; set; }
        public string Description { get; set; }
        public CommonWordListType Type { get; set; }
        public ObservableCollection<UnitTypeComboBoxItem> RussianUnitTypes { get; set; }
        public UnitTypeComboBoxItem SelectedRussianUnitType { get; set; }
        public ObservableCollection<UnitTypeComboBoxItem> EnglishUnitTypes { get; set; }
        public UnitTypeComboBoxItem SelectedEnglishUnitType { get; set; }

        public ObservableCollection<Relation> Relations { get; set; }

        #region Commands

        public DelegateCommand GoBack { get; set; }

        protected override void InitCommands()
        {
            this.GoBack = new DelegateCommand(arg =>
            {
                App.ServiceProvider.GetService<AppWindowVM>().OpenListsPage.Execute(arg);
            });
        }

        #endregion

        public EditListPageVM(ICommonWordListsRepository commonWordListsRepository)
        {
            this.commonWordListsRepository = commonWordListsRepository;
            SetRussianUnitTypes();
            SetEnglishUnitTypes();
        }

        public async Task SetCurrentList(int listId)
        {
            this.currentCommonListId = listId;
            CommonWordList currentCommonList = await commonWordListsRepository.GetCommonWordListAsync(currentCommonListId);
            this.Name = currentCommonList.Name;
            this.Description = currentCommonList.Description;
            this.Type = currentCommonList.Type;
            this.Relations = new ObservableCollection<Relation>(currentCommonList.Relations.Select(relation => new Relation(new RelationVM())));
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
