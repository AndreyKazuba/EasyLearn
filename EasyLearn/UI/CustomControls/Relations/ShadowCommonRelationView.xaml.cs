using EasyLearn.VM.ViewModels.CustomControls;
using System.Windows.Controls;

namespace EasyLearn.UI.CustomControls
{
    public partial class ShadowCommonRelationView : UserControl
    {
        public ShadowCommonRelationView(ShadowCommonRelationVM viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }

        #region Static methods
        public static ShadowCommonRelationView Create() => new ShadowCommonRelationView(new ShadowCommonRelationVM());
        #endregion
    }
}
