using EasyLearn.VM.ViewModels.CustomControls;
using System.Windows.Controls;

namespace EasyLearn.UI.CustomControls
{
    public partial class IrregularVerbDictionaryView : UserControl
    {
        public IrregularVerbDictionaryVM ViewModel { get; }
        public IrregularVerbDictionaryView(IrregularVerbDictionaryVM viewModel)
        {
            this.ViewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }

        public static IrregularVerbDictionaryView Create() => new IrregularVerbDictionaryView(new IrregularVerbDictionaryVM());
    }
}
