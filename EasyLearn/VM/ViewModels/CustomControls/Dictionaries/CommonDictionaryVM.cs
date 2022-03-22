using EasyLearn.VM.Core;
using Microsoft.Extensions.DependencyInjection;
using EasyLearn.VM.ViewModels.Pages;
using EasyLearn.VM.Windows;
using System;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Data.Helpers;
using System.Threading.Tasks;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class CommonDictionaryVM : ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EditNameFieldValue { get; set; }
        public string EditDescriptionFieldValue { get; set; }
        public bool IsCardFlipped { get; set; }

        #region Commands
        public Command OpenCurrentCommonDictionaryCommand { get; private set; }
        public Command RemoveCommonDictionaryCommand { get; private set; }
        public Command EditCommonDictionaryCommand { get; private set; }
        public Command SetEditFieldsValueCommand { get; private set; }
        public Command FlipBackAllAnotherCardsCommand { get; private set; }
        protected override void InitCommands()
        {
            this.OpenCurrentCommonDictionaryCommand = new Command(OpenCurrentCommonDictionary);
            this.RemoveCommonDictionaryCommand = new Command(RemoveCommonDictionary);
            this.EditCommonDictionaryCommand = new Command(async () => await EditCommonDictionary());
            this.SetEditFieldsValueCommand = new Command(SetEditFieldsValue);
            this.FlipBackAllAnotherCardsCommand = new Command(FlipBackAllAnotherCards);
        }
        #endregion

        public CommonDictionaryVM(CommonDictionary commonDictionary)
        {
            this.Name = StringHelper.NormalizeRegister(commonDictionary.Name);
            this.Description = StringHelper.NormalizeRegister(commonDictionary.Description);
            this.Id = commonDictionary.Id;
        }
        private void RemoveCommonDictionary() => GetDictionariesPageVM().DeleteCommonDictionaryCommand.Execute(Id);
        private async Task EditCommonDictionary()
        {
            string newDictionaryName = this.EditNameFieldValue;
            string newDictionaryDescription = this.EditDescriptionFieldValue;
            if (StringHelper.Equals(this.Name, newDictionaryName) && StringHelper.Equals(this.Description, newDictionaryDescription))
                return;
            this.Name = newDictionaryName;
            this.Description = newDictionaryDescription;
            await App.GetService<ICommonDictionaryRepository>().EditCommonDictionary(this.Id, newDictionaryName, newDictionaryDescription);
        }

        private void OpenCurrentCommonDictionary()
        {
            SetCurrentDictionary();
            App.GetService<AppWindowVM>().OpenEditCommonDictionaryPageCommand.Execute();
        }
        private void FlipBackAllAnotherCards() => GetDictionariesPageVM().FlipBackAllCardsCommand.Execute();
        private void SetCurrentDictionary() => App.GetService<EditCommonDictionaryPageVM>().SetDictionaryAsCurrentCommand.Execute(Id);
        private void SetEditFieldsValue()
        {
            this.EditDescriptionFieldValue = this.Description;
            this.EditNameFieldValue = this.Name;
        }
        private DictionariesPageVM GetDictionariesPageVM()
        {
            DictionariesPageVM? dictionariesPageVM = App.ServiceProvider.GetRequiredService<DictionariesPageVM>();
            if (dictionariesPageVM is not null)
                return dictionariesPageVM;
            else
                throw new Exception("Something went wrong :(");
        }
    }
}
