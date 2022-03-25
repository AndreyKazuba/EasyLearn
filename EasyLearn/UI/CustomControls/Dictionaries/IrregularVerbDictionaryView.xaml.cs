using EasyLearn.VM.ViewModels.CustomControls;
using System.Windows.Controls;

namespace EasyLearn.UI.CustomControls
{
    public partial class IrregularVerbDictionaryView : UserControl
    {
        public IrregularVerbDictionaryView(IrregularVerbDictionaryVM viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }

        #region Static members
        public static IrregularVerbDictionaryView Create() => new IrregularVerbDictionaryView(new IrregularVerbDictionaryVM());
        #endregion
    }
}
