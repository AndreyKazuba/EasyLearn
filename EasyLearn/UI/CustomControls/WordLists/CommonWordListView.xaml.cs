using System.Windows.Controls;
using EasyLearn.VM.ViewModels.CustomControls;

namespace EasyLearn.UI.CustomControls
{
    public partial class CommonWordListView : UserControl
    {
        public CommonWordListVM ViewModel { get; }
        public CommonWordListView(CommonWordListVM viewModel)
        {
            this.ViewModel = viewModel;
            this.DataContext = ViewModel;
            InitializeComponent();
        }
    }
}
