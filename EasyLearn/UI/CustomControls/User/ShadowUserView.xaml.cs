using EasyLearn.VM.ViewModels.CustomControls;
using System.Windows.Controls;

namespace EasyLearn.UI.CustomControls
{
    public partial class ShadowUserView : UserControl
    {
        public ShadowUserView(ShadowUserVM viewModel) 
        {
            this.DataContext = viewModel;
            this.InitializeComponent();
        }

        #region Static methods
        public static ShadowUserView Create() => new ShadowUserView(new ShadowUserVM());
        #endregion
    }
}
