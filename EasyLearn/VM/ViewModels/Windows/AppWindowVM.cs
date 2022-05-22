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

        public AppWindowVM()
        {
            SetStartPage();
        }

        #region Commands
        public Command OpenDictationPage { get; set; }
        public Command OpenUsersPage { get; set; }
        public Command OpenDictionariesPageCommand { get; set; }
        public Command OpenEditCommonDictionaryPageCommand { get; set; }
        public Command OpenEditVerbPrepositionDictionaryPageCommand { get; set; }
        public Command MinimizeCommand { get; set; }
        public Command MaximizeCommand { get; set; }
        public Command CloseCommand { get; set; }
        public Command SetCloseMenuButtonCommand { get; set; }
        public Command SetShowMenuButtonCommand { get; set; }
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
        }

        protected override void InitEvents()
        {
            AppWindow.DrawerButtonClick += OnDrawerButtonClick;
        }
        private void OnDrawerButtonClick() => SetShowMenuButton();

        #endregion
        private void SetCloseMenuButton() => this.ShowMenuButtonIsVisible = false;
        private void SetShowMenuButton() => this.ShowMenuButtonIsVisible = true;
        private void SetStartPage() => this.CurrentPage = Page.Users;
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
