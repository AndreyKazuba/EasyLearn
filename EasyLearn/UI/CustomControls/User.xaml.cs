using System.Windows.Controls;
using EasyLearn.VM.ViewModels.CustomControls;

namespace EasyLearn.UI.CustomControls
{
    public partial class User : UserControl
    {
        public UserVM ViewModel { get; }
        public User(UserVM viewModel)
        {
            this.DataContext = viewModel;
            this.ViewModel = viewModel;
            InitializeComponent();
        }
    }
}
