using System.Windows.Controls;
using EasyLearn.Data.Models;
using EasyLearn.VM.ViewModels.CustomControls;

namespace EasyLearn.UI.CustomControls
{
    public partial class CommonDictionaryView : UserControl
    {
        public CommonDictionaryVM ViewModel { get; }
        public CommonDictionaryView(CommonDictionaryVM viewModel)
        {
            this.ViewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }

        public static CommonDictionaryView Create(CommonDictionary commonDictionary) => new CommonDictionaryView(new CommonDictionaryVM(commonDictionary));
    }
}
