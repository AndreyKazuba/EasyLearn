using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.Pages;
using EasyLearn.VM.Windows;
using System.Threading.Tasks;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class VerbPrepositionDictionaryVM : ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EditNameFieldValue { get; set; }
        public string EditDescriptionFieldValue { get; set; }
        public bool IsCardFlipped { get; set; }

        #region Commands
        public Command OpenCurrentVerbPrepositionDictionaryCommand { get; private set; }
        public Command RemoveVerbPrepositionDictionaryCommand { get; private set; }
        public Command EditVerbPrepositionDictionaryCommand { get; private set; }
        public Command SetEditFieldsValueCommand { get; private set; }
        public Command FlipBackAllAnotherCardsCommand { get; private set; }
        protected override void InitCommands()
        {
            this.OpenCurrentVerbPrepositionDictionaryCommand = new Command(OpenCurrentVerbPrepositionDictionary);
            this.RemoveVerbPrepositionDictionaryCommand = new Command(RemoveVerbPrepositionDictionary);
            this.SetEditFieldsValueCommand = new Command(SetEditFieldsValue);
            this.EditVerbPrepositionDictionaryCommand = new Command(async () => await EditVerbPrepositionDictionary());
            this.FlipBackAllAnotherCardsCommand = new Command(FlipBackAllAnotherCards);
        }
        #endregion

        public VerbPrepositionDictionaryVM(VerbPrepositionDictionnary verbPrepositionDictionnary)
        {
            this.Name = StringHelper.NormalizeRegister(verbPrepositionDictionnary.Name);
            this.Description = verbPrepositionDictionnary.Description.TryNormalizeRegister().EmptyIfNull();
            this.Id = verbPrepositionDictionnary.Id;
        }

        private void FlipBackAllAnotherCards() => App.GetService<DictionariesPageVM>().FlipBackAllCardsCommand.Execute();
        private void RemoveVerbPrepositionDictionary() => App.GetService<DictionariesPageVM>().DeleteVerbPrepositionDictionaryCommand.Execute(Id);
        private void OpenCurrentVerbPrepositionDictionary()
        {
            SetCurrentDictionary();
            App.GetService<AppWindowVM>().OpenEditVerbPrepositionDictionaryPageCommand.Execute();
            App.GetService<AppWindowVM>().SetGoBackButtonCommand.Execute();
        }
        private async Task EditVerbPrepositionDictionary()
        {
            string newDictionaryName = this.EditNameFieldValue;
            string newDictionaryDescription = this.EditDescriptionFieldValue;
            if (StringHelper.Equals(this.Name, newDictionaryName) && StringHelper.Equals(this.Description, newDictionaryDescription))
                return;
            this.Name = newDictionaryName;
            this.Description = newDictionaryDescription;
            await App.GetService<IVerbPrepositionDictionaryRepository>().EditVerbPrepositionDictionary(this.Id, newDictionaryName,StringHelper.NullIfEmptyOrWhiteSpace(newDictionaryDescription));
        }

        private void SetCurrentDictionary() => App.GetService<EditVerbPrepositionDictionaryPageVM>().SetDictionaryAsCurrentCommand.Execute(Id);

        private void SetEditFieldsValue()
        {
            this.EditDescriptionFieldValue = this.Description;
            this.EditNameFieldValue = this.Name;
        }
    }
}
