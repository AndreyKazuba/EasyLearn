using System.Windows.Controls;
using EasyLearn.Data.Models;
using EasyLearn.VM.ViewModels.CustomControls;

namespace EasyLearn.UI.CustomControls
{
    public partial class UserView : UserControl
    {
        public UserVM ViewModel { get; }
        public UserView(UserVM viewModel)
        {
            this.DataContext = viewModel;
            this.ViewModel = viewModel;
            InitializeComponent();
        }

        public static UserView Create(EasyLearnUser user) => new UserView(new UserVM(user));
    }
}
