using System.Windows.Controls;
using EasyLearn.Data.Models;
using EasyLearn.Infrastructure.UIInterfaces;
using EasyLearn.VM.ViewModels.CustomControls;

namespace EasyLearn.UI.CustomControls
{
    public partial class CommonDictionaryView : UserControl, IHavingOrder
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
        public int Order => viewModel.Order;
        #endregion

        public CommonDictionaryView(CommonDictionaryVM viewModel)
        {
            this.viewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }

        #region Static members
        public static CommonDictionaryView Create(CommonDictionary commonDictionary) => new CommonDictionaryView(new CommonDictionaryVM(commonDictionary));
        #endregion
    }
}
