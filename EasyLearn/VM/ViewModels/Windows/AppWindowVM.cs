using EasyLearn.VM.Core;
using EasyLearn.Infrastructure.Enums;

namespace EasyLearn.VM.Windows
{
    public class AppWindowVM : ViewModel
    {
        public Page CurrentPage { get; set; }

        public AppWindowVM()
        {
            SetStartPage();
        }

        #region Commands

        public DelegateCommand OpenDictationPage { get; set; }
        public DelegateCommand OpenUsersPage { get; set; }
        public DelegateCommand OpenListsPage { get; set; }
        public DelegateCommand OpenEditCommonWordListPage { get; set; }
        public DelegateCommand OpenEditVerbPrepositionListPage { get; set; }

        protected override void InitCommands()
        {
            this.OpenDictationPage = new DelegateCommand(arg => this.CurrentPage = Page.Dictation);
            this.OpenUsersPage = new DelegateCommand(arg => this.CurrentPage = Page.Users);
            this.OpenListsPage = new DelegateCommand(arg => this.CurrentPage = Page.Lists);
            this.OpenEditCommonWordListPage = new DelegateCommand(arg => this.CurrentPage = Page.EditCommonWordListPage);
            this.OpenEditVerbPrepositionListPage = new DelegateCommand(arg => this.CurrentPage = Page.EditVerbPrepositionListPage);
        }

        #endregion

        private void SetStartPage() => this.CurrentPage = Page.Users;
        
    }
}
