using System;
using System.Windows.Controls;
using System.Windows.Input;
using EasyLearn.VM.ViewModels.Pages;

namespace EasyLearn.UI.Pages
{
    public partial class UsersPage : Page
    {
        #region Events
        public static event Action? UserNameValueTextBoxEnterDown;
        #endregion

        public UsersPage(UsersPageVM viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }

        #region UI event handlers
        private void OnUserNameValueTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && UserNameValueTextBoxEnterDown is not null)
                UserNameValueTextBoxEnterDown();
        }
        #endregion
    }
}
