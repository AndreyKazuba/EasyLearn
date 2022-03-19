using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyLearn.VM.Core;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Data.Models;
using EasyLearn.UI.CustomControls;
using System.Collections.ObjectModel;
using EasyLearn.VM.ViewModels.CustomControls;
using EasyLearn.VM.Windows;

namespace EasyLearn.VM.ViewModels.Pages
{
    public class UsersPageVM : ViewModel
    {
        private readonly IEasyLearnUserRepository usersRerository;
        public UsersPageVM(IEasyLearnUserRepository usersRerository)
        {
            this.usersRerository = usersRerository;
            App.GetService<AppWindowVM>().CurrentPageChanged += () => FlipBackAllCards();
            LoadUsers();
        }

        #region Props for binding

        public ObservableCollection<UserView> Users { get; set; }
        public string NewUserName { get; set; }
        public bool ConfirmNewUserButtonIsEnabled { get; set; } = true;

        #endregion

        #region Commands

        public DelegateCommand CreateUserCommand { get; private set; }
        public DelegateCommand RemoveUserCommand { get; private set; }
        public DelegateCommand SetUserAsCurrentCommand { get; private set; }
        public DelegateCommand ClearUserAddingWindowCommand { get; private set; }
        public DelegateCommand FlipBackAllCardsCommand { get; private set; }

        protected override void InitCommands()
        {
            this.CreateUserCommand = new DelegateCommand(async arg => await CreateUser());
            this.RemoveUserCommand = new DelegateCommand(async userId => await RemoveUser((int)userId));
            this.SetUserAsCurrentCommand = new DelegateCommand(async userId => await SetUserAsCurrent((int)userId));
            this.ClearUserAddingWindowCommand = new DelegateCommand(arg => ClearUserAddingWindow());
            this.FlipBackAllCardsCommand = new DelegateCommand(arg => FlipBackAllCards());
        }

        #endregion

        private async Task SetUserAsCurrent(int userId)
        {
            if (!this.Users.Any())
                throw new Exception("There are no users");
            UserView? lastCurrentUser = this.Users.FirstOrDefault(user => user.ViewModel.IsCurrent);
            if (lastCurrentUser is not null)
                lastCurrentUser.ViewModel.IsCurrent = false;
            this.Users.First(user => user.ViewModel.Id == userId).ViewModel.IsCurrent = true;
            await usersRerository.SetUserAsCurrent(userId);
            UpdateDictionariesPageView();
        }
        private async Task CreateUser()
        {
            EasyLearnUser? newUser = await usersRerository.CreateUser(this.NewUserName);
            if (newUser is null)
                throw new Exception("Name is incorrect or this user has already created");
            AddUserToUI(newUser);
            await SetUserAsCurrent(newUser.Id);
        }
        private async Task RemoveUser(int userId)
        {
            UserView user = this.Users.First(user => user.ViewModel.Id == userId);
            bool isCurrent = user.ViewModel.IsCurrent;
            this.Users.Remove(user);
            if (isCurrent && this.Users.Any())
                await SetUserAsCurrent(this.Users[0].ViewModel.Id);
            await usersRerository.DeleteUser(userId);
        }
        private void ClearUserAddingWindow() => this.NewUserName = string.Empty;
        private void LoadUsers()
        {
            IEnumerable<EasyLearnUser> easyLearnUsers = usersRerository.GetAllUsers();
            IEnumerable<UserView> userViews = easyLearnUsers.Select(easyLearnUser => new UserView(new UserVM(easyLearnUser)));
            this.Users = new ObservableCollection<UserView>(userViews);
        }
        private void AddUserToUI(EasyLearnUser user) => this.Users.Add(new UserView(new UserVM(user)));
        private void UpdateDictionariesPageView() => App.GetService<DictionariesPageVM>().UpdateView();
        private void FlipBackAllCards()
        {
            foreach (UserView userView in this.Users)
                userView.ViewModel.IsCardFlipped = false;
        }
    }
}
