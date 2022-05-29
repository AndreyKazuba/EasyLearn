using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.Pages;
using EasyLearn.VM.Windows;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Data.Helpers;
using System.Threading.Tasks;
using System.Linq;
using EasyLearn.Data.Constants;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class CommonDictionaryVM : ViewModel
    {
        #region Private fields
        private string lastValidDictionaryName;
        #endregion

        #region Public props
        public int Id { get; private set; }
        #endregion

        #region Binding props
        public string Name { get; set; }
        public string EditNameFieldValue { get; set; }
        public string TotalDictionaryRelationsCount { get; set; }
        public string DictionaryStudiedRelationsCount { get; set; }
        public string DictionaryLeftToLearnRelationsCount { get; set; }
        public int TotalDictionaryProgress { get; set; }
        public bool IsCardFlipped { get; set; }
        #endregion

#pragma warning disable CS8618
        public CommonDictionaryVM(CommonDictionary commonDictionary)
        {
            int totalDictionaryRelationsCount = commonDictionary.Relations.Count;
            int dictionaryStudiedRelationsCount = commonDictionary.Relations.Count(commonRelation => commonRelation.Studied);
            int dictionaryLeftToLearnRelationsCount = totalDictionaryRelationsCount - dictionaryStudiedRelationsCount;
            Name = StringHelper.NormalizeRegister(commonDictionary.Name);
            Id = commonDictionary.Id;
            TotalDictionaryRelationsCount = totalDictionaryRelationsCount.ToString();
            DictionaryStudiedRelationsCount = dictionaryStudiedRelationsCount.ToString();
            DictionaryLeftToLearnRelationsCount = dictionaryLeftToLearnRelationsCount.ToString();
            SetTotalDictionaryProgress(commonDictionary);
        }
#pragma warning restore CS8618

        #region Commands
        public Command OpenCurrentCommonDictionaryCommand { get; private set; }
        public Command RemoveCommonDictionaryCommand { get; private set; }
        public Command UpdateCommonDictionaryCommand { get; private set; }
        public Command SetUpdateFieldsValueCommand { get; private set; }
        public Command FlipBackAllAnotherCardsCommand { get; private set; }
        public Command SaveLastValidDictionaryNameCommand { get; private set; }
        protected override void InitCommands()
        {
            OpenCurrentCommonDictionaryCommand = new Command(OpenCurrentCommonDictionary);
            RemoveCommonDictionaryCommand = new Command(RemoveCommonDictionary);
            UpdateCommonDictionaryCommand = new Command(async () => await UpdateCommonDictionary());
            SetUpdateFieldsValueCommand = new Command(SetUpdateFieldsValue);
            FlipBackAllAnotherCardsCommand = new Command(FlipBackAllAnotherCards);
            SaveLastValidDictionaryNameCommand = new Command(SaveLastValidDictionaryName);
        }
        private void OpenCurrentCommonDictionary()
        {
            SetCurrentDictionary();
            App.GetService<AppWindowVM>().OpenEditCommonDictionaryPageCommand.Execute();
            App.GetService<AppWindowVM>().SetGoBackButtonCommand.Execute();
        }
        private void RemoveCommonDictionary() => App.GetService<DictionariesPageVM>().OpenDeleteCommonDictionaryWindowCommand.Execute(Id);
        private async Task UpdateCommonDictionary()
        {
            string newDictionaryName = lastValidDictionaryName.Prepare().NormalizeRegister();
            if (StringHelper.Equals(Name, newDictionaryName))
                return;
            Name = newDictionaryName;
            await App.GetService<ICommonDictionaryRepository>().EditCommonDictionary(Id, newDictionaryName);
        }
        private void SetUpdateFieldsValue()
        {
            EditNameFieldValue = Name;
        }
        private void FlipBackAllAnotherCards() => App.GetService<DictionariesPageVM>().FlipBackAllCardsCommand.Execute();
        private void SaveLastValidDictionaryName()
        {
            if (EditNameFieldValue.Length >= ModelConstants.DictionaryNameMinLength && EditNameFieldValue.Length <= ModelConstants.DictionaryNameMaxLength)
                lastValidDictionaryName = EditNameFieldValue;
        }
        #endregion

        #region Private helpers
        private void SetCurrentDictionary() => App.GetService<EditCommonDictionaryPageVM>().SetDictionaryCommand.Execute(Id);
        private void SetTotalDictionaryProgress(CommonDictionary commonDictionary)
        {
            int hundredPercentValue = commonDictionary.Relations.Count * 100;
            int currentValue = commonDictionary.Relations.Sum(commonRelation => commonRelation.Rating);
            TotalDictionaryProgress = (int)(currentValue * (100d / hundredPercentValue));
        }
        #endregion
    }
}
