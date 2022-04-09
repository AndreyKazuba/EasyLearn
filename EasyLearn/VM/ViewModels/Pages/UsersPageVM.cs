using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using EasyLearn.VM.Core;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Data.Models;
using EasyLearn.UI.CustomControls;
using EasyLearn.VM.Windows;
using EasyLearn.Infrastructure.Validation;
using EasyLearn.UI.Pages;
using EasyLearn.UI;
using EasyLearn.Infrastructure.Enums;
using EasyLearn.Infrastructure.Helpers;

namespace EasyLearn.VM.ViewModels.Pages
{
    public class UsersPageVM : ViewModel
    {
        #region Repositories
        private readonly IEasyLearnUserRepository userRerository;
        #endregion

#pragma warning disable CS8618
        public UsersPageVM(IEasyLearnUserRepository userRerository)
        {
            this.userRerository = userRerository;
            LoadUserViews();
        }
#pragma warning restore CS8618

        #region Props for binding
        public ObservableCollection<UserView> UserViews { get; set; }
        public string AddingWindowUserNameValue { get; set; }
        public bool ConfirmUserAddingButtonIsEnabled { get; set; }
        #endregion

        #region Events
        protected override void InitEvents()
        {
            App.GetService<AppWindowVM>().CurrentPageChanged += () => FlipBackAllCards();
            UsersPage.UserNameValueTextBoxEnterDown += OnUserNameValueTextBoxEnterDown;
            AppWindow.WindowCtrlNDown += OnWindowCtrlNDown;
            AppWindow.WindowEscDown += OnWindowEscDown;
        }
        private void OnUserNameValueTextBoxEnterDown()
        {
            if (ValidationPool.IsValid(ValidationRulesGroup.AddNewUser))
                AddingNewUserButtonSoftClick();
        }
        private void OnWindowCtrlNDown()
        {
            if (App.GetService<AppWindowVM>().CurrentPage == Page.Users)
                OpenNewUserAddingWindowButtonSoftClick();
        }
        private void OnWindowEscDown()
        {
            if (App.GetService<AppWindowVM>().CurrentPage == Page.Users)
                NewUserAddingWindowCancelButtonSoftClick();
        }
        #endregion

        #region Commands
        public Command CreateUserCommand { get; private set; }
        public Command<int> DeleteUserCommand { get; private set; }
        public Command<int> SetUserAsCurrentCommand { get; private set; }
        public Command ClearAddingWindowCommand { get; private set; }
        public Command FlipBackAllCardsCommand { get; private set; }
        public Command UpdateConfirmUserAddingButtonAvailabilityCommand { get; private set; }
        protected override void InitCommands()
        {
            this.CreateUserCommand = new Command(async () => await CreateUser());
            this.DeleteUserCommand = new Command<int>(async userId => await DeleteUser(userId));
            this.SetUserAsCurrentCommand = new Command<int>(async userId => await SetUserAsCurrent(userId));
            this.ClearAddingWindowCommand = new Command(ClearAddingWindow);
            this.FlipBackAllCardsCommand = new Command(FlipBackAllCards);
            this.UpdateConfirmUserAddingButtonAvailabilityCommand = new Command(UpdateConfirmUserAddingButtonAvailability);
        }
        #endregion

        #region Command logic methods
        private async Task CreateUser()
        {
            string userName = this.AddingWindowUserNameValue;
            EasyLearnUser newUser = await userRerository.CreateUser(userName);
            AddUserToUI(newUser);
            await SetUserAsCurrent(newUser.Id);
        }
        private async Task DeleteUser(int userId)
        {
            UserView userView = FindUserView(userId);
            bool wasCurrent = userView.IsCurrent;
            this.UserViews.Remove(userView);
            if (wasCurrent && this.UserViews.Any())
                await SetUserAsCurrent(this.UserViews[0].Id);
            await userRerository.DeleteUser(userId);
        }
        private async Task SetUserAsCurrent(int userId)
        {
            UserView? lastCurrentUserView = TryFindCurrentUserView();
            if (lastCurrentUserView is not null)
                lastCurrentUserView.IsCurrent = false;
            FindUserView(userId).IsCurrent = true;
            await userRerository.SetUserAsCurrent(userId);
            UpdatePagesForNewUser();
        }
        private void ClearAddingWindow()
        {
            this.AddingWindowUserNameValue = String.Empty;
            this.ConfirmUserAddingButtonIsEnabled = false;
        }
        private void FlipBackAllCards()
        {
            foreach (UserView userView in this.UserViews)
                userView.IsCardFlipped = false;
        }
        private void UpdateConfirmUserAddingButtonAvailability() => this.ConfirmUserAddingButtonIsEnabled = ValidationPool.IsValid(ValidationRulesGroup.AddNewUser);
        #endregion

        #region Other private methods
        private void LoadUserViews()
        {
            IEnumerable<EasyLearnUser> easyLearnUsers = userRerository.GetAllUsers();
            IEnumerable<UserView> userViews = easyLearnUsers.Select(easyLearnUser => UserView.Create(easyLearnUser));
            this.UserViews = new ObservableCollection<UserView>(userViews);
        }
        private void AddUserToUI(EasyLearnUser user) => this.UserViews.Add(UserView.Create(user));
        private UserView FindUserView(int userId) => this.UserViews.First(userView => userView.Id == userId);
        private UserView? TryFindCurrentUserView() => this.UserViews.FirstOrDefault(userView => userView.IsCurrent);
        private void UpdatePagesForNewUser()
        {
            App.GetService<DictionariesPageVM>().UpdatePageForNewUserCommand.Execute();
            App.GetService<DictationPageVM>().UpdatePageForNewUserCommand.Execute();
        }
        private void AddingNewUserButtonSoftClick() => App.GetService<UsersPage>().confirmUserAddingButton.SoftClick();
        private void OpenNewUserAddingWindowButtonSoftClick() => App.GetService<UsersPage>().addNewUserButton.SoftClick();
        private void NewUserAddingWindowCancelButtonSoftClick() => App.GetService<UsersPage>().cancelUserAddingButton.SoftClick();
        #endregion
    }
}
