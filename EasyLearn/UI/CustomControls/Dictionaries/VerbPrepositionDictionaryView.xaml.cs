using EasyLearn.Data.Models;
using EasyLearn.Infrastructure.UIInterfaces;
using EasyLearn.VM.ViewModels.CustomControls;
using System.Windows.Controls;

namespace EasyLearn.UI.CustomControls
{
    public partial class VerbPrepositionDictionaryView : UserControl, IHavingOrder
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
        public int Order => viewModel.Order;
        #endregion

        public VerbPrepositionDictionaryView(VerbPrepositionDictionaryVM viewModel)
        {
            this.viewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }

        #region Static members
        public static VerbPrepositionDictionaryView Create(VerbPrepositionDictionnary verbPrepositionDictionary) => new VerbPrepositionDictionaryView(new VerbPrepositionDictionaryVM(verbPrepositionDictionary));
        #endregion
    }
}
