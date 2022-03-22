using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.UI.CustomControls;
using EasyLearn.VM.Core;
using EasyLearn.VM.Windows;

namespace EasyLearn.VM.ViewModels.Pages
{
    public class EditVerbPrepositionDictionaryPageVM : ViewModel
    {
        #region Repositories
        private readonly IVerbPrepositionRepository verbPrepositionRepository;
        private readonly IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository;
        #endregion

        #region Private fields
        private int dictionaryId;
        #endregion

#pragma warning disable CS8618
        public EditVerbPrepositionDictionaryPageVM(IVerbPrepositionRepository verbPrepositionRepository, IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository)
        {
            this.verbPrepositionRepository = verbPrepositionRepository;
            this.verbPrepositionDictionaryRepository = verbPrepositionDictionaryRepository;
        }
#pragma warning restore CS8618

        #region Props for binding
        public ObservableCollection<VerbPrepositionView> VerbPrepositionViews { get; set; }
        public string DictionaryName { get; set; }
        public string DictionaryDescription { get; set; }
        public string AddingWindowVerbValue { get; set; }
        public string AddingWindowPrepositionValue { get; set; }
        public string AddingWindowCommentValue { get; set; }
        public string AddingWindowTranslationValue { get; set; }
        #endregion

        #region Commands 
        public Command GoBackCommand { get; private set; }
        public Command CreateVerbPrepositionCommand { get; private set; }
        public Command ClearAddingWindowCommand { get; private set; }
        public Command<int> SetDictionaryAsCurrentCommand { get; private set; }
        protected override void InitCommands()
        {
            this.GoBackCommand = new Command(arg => GoBack());
            this.CreateVerbPrepositionCommand = new Command(async arg => await CreateVerbPreposition());
            this.ClearAddingWindowCommand = new Command(arg => ClearAddingWindow());
            this.SetDictionaryAsCurrentCommand = new Command<int>(async verbPrepositionDictionaryId => await SetDictionaryAsCurrent(verbPrepositionDictionaryId));
        }
        #endregion

        #region Command logic methods
        private void GoBack() => App.GetService<AppWindowVM>().OpenDictionariesPageCommand.Execute();
        private async Task CreateVerbPreposition()
        {
            string prepositionValue = this.AddingWindowPrepositionValue;
            string verbValue = this.AddingWindowVerbValue;
            string translation = this.AddingWindowTranslationValue;
            string? comment = StringHelper.NullIfEmptyOrWhiteSpace(this.AddingWindowCommentValue);
            int verbPrepositionDictionaryId = this.dictionaryId;
            VerbPreposition newVerbPreposition = await verbPrepositionRepository.CreateVerbPreposition(verbValue, prepositionValue, verbPrepositionDictionaryId, translation, comment);
            AddVerbPrepositionToUI(newVerbPreposition);
        }
        private void ClearAddingWindow()
        {
            this.AddingWindowVerbValue = String.Empty;
            this.AddingWindowPrepositionValue = String.Empty;
            this.AddingWindowCommentValue = String.Empty;
            this.AddingWindowTranslationValue = String.Empty;
        }
        private async Task SetDictionaryAsCurrent(int verbPrepositionDictionaryId)
        {
            VerbPrepositionDictionnary verbPrepositionDictionary = await verbPrepositionDictionaryRepository.GetVerbPrepositionDictionaryAsync(verbPrepositionDictionaryId);
            this.dictionaryId = verbPrepositionDictionaryId;
            this.DictionaryName = verbPrepositionDictionary.Name;
            this.DictionaryDescription = StringHelper.EmptyIfNull(verbPrepositionDictionary.Description);
            IEnumerable<VerbPrepositionView> verbPrepositionViews = verbPrepositionDictionary.VerbPrepositions.Select(verbPreposition => VerbPrepositionView.Create(verbPreposition));
            this.VerbPrepositionViews = new ObservableCollection<VerbPrepositionView>(verbPrepositionViews);
        }
        #endregion

        #region Other private methods
        private void AddVerbPrepositionToUI(VerbPreposition verbPreposition) => this.VerbPrepositionViews.Add(VerbPrepositionView.Create(verbPreposition));
        #endregion
    }
}
