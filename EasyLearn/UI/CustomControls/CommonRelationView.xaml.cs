using EasyLearn.Data.Models;
using EasyLearn.VM.ViewModels.CustomControls;
using System.Windows.Controls;

namespace EasyLearn.UI.CustomControls
{
    public partial class CommonRelationView : UserControl
    {
        public CommonRelationVM ViewModel { get; }
        public CommonRelationView(CommonRelationVM viewModel)
        {
            ViewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }
        public static CommonRelationView Create(CommonRelation commonRelation) => new CommonRelationView(new CommonRelationVM(commonRelation));
    }
}
