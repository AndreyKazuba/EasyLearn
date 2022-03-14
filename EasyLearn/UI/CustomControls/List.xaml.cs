using System.Windows.Controls;
using EasyLearn.VM.ViewModels.CustomControls;

namespace EasyLearn.UI.CustomControls
{
    public partial class List : UserControl
    {
        public ListVM ViewModel { get; }
        public List(ListVM viewModel)
        {
            this.ViewModel = viewModel;
            this.DataContext = ViewModel;
            InitializeComponent();
        }
    }
}
