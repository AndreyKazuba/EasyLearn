using EasyLearn.VM.ViewModels.CustomControls;
using System.Windows.Controls;

namespace EasyLearn.UI.CustomControls
{
    public partial class VerbPrepositionDictionaryView : UserControl
    {
        public VerbPrepositionDictionaryVM ViewModel { get; }
        public VerbPrepositionDictionaryView(VerbPrepositionDictionaryVM viewModel)
        {
            this.ViewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
