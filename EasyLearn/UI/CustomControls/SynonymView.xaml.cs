using EasyLearn.Data.Models;
using EasyLearn.Infrastructure.Enums;
using EasyLearn.VM.ViewModels.CustomControls;
using System.Windows.Controls;

namespace EasyLearn.UI.CustomControls
{
    public partial class SynonymView : UserControl
    {
        public SynonymView(SynonymVM viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }

        #region Static methods
        public static SynonymView Create(CommonRelation commonRelation, DictationDirection direction) 
            => new SynonymView(new SynonymVM(commonRelation, direction));
        #endregion
    }
}
