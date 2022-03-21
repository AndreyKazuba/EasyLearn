using EasyLearn.VM.ViewModels.Pages;
using System.Windows.Controls;
using System.Windows.Input;

namespace EasyLearn.UI.Pages
{
    public partial class DictationPage : Page
    {
        private DictationPageVM viewModel;
        public DictationPage(DictationPageVM viewModel)
        {
            this.DataContext = viewModel;
            this.viewModel = viewModel;
            InitializeComponent();
        }

        private void dictationTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                this.viewModel.PressEnterCommand.Execute();
        }
    }
}
