using System.Windows.Controls;
using EasyLearn.VM.ViewModels.Pages;

namespace EasyLearn.UI.Pages
{
    public partial class DictionariesPage : Page
    {
        public DictionariesPage(DictionariesPageVM viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
