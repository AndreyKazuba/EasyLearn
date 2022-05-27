using System;
using System.Windows;
using System.Windows.Input;
using EasyLearn.Helpers;
using EasyLearn.VM.Windows;

namespace EasyLearn.UI
{
    public partial class AppWindow : Window
    {
        #region Events
        public static event Action? WindowCtrlNDown;
        public static event Action? WindowEscDown;
        public static event Action? DrawerButtonClick;
        public static event Action? GoBackButtonClick;
        public static event Action? OpenMenuButtonClick;
        #endregion

        public AppWindow(AppWindowVM viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            new WindowResizer(this);
        }

        #region UI event handlers
        private void OnWindowKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.N && (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || e.KeyboardDevice.IsKeyDown(Key.RightCtrl)) && WindowCtrlNDown is not null)
                WindowCtrlNDown();
            if (e.Key == Key.Escape && WindowEscDown is not null)
                WindowEscDown();
        }
        private void OnDrawerButtonClick(object sender, RoutedEventArgs e)
        {
            if (DrawerButtonClick is not null)
                DrawerButtonClick();
        }
        private void OnGoBackButtonClick(object sender, RoutedEventArgs e)
        {
            if (GoBackButtonClick is not null)
                GoBackButtonClick();
        }
        private void OnOpenMenuButtonClick(object sender, RoutedEventArgs e)
        {
            if (OpenMenuButtonClick is not null)
                OpenMenuButtonClick();
        }
        #endregion
    }
}
