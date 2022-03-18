using EasyLearn.Data.Models;
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
        private string description;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description
        {
            get { return $"Описание: {description}"; }
            set { description = value; }
        }

        #region Commands

        public DelegateCommand OpenCurrentVerbPrepositionDictionaryCommand { get; set; }
        public DelegateCommand RemoveVerbPrepositionDictionaryCommand { get; set; }

        protected override void InitCommands()
        {
            this.OpenCurrentVerbPrepositionDictionaryCommand = new DelegateCommand(arg => OpenCurrentVerbPrepositionDictionary());
            this.RemoveVerbPrepositionDictionaryCommand = new DelegateCommand(arg => RemoveVerbPrepositionDictionary());
        }
        #endregion

        public VerbPrepositionDictionaryVM(VerbPrepositionDictionnary verbPrepositionDictionnary)
        {
            this.Name = verbPrepositionDictionnary.Name;
            this.description = verbPrepositionDictionnary.Description;
            this.Id = verbPrepositionDictionnary.Id;
        }

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

        private async void SetCurrentDictionary()
        {
            EditVerbPrepositionDictionaryPageVM? editVerbPrepositionDictionaryPageVM = App.ServiceProvider.GetService<EditVerbPrepositionDictionaryPageVM>();
            if (editVerbPrepositionDictionaryPageVM is not null)
            {
                await editVerbPrepositionDictionaryPageVM.SetAsCurrentDictionary(Id);
            }
        }
    }
}
