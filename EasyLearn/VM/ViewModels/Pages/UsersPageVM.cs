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
using EasyLearn.Infrastructure.Helpers;
using System.Windows.Controls;
using Page = EasyLearn.Infrastructure.Enums.Page;

namespace EasyLearn.VM.ViewModels.Pages
{
    public class UsersPageVM : ViewModel
    {
        #region Repositories
        private readonly IEasyLearnUserRepository userRerository;
        #endregion

        #region Private fields
        private int userIdForDelete;
        #endregion

        #region Binding props
        public ObservableCollection<UserControl> UserViews { get; set; }
        public string AddingWindowUserNameValue { get; set; }
        public bool ConfirmUserAddingButtonIsEnabled { get; set; }
        #endregion

#pragma warning disable CS8618
        public UsersPageVM(IEasyLearnUserRepository userRerository)
        {
            this.userRerository = userRerository;
            LoadUserViews();
        }
#pragma warning restore CS8618

        #region Commands
        public Command CreateUserCommand { get; private set; }
        public Command DeleteUserCommand { get; private set; }
        public Command<int> SetUserAsCurrentCommand { get; private set; }
        public Command ClearAddingWindowCommand { get; private set; }
        public Command FlipBackAllCardsCommand { get; private set; }
        public Command UpdateConfirmUserAddingButtonAvailabilityCommand { get; private set; }
        public Command OpenAddingUserWindowCommand { get; private set; }
        public Command<int> OpenDeleteUserWindowCommand { get; private set; }
        protected override void InitCommands()
        {
            CreateUserCommand = new Command(async () => await CreateUser());
            DeleteUserCommand = new Command(async () => await DeleteUser());
            SetUserAsCurrentCommand = new Command<int>(async userId => await SetUserAsCurrent(userId));
            ClearAddingWindowCommand = new Command(ClearAddingWindow);
            FlipBackAllCardsCommand = new Command(FlipBackAllCards);
            UpdateConfirmUserAddingButtonAvailabilityCommand = new Command(UpdateConfirmUserAddingButtonAvailability);
            OpenAddingUserWindowCommand = new Command(OpenAddingUserWindow);
            OpenDeleteUserWindowCommand = new Command<int>(OpenDeleteUserWindow);
        }
        private async Task CreateUser()
        {
            string userName = AddingWindowUserNameValue;
            EasyLearnUser newUser = await userRerository.CreateUser(userName);
            AddUserToViewUI(newUser);
            await SetUserAsCurrent(newUser.Id);
            CheckPageBarButtonsAvailability();
        }
        private async Task DeleteUser()
        {
            UserView userView = FindUserView(userIdForDelete);
            bool wasCurrent = userView.IsCurrent;
            UserViews.Remove(userView);
            if (wasCurrent && UserViews.Any(userView => userView is UserView))
                await SetUserAsCurrent(((UserView)UserViews[0]).Id);
            await userRerository.DeleteUser(userIdForDelete);
            CheckPageBarButtonsAvailability();
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
            AddingWindowUserNameValue = string.Empty;
            ConfirmUserAddingButtonIsEnabled = false;
        }
        private void FlipBackAllCards()
        {
            foreach (UserControl userView in UserViews)
            {
                UserView? user = userView as UserView;
                if (user is not null)
                    user.IsCardFlipped = false;
            }
        }
        private void UpdateConfirmUserAddingButtonAvailability() => ConfirmUserAddingButtonIsEnabled = ValidationPool.IsValid(ValidationRulesGroup.AddNewUser);
        private void OpenAddingUserWindow() => OpenNewUserAddingWindowButtonSoftClick();
        private void OpenDeleteUserWindow(int userId)
        {
            userIdForDelete = userId;
            OpenDeleteUserWindowButtonSoftClick();
        }
        #endregion

        #region Event handling
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
                ConfirmUserAddingButton();
        }
        private void OnWindowCtrlNDown()
        {
            if (App.GetService<AppWindowVM>().CurrentPage == Page.Users)
                OpenNewUserAddingWindowButtonSoftClick();
        }
        private void OnWindowEscDown()
        {
            if (App.GetService<AppWindowVM>().CurrentPage == Page.Users)
                CancelUserAddingButtonSoftClick();
        }
        #endregion

        #region Private helpers
        private void LoadUserViews()
        {
            IEnumerable<EasyLearnUser> easyLearnUsers = userRerository.GetAllUsers();
            IEnumerable<UserView> userViews = easyLearnUsers.Select(easyLearnUser => UserView.Create(easyLearnUser));
            UserViews = new ObservableCollection<UserControl>(userViews);
            AddShadowUserViewToUI();
        }
        private void UpdatePagesForNewUser()
        {
            App.GetService<DictionariesPageVM>().UpdatePageCommand.Execute();
            App.GetService<DictationPageVM>().UpdatePageForNewUserCommand.Execute();
        }
        private void CheckPageBarButtonsAvailability() => App.GetService<AppWindowVM>().CheckPageBarButtonsAvailabilityCommand.Execute();
        #endregion

        #region Private UI methods
        private void AddUserToViewUI(EasyLearnUser user)
        {
            RemoveShadowUserViewFromUI();
            UserViews.Add(UserView.Create(user));
            AddShadowUserViewToUI();
        }
        private void AddShadowUserViewToUI() => UserViews.Add(ShadowUserView.Create());
        private void RemoveShadowUserViewFromUI() => UserViews.RemoveAt(UserViews.Count - 1);
        private UserView FindUserView(int userId) => (UserView)UserViews.First(userView => userView is UserView && ((UserView)userView).Id == userId);
        private UserView? TryFindCurrentUserView()
        {
            UserControl? currentUser = UserViews.FirstOrDefault(userView => userView is UserView && ((UserView)userView).IsCurrent);
            return currentUser is null ? null : (UserView?)currentUser;
        }
        private void CancelUserAddingButtonSoftClick() => App.GetService<UsersPage>().cancelUserAddingButton.SoftClick();
        private void ConfirmUserAddingButton() => App.GetService<UsersPage>().confirmUserAddingButton.SoftClick();
        private void OpenNewUserAddingWindowButtonSoftClick() => App.GetService<UsersPage>().openNewUserAddingWindowButton.SoftClick();
        private void ConfirmUserAddingButtonSoftClick() => App.GetService<UsersPage>().confirmUserAddingButton.SoftClick();
        private void OpenDeleteUserWindowButtonSoftClick() => App.GetService<UsersPage>().openDeleteUserWindowButton.SoftClick();
        #endregion
    }
}
