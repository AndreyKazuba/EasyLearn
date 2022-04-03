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
using EasyLearn.Infrastructure.Validation;

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
        private Guid verbPrepositionExistValidationRuleId;
        #endregion

        private bool VerbPrepositionExist
        {
            set
            {
                ValidationPool.Set(ValidationRulesGroup.AddVerbPreposition, verbPrepositionExistValidationRuleId, !value);
            }
        }

#pragma warning disable CS8618
        public EditVerbPrepositionDictionaryPageVM(IVerbPrepositionRepository verbPrepositionRepository, IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository)
        {
            this.verbPrepositionRepository = verbPrepositionRepository;
            this.verbPrepositionDictionaryRepository = verbPrepositionDictionaryRepository;
            this.verbPrepositionExistValidationRuleId = ValidationPool.Register(ValidationRulesGroup.AddVerbPreposition);
        }
#pragma warning restore CS8618

        #region Props for binding
        public ObservableCollection<VerbPrepositionView> VerbPrepositionViews { get; set; }
        public string AddingWindowVerbValue { get; set; }
        public string AddingWindowPrepositionValue { get; set; }
        public string AddingWindowTranslationValue { get; set; }
        public string AddingWindowCommentValue { get; set; }
        public bool IsConfirmVerbPrepositionAddingButtonEnabled { get; set; }
        #endregion

        #region Commands 
        public Command GoBackCommand { get; private set; }
        public Command CreateVerbPrepositionCommand { get; private set; }
        public Command ClearAddingWindowCommand { get; private set; }
        public Command<int> SetDictionaryAsCurrentCommand { get; private set; }
        public Command CheckVerbPrepositionForExistingCommand { get; private set; }
        public Command UpdateConfirmVerbPrepositionAddingButtonAvailabilityCommand { get; private set; }
        protected override void InitCommands()
        {
            this.GoBackCommand = new Command(GoBack);
            this.CreateVerbPrepositionCommand = new Command(async () => await CreateVerbPreposition());
            this.ClearAddingWindowCommand = new Command(ClearAddingWindow);
            this.SetDictionaryAsCurrentCommand = new Command<int>(async verbPrepositionDictionaryId => await SetDictionaryAsCurrent(verbPrepositionDictionaryId));
            this.CheckVerbPrepositionForExistingCommand = new Command(CheckVerbPrepositionForExisting);
            this.UpdateConfirmVerbPrepositionAddingButtonAvailabilityCommand = new Command(UpdateConfirmVerbPrepositionAddingButtonAvailability);
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
            IEnumerable<VerbPrepositionView> verbPrepositionViews = verbPrepositionDictionary.VerbPrepositions.Select(verbPreposition => VerbPrepositionView.Create(verbPreposition));
            this.VerbPrepositionViews = new ObservableCollection<VerbPrepositionView>(verbPrepositionViews);
        }
        private void CheckVerbPrepositionForExisting()
        {
            string verbValue = this.AddingWindowVerbValue;
            string prepositionValue = this.AddingWindowPrepositionValue;
            this.VerbPrepositionExist = this.verbPrepositionRepository.IsVerbPrepositionExist(verbValue, prepositionValue, dictionaryId);
        }
        private void UpdateConfirmVerbPrepositionAddingButtonAvailability() => IsConfirmVerbPrepositionAddingButtonEnabled = ValidationPool.IsValid(ValidationRulesGroup.AddVerbPreposition);
        #endregion

        #region Other private methods
        private void AddVerbPrepositionToUI(VerbPreposition verbPreposition) => this.VerbPrepositionViews.Add(VerbPrepositionView.Create(verbPreposition));
        #endregion
    }
}
