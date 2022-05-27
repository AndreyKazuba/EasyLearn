using EasyLearn.VM.ViewModels.Pages;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace EasyLearn.UI.Pages
{
    public partial class DictationPage : Page
    {
        #region Events
        public static event Action? EnterClick;
        public static event Action? PromtMouseEnter;
        public static event Action? PromtMouseLeave;
        #endregion

        public DictationPage(DictationPageVM viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }

        #region UI event handlers
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
        #endregion
    }
}
