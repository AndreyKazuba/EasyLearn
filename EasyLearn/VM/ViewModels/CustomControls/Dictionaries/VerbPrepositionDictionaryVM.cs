using EasyLearn.Data.Constants;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.Pages;
using EasyLearn.VM.Windows;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class VerbPrepositionDictionaryVM : ViewModel
    {
        #region Private fields
        private string lastValidDictionaryName;
        #endregion

        #region Public props
        public int Id { get; private set; }
        public int Order { get; private set; }
        #endregion

        #region Binding props
        public string Name { get; set; }
        public string EditNameFieldValue { get; set; }
        public bool IsCardFlipped { get; set; }
        public string TotalDictionaryVerbPrepositionsCount { get; set; }
        public string DictionaryStudiedVerbPrepositionsCount { get; set; }
        public string DictionaryLeftToLearnVerbPrepositionsCount { get; set; }
        public int TotalDictionaryProgress { get; set; }
        #endregion

#pragma warning disable CS8618
        public VerbPrepositionDictionaryVM(VerbPrepositionDictionnary verbPrepositionDictionnary)
        {
            int totalDictionaryVerbPrepositionsCount = verbPrepositionDictionnary.VerbPrepositions.Count;
            int dictionaryStudiedVerbPrepositionsCount = verbPrepositionDictionnary.VerbPrepositions.Count(verbPreposition => verbPreposition.Studied);
            int dictionaryLeftToLearnVerbPrepositionsCount = totalDictionaryVerbPrepositionsCount - dictionaryStudiedVerbPrepositionsCount;
            Name = StringHelper.NormalizeRegister(verbPrepositionDictionnary.Name);
            Id = verbPrepositionDictionnary.Id;
            TotalDictionaryVerbPrepositionsCount = totalDictionaryVerbPrepositionsCount.ToString();
            DictionaryStudiedVerbPrepositionsCount = dictionaryStudiedVerbPrepositionsCount.ToString();
            DictionaryLeftToLearnVerbPrepositionsCount = dictionaryLeftToLearnVerbPrepositionsCount.ToString();
            Order = verbPrepositionDictionnary.VerbPrepositions.Count;
            lastValidDictionaryName = Name;
            SetTotalDictionaryProgress(verbPrepositionDictionnary);
        }
#pragma warning disable CS8618

        #region Commands
        public Command OpenCurrentVerbPrepositionDictionaryCommand { get; private set; }
        public Command RemoveVerbPrepositionDictionaryCommand { get; private set; }
        public Command SetEditFieldsValueCommand { get; private set; }
        public Command EditVerbPrepositionDictionaryCommand { get; private set; }
        public Command FlipBackAllAnotherCardsCommand { get; private set; }
        public Command SaveLastValidDictionaryNameCommand { get; private set; }
        protected override void InitCommands()
        {
            OpenCurrentVerbPrepositionDictionaryCommand = new Command(OpenCurrentVerbPrepositionDictionary);
            RemoveVerbPrepositionDictionaryCommand = new Command(RemoveVerbPrepositionDictionary);
            SetEditFieldsValueCommand = new Command(SetEditFieldsValue);
            EditVerbPrepositionDictionaryCommand = new Command(async () => await EditVerbPrepositionDictionary());
            FlipBackAllAnotherCardsCommand = new Command(FlipBackAllAnotherCards);
            SaveLastValidDictionaryNameCommand = new Command(SaveLastValidDictionaryName);
        }
        private void OpenCurrentVerbPrepositionDictionary()
        {
            SetCurrentDictionary();
            App.GetService<AppWindowVM>().OpenEditVerbPrepositionDictionaryPageCommand.Execute();
            App.GetService<AppWindowVM>().SetGoBackButtonCommand.Execute();
        }
        private void RemoveVerbPrepositionDictionary() => App.GetService<DictionariesPageVM>().OpenDeleteVerbPrepositionDictionaryWindowCommand.Execute(Id);
        private void SetEditFieldsValue()
        {
            EditNameFieldValue = Name;
        }
        private async Task EditVerbPrepositionDictionary()
        {
            string newDictionaryName = lastValidDictionaryName.Prepare().NormalizeRegister();
            if (StringHelper.Equals(Name, newDictionaryName))
                return;
            Name = newDictionaryName;
            await App.GetService<IVerbPrepositionDictionaryRepository>().EditVerbPrepositionDictionary(Id, newDictionaryName);
        }
        private void FlipBackAllAnotherCards() => App.GetService<DictionariesPageVM>().FlipBackAllCardsCommand.Execute();
        private void SaveLastValidDictionaryName()
        {
            if (EditNameFieldValue.Length >= ModelConstants.DictionaryNameMinLength && EditNameFieldValue.Length <= ModelConstants.DictionaryNameMaxLength)
                lastValidDictionaryName = EditNameFieldValue;
        }
        #endregion

        #region Private helpers
        private void SetCurrentDictionary() => App.GetService<EditVerbPrepositionDictionaryPageVM>().SetDictionaryCommand.Execute(Id);
        private void SetTotalDictionaryProgress(VerbPrepositionDictionnary verbPrepositionDictionnary)
        {
            int hundredPercentValue = verbPrepositionDictionnary.VerbPrepositions.Count * 100;
            int ratingCurrentValue = verbPrepositionDictionnary.VerbPrepositions.Sum(verbPreposition => verbPreposition.Rating);
            int ratingTotalValue = (int)(ratingCurrentValue * (100d / hundredPercentValue) * 0.8);
            int studiedCurrentValue = verbPrepositionDictionnary.VerbPrepositions.Count(verbPreposition => verbPreposition.Studied) * 100;
            int studiedTotalValue = (int)(studiedCurrentValue * (100d / hundredPercentValue) * 0.2);
            TotalDictionaryProgress = ratingTotalValue + studiedTotalValue;
        }
        #endregion
    }
}
