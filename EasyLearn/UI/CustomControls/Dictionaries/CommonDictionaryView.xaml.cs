using System.Windows.Controls;
using EasyLearn.Data.Models;
using EasyLearn.VM.ViewModels.CustomControls;

namespace EasyLearn.UI.CustomControls
{
    public partial class CommonDictionaryView : UserControl
    {
        #region Private fields
        private CommonDictionaryVM viewModel;
        #endregion

        #region Public props
        public int Id => viewModel.Id; 
        public bool IsCardFlipped
        {
            get => viewModel.IsCardFlipped;
            set => viewModel.IsCardFlipped = value;
        }
        #endregion

        public CommonDictionaryView(CommonDictionaryVM viewModel)
        {
            this.viewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }

        #region Static members
        public static CommonDictionaryView Create(CommonDictionary commonDictionary) => new CommonDictionaryView(new CommonDictionaryVM(commonDictionary));
        #endregion
    }
}
