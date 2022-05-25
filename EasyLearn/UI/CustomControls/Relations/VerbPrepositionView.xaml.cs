using EasyLearn.Data.Models;
using EasyLearn.VM.ViewModels.CustomControls;
using System.Windows.Controls;

namespace EasyLearn.UI.CustomControls
{
    public partial class VerbPrepositionView : UserControl
    {
        private readonly VerbPrepositionVM viewModel;
        //public int Id => viewModel.Id;
        public int Order => viewModel.OrderValue;
        public VerbPrepositionView(VerbPrepositionVM viewModel)
        {
            this.viewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }

        #region Static members
        public static VerbPrepositionView Create(VerbPreposition verbPreposition) => new VerbPrepositionView(new VerbPrepositionVM(verbPreposition));
        #endregion
    }
}
