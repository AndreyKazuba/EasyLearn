using System.Threading.Tasks;
using EasyLearn.VM.Core;
using EasyLearn.Data.Models;
using EasyLearn.VM.ViewModels.Pages;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Data.Helpers;
using System.Linq;
using EasyLearn.Data.Constants;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class UserVM : ViewModel
    {
        #region Private fields
        private string lastValidDictionaryName;
        #endregion

        #region Public props
        public int Id { get; }
        #endregion

        #region Binding props
        public string Name { get; set; }
        public bool IsCurrent { get; set; }
        public string EditNameFieldValue { get; set; }
        public string TotalDictionariesCount { get; set; }
        public string TotalRelationsCount { get; set; }
        public string TotalStudiedRelationsCount { get; set; }
        public string TotalLeftToLearnRelationsCount { get; set; }
        public bool IsCardFlipped { get; set; }
        public bool BackButtonIsEnabled { get; set; } = true;
        public int TotalUserProgress { get; set; }
        #endregion

#pragma warning disable CS8618
        public UserVM(EasyLearnUser user)
        {
            Id = user.Id;
            Name = StringHelper.NormalizeRegister(user.Name);
            EditNameFieldValue = Name;
            IsCurrent = user.IsCurrent;
            int totalDictionariesCount = user.CommonDictionaries.Count + user.VerbPrepositionDictionaries.Count;
            int totalRelationsCount = user.CommonDictionaries.Sum(commonDictionary => commonDictionary.Relations.Count)
                                    + user.VerbPrepositionDictionaries.Sum(verbPrepositionDictionary => verbPrepositionDictionary.VerbPrepositions.Count);
            int totalStudiedRelationsCount = user.CommonDictionaries.Sum(commonDictionary => commonDictionary.Relations.Count(commonRelation => commonRelation.Studied))
                                        + user.VerbPrepositionDictionaries.Sum(verbPrepositionDictionary => verbPrepositionDictionary.VerbPrepositions.Count(verbPreposition => verbPreposition.Studied));
            int totalLeftToLearnRelationsCount = totalRelationsCount - totalStudiedRelationsCount;
            TotalDictionariesCount = totalDictionariesCount.ToString();
            TotalRelationsCount = totalRelationsCount.ToString();
            TotalStudiedRelationsCount = totalStudiedRelationsCount.ToString();
            TotalLeftToLearnRelationsCount = totalLeftToLearnRelationsCount.ToString();
            SetTotalUserProgress(user);
        }
#pragma warning restore CS8618

        #region Commands
        public Command SetUserAsCurrentCommand { get; private set; }
        public Command RemoveUserCommand { get; private set; }
        public Command UpdateUserCommand { get; private set; }
        public Command SetEditNameFieldValueCommand { get; private set; }
        public Command FlipBackAllAnotherCardsCommand { get; private set; }
        public Command SaveLastValidDictionaryNameCommand { get; private set; }
        protected override void InitCommands()
        {
            SetUserAsCurrentCommand = new Command(SetUserAsCurrent);
            RemoveUserCommand = new Command(RemoveUser);
            UpdateUserCommand = new Command(async () => await UpdateUser());
            SetEditNameFieldValueCommand = new Command(SetEditNameFieldValue);
            FlipBackAllAnotherCardsCommand = new Command(FlipBackAllAnotherCards);
            SaveLastValidDictionaryNameCommand = new Command(SaveLastValidDictionaryName);
        }
        private void SetUserAsCurrent() => App.GetService<UsersPageVM>().SetUserAsCurrentCommand.Execute(Id);
        private void RemoveUser() => App.GetService<UsersPageVM>().OpenDeleteUserWindowCommand.Execute(Id);
        private async Task UpdateUser()
        {
            string newUserName = lastValidDictionaryName.Prepare().NormalizeRegister();
            if (StringHelper.Equals(Name, newUserName))
                return;
            Name = StringHelper.PrepareAndNormalize(newUserName);
            await App.GetService<IEasyLearnUserRepository>().EditUser(Id, newUserName);
        }
        private void SetEditNameFieldValue() => EditNameFieldValue = Name;
        private void FlipBackAllAnotherCards() => App.GetService<UsersPageVM>().FlipBackAllCardsCommand.Execute();
        private void SaveLastValidDictionaryName()
        {
            if (EditNameFieldValue.Length <= ModelConstants.UserNameMaxLength && EditNameFieldValue.Length >= ModelConstants.UserNameMinLength)
                lastValidDictionaryName = EditNameFieldValue;
        }
        #endregion

        #region Private helpers
        private void SetTotalUserProgress(EasyLearnUser user)
        {
            int hundredPercentValue = (user.CommonDictionaries.Sum(commonDictionary => commonDictionary.Relations.Count)
                                     + user.VerbPrepositionDictionaries.Sum(verbPrepositionDictionary => verbPrepositionDictionary.VerbPrepositions.Count))
                                     * 100;
            int ratingCurrentValue = user.CommonDictionaries.Sum(commonDictionary => commonDictionary.Relations.Sum(commonRelation => commonRelation.Rating))
                + user.VerbPrepositionDictionaries.Sum(verbPrepositionDictionary => verbPrepositionDictionary.VerbPrepositions.Sum(verbPreposition => verbPreposition.Rating));
            int ratingTotalValue = (int)(ratingCurrentValue * (100d / hundredPercentValue) * 0.8);
            int studiedCurrentValue = (user.CommonDictionaries.Sum(commonDictionary => commonDictionary.Relations.Count(commonRelation => commonRelation.Studied))
                + user.VerbPrepositionDictionaries.Sum(verbPrepositionDictionary => verbPrepositionDictionary.VerbPrepositions.Count(verbPreposition => verbPreposition.Studied)))
                * 100;
            int studiedTotalValue = (int)(studiedCurrentValue * (100d / hundredPercentValue) * 0.2);
            TotalUserProgress = ratingTotalValue + studiedTotalValue;
        }
        #endregion
    }
}
