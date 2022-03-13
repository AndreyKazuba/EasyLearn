using EasyLearn.VM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        protected override void InitCommands()
        {
            this.OpenDictationPage = new DelegateCommand(arg => this.CurrentPage = Page.Dictation);
            this.OpenUsersPage = new DelegateCommand(arg => this.CurrentPage = Page.Users);
            this.OpenListsPage = new DelegateCommand(arg => this.CurrentPage = Page.Lists);
        }

        #endregion

        private void SetStartPage()
        {
            this.CurrentPage = Page.Users;
        }
    }
}
