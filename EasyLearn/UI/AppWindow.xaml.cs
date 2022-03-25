using System.Windows;
using EasyLearn.VM.Windows;

namespace EasyLearn.UI
{
    public partial class AppWindow : Window
    {
        public AppWindow(AppWindowVM viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
