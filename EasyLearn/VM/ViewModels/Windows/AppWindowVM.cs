using EasyLearn.VM.Core;
using EasyLearn.Infrastructure.Enums;
using System;
using System.Windows;
using EasyLearn.UI;
using EasyLearn.Data.Repositories.Interfaces;

namespace EasyLearn.VM.Windows
{
    public class AppWindowVM : ViewModel
    {
        #region Private fields
        private Page currentPage;
        #endregion

        #region Events
        public event Action CurrentPageChanged;
        #endregion

        #region Binding props
        public Page CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                if (CurrentPageChanged is not null)
                    CurrentPageChanged.Invoke();
            }
        }
        public bool ShowMenuButtonIsVisible { get; set; } = true;
        public bool CloseMenuButtonIsVisible { get; set; }
        public bool GoBackButtonIsVisible { get; set; }
        public bool OpenDictationPageButtonIsEnabled { get; set; }
        public bool OpenDictionariesPageButtonIsEnabled { get; set; }
        public bool OpenUsersPageButtonIsEnabled { get; set; }
        #endregion

#pragma warning disable CS8618
        public AppWindowVM() => SetCurrentPage();
#pragma warning restore CS8618

        #region Commands
        public Command OpenDictationPageCommand { get; private set; }
        public Command OpenUsersPageCommand { get; private set; }
        public Command OpenDictionariesPageCommand { get; private set; }
        public Command OpenEditCommonDictionaryPageCommand { get; private set; }
        public Command OpenEditVerbPrepositionDictionaryPageCommand { get; private set; }
        public Command MinimizeCommand { get; private set; }
        public Command MaximizeCommand { get; private set; }
        public Command CloseCommand { get; private set; }
        public Command SetCloseMenuButtonCommand { get; private set; }
        public Command SetShowMenuButtonCommand { get; private set; }
        public Command SetGoBackButtonCommand { get; private set; }
        public Command HideGoBackButtonCommand { get; private set; }
        public Command CheckPageBarButtonsAvailabilityCommand { get; private set; }
        public Command DisableNavigationBarCommand { get; private set; }
        public Command EnableNavigationBarCommand { get; private set; }
        protected override void InitCommands()
        {
            OpenDictationPageCommand = new Command(OpenDictationPage);
            OpenUsersPageCommand = new Command(OpenUsersPage);
            OpenDictionariesPageCommand = new Command(OpenDictionariesPage);
            OpenEditCommonDictionaryPageCommand = new Command(OpenEditCommonDictionaryPage);
            OpenEditVerbPrepositionDictionaryPageCommand = new Command(OpenEditVerbPrepositionDictionaryPage);
            MinimizeCommand = new Command(Minimize);
            MaximizeCommand = new Command(Maximize);
            CloseCommand = new Command(Close);
            SetCloseMenuButtonCommand = new Command(SetCloseMenuButton);
            SetShowMenuButtonCommand = new Command(SetShowMenuButton);
            SetGoBackButtonCommand = new Command(SetGoBackButton);
            HideGoBackButtonCommand = new Command(HideGoBackButton);
            CheckPageBarButtonsAvailabilityCommand = new Command(CheckPageBarButtonsAvailability);
            DisableNavigationBarCommand = new Command(DisableNavigationBar);
            EnableNavigationBarCommand = new Command(EnableNavigationBar);
        }
        private void OpenDictationPage() => CurrentPage = Page.Dictation;
        private void OpenUsersPage() => CurrentPage = Page.Users;
        private void OpenDictionariesPage() => CurrentPage = Page.Dictionaries;
        private void OpenEditCommonDictionaryPage() => CurrentPage = Page.EditCommonDictionaryPage;
        private void OpenEditVerbPrepositionDictionaryPage() => CurrentPage = Page.EditVerbPrepositionDictionaryPage;
        private void Minimize()
        {
            AppWindow window = App.GetService<AppWindow>();
            window.WindowState = WindowState.Minimized;
        }
        private void Maximize()
        {
            AppWindow window = App.GetService<AppWindow>();
            window.WindowState ^= WindowState.Maximized;
        }
        private void Close()
        {
            AppWindow window = App.GetService<AppWindow>();
            window.Close();
        }
        private void SetCloseMenuButton()
        {
            ShowMenuButtonIsVisible = false;
            CloseMenuButtonIsVisible = true;
        }
        private void SetShowMenuButton()
        {
            CloseMenuButtonIsVisible = false;
            ShowMenuButtonIsVisible = true;
        }
        private void SetGoBackButton() => GoBackButtonIsVisible = true;
        private void HideGoBackButton() => GoBackButtonIsVisible = false;
        private void CheckPageBarButtonsAvailability()
        {
            bool atLeastOneUserExist = App.GetService<IEasyLearnUserRepository>().IsAtLeastOneUserExist();
            if (atLeastOneUserExist)
            {
                OpenUsersPageButtonIsEnabled = true;
                OpenDictionariesPageButtonIsEnabled = true;
                OpenDictationPageButtonIsEnabled = true;
            }
            else
            {
                OpenUsersPageButtonIsEnabled = true;
                OpenDictionariesPageButtonIsEnabled = false;
                OpenDictationPageButtonIsEnabled = false;
            }
        }
        private void DisableNavigationBar()
        {
            OpenUsersPageButtonIsEnabled = false;
            OpenDictionariesPageButtonIsEnabled = false;
            OpenDictationPageButtonIsEnabled = false;
        }
        private void EnableNavigationBar()
        {
            OpenUsersPageButtonIsEnabled = true;
            OpenDictionariesPageButtonIsEnabled = true;
            OpenDictationPageButtonIsEnabled = true;
        }
        #endregion

        #region Event handling
        protected override void InitEvents()
        {
            AppWindow.DrawerButtonClick += OnDrawerButtonClick;
        }
        private void OnDrawerButtonClick() => SetShowMenuButton();
        #endregion

        #region Private helpers
        private void SetCurrentPage()
        {
            bool atLeastOneUserExist = App.GetService<IEasyLearnUserRepository>().IsAtLeastOneUserExist();
            if (atLeastOneUserExist)
            {
                OpenUsersPageButtonIsEnabled = true;
                OpenDictionariesPageButtonIsEnabled = true;
                OpenDictationPageButtonIsEnabled = true;
                CurrentPage = Page.Dictation;
            }
            else
            {
                OpenUsersPageButtonIsEnabled = true;
                OpenDictionariesPageButtonIsEnabled = false;
                OpenDictationPageButtonIsEnabled = false;
                CurrentPage = Page.Users;
            }
        }
        #endregion
    }
}
