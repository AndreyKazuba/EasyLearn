using EasyLearn.Data.Models;
using EasyLearn.Infrastructure.Enums;
using EasyLearn.VM.ViewModels.CustomControls;
using System.Windows.Controls;

namespace EasyLearn.UI.CustomControls
{
    public partial class SynonymView : UserControl
    {
        #region Private fields
        private SynonymVM viewModel;
        #endregion
        public SynonymView(SynonymVM viewModel)
        {
            this.viewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }

        #region Static members
        public static SynonymView Create(CommonRelation commonRelation, DictationDirection direction) 
            => new SynonymView(new SynonymVM(commonRelation, direction));
        #endregion
    }
}
