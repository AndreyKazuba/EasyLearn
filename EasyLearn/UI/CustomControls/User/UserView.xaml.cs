using System.Windows.Controls;
using EasyLearn.Data.Models;
using EasyLearn.VM.ViewModels.CustomControls;

namespace EasyLearn.UI.CustomControls
{
    public partial class UserView : UserControl
    {
        #region Private fields
        private UserVM viewModel;
        #endregion

        #region Public props
        public int Id => viewModel.Id;
        public bool IsCurrent
        {
            get => viewModel.IsCurrent;
            set => viewModel.IsCurrent = value;
        }
        public bool IsCardFlipped
        {
            get => viewModel.IsCardFlipped;
            set => viewModel.IsCardFlipped = value;
        }
        #endregion

        public UserView(UserVM viewModel)
        {
            this.DataContext = viewModel;
            this.viewModel = viewModel;
            InitializeComponent();
        }

        #region Static members
        public static UserView Create(EasyLearnUser user) => new UserView(new UserVM(user));
        #endregion
    }
}
