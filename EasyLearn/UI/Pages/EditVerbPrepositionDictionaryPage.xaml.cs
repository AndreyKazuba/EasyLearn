using EasyLearn.VM.ViewModels.Pages;
using System.Windows.Controls;

namespace EasyLearn.UI.Pages
{
    public partial class EditVerbPrepositionDictionaryPage : Page
    {
        public EditVerbPrepositionDictionaryPage(EditVerbPrepositionDictionaryPageVM viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
