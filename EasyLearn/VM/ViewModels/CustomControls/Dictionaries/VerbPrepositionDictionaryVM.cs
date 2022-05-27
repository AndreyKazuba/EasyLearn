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
        #region Public props
        public int Id { get; private set; }
        #endregion

        #region Binding props
        public string Name { get; set; }
        public string Description { get; set; }
        public string EditNameFieldValue { get; set; }
        public string EditDescriptionFieldValue { get; set; }
        public bool IsCardFlipped { get; set; }
        #endregion

#pragma warning disable CS8618
        public VerbPrepositionDictionaryVM(VerbPrepositionDictionnary verbPrepositionDictionnary)
        {
            this.Name = StringHelper.NormalizeRegister(verbPrepositionDictionnary.Name);
            this.Description = verbPrepositionDictionnary.Description.TryNormalizeRegister().EmptyIfNull();
            this.Id = verbPrepositionDictionnary.Id;
        }
#pragma warning disable CS8618

        #region Commands
        public Command OpenCurrentVerbPrepositionDictionaryCommand { get; private set; }
        public Command RemoveVerbPrepositionDictionaryCommand { get; private set; }
        public Command SetEditFieldsValueCommand { get; private set; }
        public Command EditVerbPrepositionDictionaryCommand { get; private set; }
        public Command FlipBackAllAnotherCardsCommand { get; private set; }
        protected override void InitCommands()
        {
            OpenCurrentVerbPrepositionDictionaryCommand = new Command(OpenCurrentVerbPrepositionDictionary);
            RemoveVerbPrepositionDictionaryCommand = new Command(RemoveVerbPrepositionDictionary);
            SetEditFieldsValueCommand = new Command(SetEditFieldsValue);
            EditVerbPrepositionDictionaryCommand = new Command(async () => await EditVerbPrepositionDictionary());
            FlipBackAllAnotherCardsCommand = new Command(FlipBackAllAnotherCards);
        }
        private void OpenCurrentVerbPrepositionDictionary()
        {
            SetCurrentDictionary();
            App.GetService<AppWindowVM>().OpenEditVerbPrepositionDictionaryPageCommand.Execute();
            App.GetService<AppWindowVM>().SetGoBackButtonCommand.Execute();
        }
        private void RemoveVerbPrepositionDictionary() => App.GetService<DictionariesPageVM>().DeleteVerbPrepositionDictionaryCommand.Execute(Id);
        private void SetEditFieldsValue()
        {
            EditDescriptionFieldValue = Description;
            EditNameFieldValue = Name;
        }
        private async Task EditVerbPrepositionDictionary()
        {
            string newDictionaryName = EditNameFieldValue;
            string newDictionaryDescription = EditDescriptionFieldValue;
            if (StringHelper.Equals(Name, newDictionaryName) && StringHelper.Equals(Description, newDictionaryDescription))
                return;
            Name = newDictionaryName;
            Description = newDictionaryDescription;
            await App.GetService<IVerbPrepositionDictionaryRepository>().EditVerbPrepositionDictionary(Id, newDictionaryName, StringHelper.NullIfEmptyOrWhiteSpace(newDictionaryDescription));
        }
        private void FlipBackAllAnotherCards() => App.GetService<DictionariesPageVM>().FlipBackAllCardsCommand.Execute();
        #endregion

        #region Private helpers
        private void SetCurrentDictionary() => App.GetService<EditVerbPrepositionDictionaryPageVM>().SetDictionaryCommand.Execute(Id);
        #endregion
    }
}
