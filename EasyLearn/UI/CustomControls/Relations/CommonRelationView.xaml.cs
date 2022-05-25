using EasyLearn.Data.Models;
using EasyLearn.VM.ViewModels.CustomControls;
using System.Windows.Controls;

namespace EasyLearn.UI.CustomControls
{
    public partial class CommonRelationView : UserControl
    {
        #region Private fields
        private readonly CommonRelationVM viewModel;
        #endregion

        #region Public props
        public int Id => viewModel.Id;
        public int Order => viewModel.OrderValue;
        public string RussianValue => viewModel.RussianValue;
        public string EnglishValue => viewModel.EnglishValue;
        #endregion

        public void UpdateView(CommonRelation commonRelation) => this.viewModel.Set(commonRelation);
        public void Collapse() => this.viewModel.Collapse();
        public void Show() => this.viewModel.Show();

        public CommonRelationView(CommonRelationVM viewModel)
        {
            this.viewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }

        #region Static members
        public static CommonRelationView Create(CommonRelation commonRelation) => new CommonRelationView(new CommonRelationVM(commonRelation));
        #endregion
    }
}
