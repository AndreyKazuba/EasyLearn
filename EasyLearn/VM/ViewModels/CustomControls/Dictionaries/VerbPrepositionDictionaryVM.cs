using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.Pages;
using EasyLearn.VM.Windows;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class VerbPrepositionDictionaryVM : ViewModel
    {
        private readonly IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EditNameFieldValue { get; set; }
        public string EditDescriptionFieldValue { get; set; }
        public bool IsCardFlipped { get; set; }


        #region Commands

        public DelegateCommand OpenCurrentVerbPrepositionDictionaryCommand { get; private set; }
        public DelegateCommand RemoveVerbPrepositionDictionaryCommand { get; private set; }
        public DelegateCommand EditVerbPrepositionDictionaryCommand { get; private set; }
        public DelegateCommand SetEditFieldsValueCommand { get; private set; }
        public DelegateCommand FlipBackAllAnotherCardsCommand { get; private set; }

        protected override void InitCommands()
        {
            this.OpenCurrentVerbPrepositionDictionaryCommand = new DelegateCommand(arg => OpenCurrentVerbPrepositionDictionary());
            this.RemoveVerbPrepositionDictionaryCommand = new DelegateCommand(arg => RemoveVerbPrepositionDictionary());
            this.SetEditFieldsValueCommand = new DelegateCommand(arg => SetEditFieldsValue());
            this.EditVerbPrepositionDictionaryCommand = new DelegateCommand(async arg => await EditVerbPrepositionDictionary());
            this.FlipBackAllAnotherCardsCommand = new DelegateCommand(arg => FlipBackAllAnotherCards());
        }
        #endregion

        public VerbPrepositionDictionaryVM(VerbPrepositionDictionnary verbPrepositionDictionnary)
        {
            this.Name = verbPrepositionDictionnary.Name;
            this.Description = verbPrepositionDictionnary.Description;
            this.Id = verbPrepositionDictionnary.Id;

            IVerbPrepositionDictionaryRepository? verbPrepositionDictionaryRepository = App.ServiceProvider.GetService<IVerbPrepositionDictionaryRepository>();
            if (verbPrepositionDictionaryRepository is not null)
                this.verbPrepositionDictionaryRepository = verbPrepositionDictionaryRepository;
            else
                throw new Exception("Something went wrong");
        }

        private void FlipBackAllAnotherCards() => GetDictionariesPageVM().FlipBackAllCardsCommand.Execute();
        private void RemoveVerbPrepositionDictionary()
        {
            DictionariesPageVM? dictionariesPageVM = App.ServiceProvider.GetService<DictionariesPageVM>();

            if (dictionariesPageVM is not null)
            {
                dictionariesPageVM.RemoveVerbPrepositionDictionaryCommand.Execute(Id);
            }
            else
            {
                throw new Exception("Something went wrong :(");
            }
        }

        private void OpenCurrentVerbPrepositionDictionary()
        {
            SetCurrentDictionary();
            AppWindowVM? appWindowVM = App.ServiceProvider.GetService<AppWindowVM>();

            if (appWindowVM is not null)
            {
                appWindowVM.OpenEditVerbPrepositionDictionaryPageCommand.Execute();
            }
            else
            {
                throw new Exception("Something went wrong :(");
            }
        }
        private async Task EditVerbPrepositionDictionary()
        {
            string newDictionaryName = this.EditNameFieldValue;
            string newDictionaryDescription = this.EditDescriptionFieldValue;
            if (string.IsNullOrWhiteSpace(newDictionaryName) || string.IsNullOrWhiteSpace(newDictionaryDescription))
                throw new Exception("Stomething went wrong");
            if (StringHelper.Equals(this.Name, newDictionaryName) && StringHelper.Equals(this.Description, newDictionaryDescription))
                return;
            this.Name = newDictionaryName;
            this.Description = newDictionaryDescription;
            await verbPrepositionDictionaryRepository.EditVerbPrepositionDictionary(this.Id, newDictionaryName, newDictionaryDescription);
        }

        private async void SetCurrentDictionary()
        {
            EditVerbPrepositionDictionaryPageVM? editVerbPrepositionDictionaryPageVM = App.ServiceProvider.GetService<EditVerbPrepositionDictionaryPageVM>();
            if (editVerbPrepositionDictionaryPageVM is not null)
            {
                await editVerbPrepositionDictionaryPageVM.SetAsCurrentDictionary(Id);
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
