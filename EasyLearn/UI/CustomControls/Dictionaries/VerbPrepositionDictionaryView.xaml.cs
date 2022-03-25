using EasyLearn.Data.Models;
using EasyLearn.VM.ViewModels.CustomControls;
using System.Windows.Controls;

namespace EasyLearn.UI.CustomControls
{
    public partial class VerbPrepositionDictionaryView : UserControl
    {
        #region Private fields
        private VerbPrepositionDictionaryVM viewModel;
        #endregion

        #region Public props
        public int Id => viewModel.Id;
        public bool IsCardFlipped
        {
            get => viewModel.IsCardFlipped;
            set => viewModel.IsCardFlipped = value;
        }
        #endregion

        public VerbPrepositionDictionaryView(VerbPrepositionDictionaryVM viewModel)
        {
            this.viewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }

        #region Static members
        public static VerbPrepositionDictionaryView Create(VerbPrepositionDictionnary verbPrepositionDictionary) => new VerbPrepositionDictionaryView(new VerbPrepositionDictionaryVM(verbPrepositionDictionary));
        #endregion
    }
}
