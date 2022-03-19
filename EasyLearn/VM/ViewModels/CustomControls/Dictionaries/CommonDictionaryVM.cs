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
        private readonly ICommonDictionaryRepository commonDictionaryRepository;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EditNameFieldValue { get; set; }
        public string EditDescriptionFieldValue { get; set; }
        public bool IsCardFlipped { get; set; }

        #region Commands

        public DelegateCommand OpenCurrentCommonDictionaryCommand { get; private set; }
        public DelegateCommand RemoveCommonDictionaryCommand { get; private set; }
        public DelegateCommand EditCommonDictionaryCommand { get; private set; }
        public DelegateCommand SetEditFieldsValueCommand { get; private set; }
        public DelegateCommand FlipBackAllAnotherCardsCommand { get; private set; }


        protected override void InitCommands()
        {
            this.OpenCurrentCommonDictionaryCommand = new DelegateCommand(arg => OpenCurrentCommonDictionary());
            this.RemoveCommonDictionaryCommand = new DelegateCommand(arg => RemoveCommonDictionary());
            this.EditCommonDictionaryCommand = new DelegateCommand(async arg => await EditCommonDictionary());
            this.SetEditFieldsValueCommand = new DelegateCommand(arg => SetEditFieldsValue());
            this.FlipBackAllAnotherCardsCommand = new DelegateCommand(arg => FlipBackAllAnotherCards());
        }
        #endregion

        public CommonDictionaryVM(CommonDictionary commonDictionary)
        {
            this.Name = commonDictionary.Name;
            this.Description = commonDictionary.Description;
            this.Id = commonDictionary.Id;

            ICommonDictionaryRepository? commonDictionaryRepository = App.ServiceProvider.GetService<ICommonDictionaryRepository>();
            if (commonDictionaryRepository is not null)
                this.commonDictionaryRepository = commonDictionaryRepository;
            else
                throw new Exception("Something went wrong");
        }
        private void RemoveCommonDictionary() => GetDictionariesPageVM().RemoveCommonDictionaryCommand.Execute(Id);
        private async Task EditCommonDictionary()
        {
            string newDictionaryName = this.EditNameFieldValue;
            string newDictionaryDescription = this.EditDescriptionFieldValue;
            if (string.IsNullOrWhiteSpace(newDictionaryName) || string.IsNullOrWhiteSpace(newDictionaryDescription))
                throw new Exception("Stomething went wrong");
            if (StringHelper.Equals(this.Name, newDictionaryName) && StringHelper.Equals(this.Description, newDictionaryDescription))
                return;
            this.Name = newDictionaryName;
            this.Description = newDictionaryDescription;
            await commonDictionaryRepository.EditCommonDictionary(this.Id, newDictionaryName, newDictionaryDescription);
        }

        private void OpenCurrentCommonDictionary()
        {
            SetCurrentDictionary();
            AppWindowVM? appWindowVM = App.ServiceProvider.GetService<AppWindowVM>();

            if (appWindowVM is not null)
            {
                appWindowVM.OpenEditCommonDictionaryPageCommand.Execute();
            }
            else
            {
                throw new Exception("Something went wrong :(");
            }
        }
        private void FlipBackAllAnotherCards() => GetDictionariesPageVM().FlipBackAllCardsCommand.Execute();
        private async void SetCurrentDictionary()
        {
            EditCommonDictionaryPageVM? editListPageVM = App.ServiceProvider.GetService<EditCommonDictionaryPageVM>();
            if (editListPageVM is not null)
            {
                await editListPageVM.SetAsCurrentDictionary(Id);
            }
        }
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
