using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.Pages;
using EasyLearn.VM.Windows;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Data.Helpers;
using System.Threading.Tasks;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class CommonDictionaryVM : ViewModel
    {
        #region Public props
        public int Id { get; private set; }
        #endregion

        #region Binding props
        public string Name { get; set; }
        public string EditNameFieldValue { get; set; }
        public bool IsCardFlipped { get; set; }
        #endregion

#pragma warning disable CS8618
        public CommonDictionaryVM(CommonDictionary commonDictionary)
        {
            this.Name = StringHelper.NormalizeRegister(commonDictionary.Name);
            this.Id = commonDictionary.Id;
        }
#pragma warning restore CS8618

        #region Commands
        public Command OpenCurrentCommonDictionaryCommand { get; private set; }
        public Command RemoveCommonDictionaryCommand { get; private set; }
        public Command EditCommonDictionaryCommand { get; private set; }
        public Command SetEditFieldsValueCommand { get; private set; }
        public Command FlipBackAllAnotherCardsCommand { get; private set; }
        protected override void InitCommands()
        {
            OpenCurrentCommonDictionaryCommand = new Command(OpenCurrentCommonDictionary);
            RemoveCommonDictionaryCommand = new Command(RemoveCommonDictionary);
            EditCommonDictionaryCommand = new Command(async () => await EditCommonDictionary());
            SetEditFieldsValueCommand = new Command(SetEditFieldsValue);
            FlipBackAllAnotherCardsCommand = new Command(FlipBackAllAnotherCards);
        }
        private void OpenCurrentCommonDictionary()
        {
            SetCurrentDictionary();
            App.GetService<AppWindowVM>().OpenEditCommonDictionaryPageCommand.Execute();
            App.GetService<AppWindowVM>().SetGoBackButtonCommand.Execute();
        }
        private void RemoveCommonDictionary() => App.GetService<DictionariesPageVM>().DeleteCommonDictionaryCommand.Execute(Id);
        private async Task EditCommonDictionary()
        {
            string newDictionaryName = EditNameFieldValue;
            if (StringHelper.Equals(Name, newDictionaryName))
                return;
            Name = newDictionaryName;
            await App.GetService<ICommonDictionaryRepository>().EditCommonDictionary(Id, newDictionaryName);
        }
        private void SetEditFieldsValue()
        {
            EditNameFieldValue = Name;
        }
        private void FlipBackAllAnotherCards() => App.GetService<DictionariesPageVM>().FlipBackAllCardsCommand.Execute();
        #endregion

        #region Private helpers
        private void SetCurrentDictionary() => App.GetService<EditCommonDictionaryPageVM>().SetDictionaryCommand.Execute(Id);
        #endregion
    }
}
