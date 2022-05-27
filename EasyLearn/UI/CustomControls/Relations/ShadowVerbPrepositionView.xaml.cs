using EasyLearn.VM.ViewModels.CustomControls;
using System.Windows.Controls;

namespace EasyLearn.UI.CustomControls
{
    public partial class ShadowVerbPrepositionView : UserControl
    {
        public ShadowVerbPrepositionView(ShadowVerbPrepositionVM viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }

        #region Static methods
        public static ShadowVerbPrepositionView Create() => new ShadowVerbPrepositionView(new ShadowVerbPrepositionVM());
        #endregion
    }
}
