using EasyLearn.VM.ViewModels.Pages;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace EasyLearn.UI.Pages
{
    public partial class DictationPage : Page
    {
        public static event Action? EnterClick;
        public static event Action? PromtMouseEnter;
        public static event Action? PromtMouseLeave;
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
        private void OnPromtMouseEnter(object sender, MouseEventArgs e)
        {
            if (PromtMouseEnter is not null)
                PromtMouseEnter();
        }
        private void OnPromtMouseLeave(object sender, MouseEventArgs e)
        {
            if (PromtMouseLeave is not null)
                PromtMouseLeave();
        }
    }
}
