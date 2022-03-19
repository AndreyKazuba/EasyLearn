using EasyLearn.VM.ViewModels.Pages;
using System.Windows.Controls;

namespace EasyLearn.UI.Pages
{
    public partial class DictationPage : Page
    {
        public DictationPage(DictationPageVM viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
