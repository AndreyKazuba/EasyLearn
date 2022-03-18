using System.Windows.Controls;
using EasyLearn.VM.ViewModels.CustomControls;

namespace EasyLearn.UI.CustomControls
{
    public partial class CommonDictionaryView : UserControl
    {
        public CommonDictionaryVM ViewModel { get; }
        public CommonDictionaryView(CommonDictionaryVM viewModel)
        {
            this.ViewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
