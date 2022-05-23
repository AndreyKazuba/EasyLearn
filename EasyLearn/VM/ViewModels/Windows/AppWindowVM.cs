using EasyLearn.VM.Core;
using EasyLearn.Infrastructure.Enums;
using System;
using System.Windows;
using EasyLearn.UI;

namespace EasyLearn.VM.Windows
{
    public class AppWindowVM : ViewModel
    {
        private Page currentPage;
        public event Action CurrentPageChanged;

        public Page CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                if (this.CurrentPageChanged is not null)
                    this.CurrentPageChanged.Invoke();
            }
        }
        public bool ShowMenuButtonIsVisible { get; set; } = true;
        public bool CloseMenuButtonIsVisible { get; set; }
        public bool GoBackButtonIsVisible { get; set; }

        public AppWindowVM()
        {
            SetStartPage();
        }

        #region Commands
        public Command OpenDictationPage { get; private set; }
        public Command OpenUsersPage { get; private set; }
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
        protected override void InitCommands()
        {
            this.OpenDictationPage = new Command(() => this.CurrentPage = Page.Dictation);
            this.OpenUsersPage = new Command(() => this.CurrentPage = Page.Users);
            this.OpenDictionariesPageCommand = new Command(() => this.CurrentPage = Page.Dictionaries);
            this.OpenEditCommonDictionaryPageCommand = new Command(() => this.CurrentPage = Page.EditCommonWordListPage);
            this.OpenEditVerbPrepositionDictionaryPageCommand = new Command(() => this.CurrentPage = Page.EditVerbPrepositionListPage);
            this.MinimizeCommand = new Command(Minimize);
            this.MaximizeCommand = new Command(Maximize);
            this.CloseCommand = new Command(Close);
            this.SetCloseMenuButtonCommand = new Command(SetCloseMenuButton);
            this.SetShowMenuButtonCommand = new Command(SetShowMenuButton);
            this.SetGoBackButtonCommand = new Command(SetGoBackButton);
            this.HideGoBackButtonCommand = new Command(HideGoBackButton);
        }

        protected override void InitEvents()
        {
            AppWindow.DrawerButtonClick += OnDrawerButtonClick;
        }
        private void OnDrawerButtonClick() => SetShowMenuButton();

        #endregion
        private void SetCloseMenuButton()
        {
            this.ShowMenuButtonIsVisible = false;
            this.CloseMenuButtonIsVisible = true;
        }
        private void SetShowMenuButton()
        {
            this.CloseMenuButtonIsVisible = false;
            this.ShowMenuButtonIsVisible = true;
        }
        private void SetGoBackButton() => this.GoBackButtonIsVisible = true;
        private void HideGoBackButton() => this.GoBackButtonIsVisible = false;

        private void SetStartPage() => this.CurrentPage = Page.Dictionaries;
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

    }
}
