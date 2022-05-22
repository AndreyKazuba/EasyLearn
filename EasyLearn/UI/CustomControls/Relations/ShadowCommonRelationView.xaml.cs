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
        public static ShadowCommonRelationView Create() => new ShadowCommonRelationView(new ShadowCommonRelationVM());
    }
}
