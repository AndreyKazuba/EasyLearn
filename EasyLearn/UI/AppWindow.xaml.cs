using System;
using System.Windows;
using System.Windows.Input;
using EasyLearn.VM.Windows;

namespace EasyLearn.UI
{
    public partial class AppWindow : Window
    {
        public static event Action? WindowCtrlNDown;
        public static event Action? WindowEscDown;

        public AppWindow(AppWindowVM viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        private void OnWindowKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.N && (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || e.KeyboardDevice.IsKeyDown(Key.RightCtrl)) && WindowCtrlNDown is not null)
                WindowCtrlNDown();
            if (e.Key == Key.Escape && WindowEscDown is not null)
                WindowEscDown();
        }
    }
}
