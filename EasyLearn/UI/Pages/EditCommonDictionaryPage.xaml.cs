using System.Windows.Controls;
using EasyLearn.VM.ViewModels.Pages;

namespace EasyLearn.UI.Pages
{
    public partial class EditCommonDictionaryPage : Page
    {
        public EditCommonDictionaryPage(EditCommonDictionaryPageVM viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
