using EasyLearn.Infrastructure.UIInterfaces;
using EasyLearn.VM.ViewModels.CustomControls;
using System.Windows.Controls;

namespace EasyLearn.UI.CustomControls
{
    public partial class IrregularVerbDictionaryView : UserControl, IHavingOrder
    {
        #region Public props
        public int Order => 120;
        #endregion

        public IrregularVerbDictionaryView(IrregularVerbDictionaryVM viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        #region Static members
        public static IrregularVerbDictionaryView Create() => new IrregularVerbDictionaryView(new IrregularVerbDictionaryVM());
        #endregion
    }
}
