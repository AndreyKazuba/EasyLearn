using EasyLearn.VM.Core;
using Microsoft.Extensions.DependencyInjection;
using EasyLearn.VM.ViewModels.Pages;
using EasyLearn.VM.Windows;
using System;
using EasyLearn.Data.Models;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class CommonDictionaryVM : ViewModel
    {
        private string description;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description
        {
            get { return $"Описание: {description}"; }
            set { description = value; }
        }

        #region Commands

        public DelegateCommand OpenCurrentCommonDictionaryCommand { get; set; }
        public DelegateCommand RemoveCommonDictionaryCommand { get; set; }

        protected override void InitCommands()
        {
            this.OpenCurrentCommonDictionaryCommand = new DelegateCommand(arg => OpenCurrentCommonDictionary());
            this.RemoveCommonDictionaryCommand = new DelegateCommand(arg => RemoveCommonDictionary());
        }
        #endregion

        public CommonDictionaryVM(CommonDictionary commonDictionary)
        {
            this.Name = commonDictionary.Name;
            this.description = commonDictionary.Description;
            this.Id = commonDictionary.Id;
        }

        private void RemoveCommonDictionary()
        {
            DictionariesPageVM? dictionariesPageVM = App.ServiceProvider.GetRequiredService<DictionariesPageVM>();

            if (dictionariesPageVM is not null)
            {
                dictionariesPageVM.RemoveCommonDictionaryCommand.Execute(Id);
            }
            else
            {
                throw new Exception("Something went wrong :(");
            }
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

        private async void SetCurrentDictionary()
        {
            EditCommonDictionaryPageVM? editListPageVM = App.ServiceProvider.GetService<EditCommonDictionaryPageVM>();
            if (editListPageVM is not null)
            {
                await editListPageVM.SetAsCurrentDictionary(Id);
            }
        }

    }
}
