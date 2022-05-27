using System.Threading.Tasks;
using EasyLearn.VM.Core;
using EasyLearn.Data.Models;
using EasyLearn.VM.ViewModels.Pages;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Data.Helpers;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class UserVM : ViewModel
    {
        #region Binding props
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCurrent { get; set; }
        public string EditNameFieldValue { get; set; }
        public bool IsCardFlipped { get; set; }
        public bool BackButtonIsEnabled { get; set; } = true;
        #endregion

#pragma warning disable CS8618
        public UserVM(EasyLearnUser user)
        {
            this.Id = user.Id;
            this.Name = StringHelper.NormalizeRegister(user.Name);
            this.IsCurrent = user.IsCurrent;
        }
#pragma warning restore CS8618

        #region Commands
        public Command SetUserAsCurrentCommand { get; private set; }
        public Command RemoveUserCommand { get; private set; }
        public Command EditUserCommand { get; private set; }
        public Command SetEditNameFieldValueCommand { get; private set; }
        public Command FlipBackAllAnotherCardsCommand { get; private set; }
        protected override void InitCommands()
        {
            SetUserAsCurrentCommand = new Command(SetUserAsCurrent);
            RemoveUserCommand = new Command(RemoveUser);
            EditUserCommand = new Command(async () => await EditUser());
            SetEditNameFieldValueCommand = new Command(SetEditNameFieldValue);
            FlipBackAllAnotherCardsCommand = new Command(FlipBackAllAnotherCards);
        }
        private void SetUserAsCurrent() => App.GetService<UsersPageVM>().SetUserAsCurrentCommand.Execute(Id);
        private void RemoveUser() => App.GetService<UsersPageVM>().DeleteUserCommand.Execute(Id);
        private async Task EditUser()
        {
            string newUserName = EditNameFieldValue;
            if (StringHelper.Equals(Name, newUserName))
                return;
            Name = StringHelper.PrepareAndNormalize(newUserName);
            await App.GetService<IEasyLearnUserRepository>().EditUser(Id, newUserName);
        }
        private void SetEditNameFieldValue() => EditNameFieldValue = Name;
        private void FlipBackAllAnotherCards() => App.GetService<UsersPageVM>().FlipBackAllCardsCommand.Execute();
        #endregion
    }
}
