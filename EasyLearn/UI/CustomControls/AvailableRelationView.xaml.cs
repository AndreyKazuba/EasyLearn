using EasyLearn.Data.Models;
using EasyLearn.Infrastructure.Enums;
using EasyLearn.VM.ViewModels.CustomControls;
using System.Windows.Controls;

namespace EasyLearn.UI.CustomControls
{
    public partial class AvailableRelationView : UserControl
    {
        #region Private fields
        private AvailableRelationVM viewModel;
        #endregion
        public AvailableRelationView(AvailableRelationVM viewModel)
        {
            this.viewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }

        #region Static members
        public static AvailableRelationView Create(CommonRelation commonRelation, DictationDirection direction) 
            => new AvailableRelationView(new AvailableRelationVM(commonRelation, direction));
        #endregion
    }
}
