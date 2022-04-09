using System;
using System.Windows.Controls;
using System.Windows.Input;
using EasyLearn.VM.ViewModels.Pages;

namespace EasyLearn.UI.Pages
{
    public partial class UsersPage : Page
    {
        public static event Action? UserNameValueTextBoxEnterDown;
        public UsersPage(UsersPageVM viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
        private void OnUserNameValueTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && UserNameValueTextBoxEnterDown is not null)
                UserNameValueTextBoxEnterDown();
        }
    }
}
