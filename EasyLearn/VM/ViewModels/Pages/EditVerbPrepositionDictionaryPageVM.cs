﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.UI.CustomControls;
using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.CustomControls;
using EasyLearn.VM.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace EasyLearn.VM.ViewModels.Pages
{
    public class EditVerbPrepositionDictionaryPageVM : ViewModel
    {
        #region Private fields
        private int currentVerbPrepositionListId;
        private readonly IVerbPrepositionRepository verbPrepositionRepository;
        private readonly IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository;
        #endregion

        #region Props for binding
        public ObservableCollection<VerbPrepositionView> VerbPrepositions { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string NewVerbValue { get; set; }
        public string NewPrepositionValue { get; set; }
        public string Comment { get; set; }
        public string Translation { get; set; }
        #endregion

        #region Commands 
        public DelegateCommand GoBack { get; private set; }
        public DelegateCommand CreateVerbPrepositionCommand { get; private set; }
        public DelegateCommand CleanVerbPrepositionCreationWindowCommand { get; private set; }
        protected override void InitCommands()
        {
            this.GoBack = new DelegateCommand(arg => App.GetService<AppWindowVM>().OpenListsPage.Execute());
            this.CleanVerbPrepositionCreationWindowCommand = new DelegateCommand(arg => CleanVerbPrepositionCreationWindow());
            this.CreateVerbPrepositionCommand = new DelegateCommand(async arg => await CreateVerbPreposition());
        }
        #endregion

        public EditVerbPrepositionDictionaryPageVM(IVerbPrepositionRepository verbPrepositionsRepository, IVerbPrepositionDictionaryRepository verbPrepositionListsRepository)
        {
            this.verbPrepositionRepository = verbPrepositionsRepository;
            this.verbPrepositionDictionaryRepository = verbPrepositionListsRepository;
        }

        private async Task CreateVerbPreposition()
        {
            string prepositionValue = this.NewPrepositionValue;
            string verbValue = this.NewVerbValue;
            string translation = this.Translation;
            string? comment = StringHelper.IsEmptyOrWhiteSpace(this.Comment) ? null : this.Comment;
            VerbPreposition newVerbPreposition = await verbPrepositionRepository.CreateVerbPreposition(verbValue, prepositionValue, currentVerbPrepositionListId, translation, comment);
            this.VerbPrepositions.Add(new VerbPrepositionView(new VerbPrepositionVM(newVerbPreposition)));
        }

        private void CleanVerbPrepositionCreationWindow()
        {
            this.NewVerbValue = String.Empty;
            this.NewPrepositionValue = String.Empty;
            this.Comment = String.Empty;
        }

        public async Task SetAsCurrentDictionary(int dictionaryId)
        {
            this.currentVerbPrepositionListId = dictionaryId;
            VerbPrepositionDictionnary verbPrepositionList = await verbPrepositionDictionaryRepository.GetVerbPrepositionDictionaryAsync(currentVerbPrepositionListId);
            this.Name = verbPrepositionList.Name;
            this.Description = verbPrepositionList.Description;
            this.VerbPrepositions = new ObservableCollection<VerbPrepositionView>(verbPrepositionList.VerbPrepositions.Select(verbPreposition => new VerbPrepositionView(new VerbPrepositionVM(verbPreposition))));
        }
    }
}
