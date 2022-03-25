using EasyLearn.VM.ViewModels.Pages;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace EasyLearn.UI.Pages
{
    public partial class DictationPage : Page
    {
        public static event Action? EnterClick;
        public DictationPage(DictationPageVM viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && EnterClick is not null)
                EnterClick();
        }
    }
}
