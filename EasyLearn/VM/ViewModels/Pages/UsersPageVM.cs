using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLearn.VM.Core;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using EasyLearn.UI.CustomControls;
using System.Collections.ObjectModel;
using EasyLearn.VM.ViewModels.CustomControls;


namespace EasyLearn.VM.ViewModels.Pages
{
    public class UsersPageVM : ViewModel
    {
        private readonly IEasyLearnUsersRerository usersRerository;
        public UsersPageVM(IEasyLearnUsersRerository usersRerository)
        {
            this.usersRerository = usersRerository;

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

        protected override void InitCommands()
        {
            this.CreateUserCommand = new DelegateCommand(async arg => await CreateUser());
            this.RemoveUserCommand = new DelegateCommand(async userId => await RemoveUser((int)userId));
            this.SetUserAsCurrentCommand = new DelegateCommand(async userId => await SetUserAsCurrent((int)userId));
            this.ClearUserAddingWindowCommand = new DelegateCommand(arg => ClearUserAddingWindow());
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
            await usersRerository.RemoveUser(userId);
        }
        private void ClearUserAddingWindow() => this.NewUserName = string.Empty;
        private void LoadUsers()
        {
            IEnumerable<EasyLearnUser> easyLearnUsers = usersRerository.GetAllUsers();
            IEnumerable<UserView> userViews = easyLearnUsers.Select(easyLearnUser => new UserView(new UserVM(easyLearnUser)));
            this.Users = new ObservableCollection<UserView>(userViews);
        }
        private void AddUserToUI(EasyLearnUser user) => this.Users.Add(new UserView(new UserVM(user)));
        private void UpdateDictionariesPageView()
        {
            DictionariesPageVM? dictionariesPageVM = App.ServiceProvider.GetService<DictionariesPageVM>();
            if (dictionariesPageVM is not null)
                dictionariesPageVM.UpdateView();
        }
    }
}
