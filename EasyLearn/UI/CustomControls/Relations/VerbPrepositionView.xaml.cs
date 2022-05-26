using EasyLearn.Data.Models;
using EasyLearn.VM.ViewModels.CustomControls;
using System.Windows.Controls;

namespace EasyLearn.UI.CustomControls
{
    public partial class VerbPrepositionView : UserControl
    {
        #region Private fields
        private readonly VerbPrepositionVM viewModel;
        #endregion

        #region Public props
        public int Id => viewModel.Id;  
        public int Order => viewModel.OrderValue;
        public string VerbValue => viewModel.VerbValue;
        public string PrepositionValue => viewModel.PrepositionValue;
        #endregion

        public VerbPrepositionView(VerbPrepositionVM viewModel)
        {
            this.viewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }

        #region Public methods
        public void UpdateView(VerbPreposition verbPreposition) => viewModel.Set(verbPreposition);
        public void Collapse() => viewModel.Collapse();
        public void Show() => viewModel.Show();
        #endregion

        #region Static members
        public static VerbPrepositionView Create(VerbPreposition verbPreposition) => new VerbPrepositionView(new VerbPrepositionVM(verbPreposition));
        #endregion
    }
}
