using EasyLearn.VM.ViewModels.CustomControls;
using System.Windows.Controls;

namespace EasyLearn.UI.CustomControls
{
    public partial class VerbPrepositionView : UserControl
    {
        public VerbPrepositionView(VerbPrepositionVM viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
