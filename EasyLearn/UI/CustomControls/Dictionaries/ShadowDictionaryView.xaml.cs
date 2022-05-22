using System.Windows.Controls;
using EasyLearn.VM.ViewModels.CustomControls;

namespace EasyLearn.UI.CustomControls
{
    public partial class ShadowDictionaryView : UserControl
    {
        public ShadowDictionaryView(ShadowDictionaryVM viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
        public static ShadowDictionaryView Create() => new ShadowDictionaryView(new ShadowDictionaryVM());
    }
}
