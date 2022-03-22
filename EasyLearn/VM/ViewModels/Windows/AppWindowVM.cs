using EasyLearn.VM.Core;
using EasyLearn.Infrastructure.Enums;
using System;

namespace EasyLearn.VM.Windows
{
    public class AppWindowVM : ViewModel
    {
        private Page currentPage;
        public event Action CurrentPageChanged;

        public Page CurrentPage
        {
            get { return currentPage; }
            set 
            { 
                currentPage = value;
                if (this.CurrentPageChanged is not null)
                    this.CurrentPageChanged.Invoke();
            }
        }

        public AppWindowVM()
        {
            SetStartPage();
        }

        #region Commands

        public DelegateCommand OpenDictationPage { get; set; }
        public DelegateCommand OpenUsersPage { get; set; }
        public DelegateCommand OpenDictionariesPageCommand { get; set; }
        public DelegateCommand OpenEditCommonDictionaryPageCommand { get; set; }
        public DelegateCommand OpenEditVerbPrepositionDictionaryPageCommand { get; set; }

        protected override void InitCommands()
        {
            this.OpenDictationPage = new DelegateCommand(arg => this.CurrentPage = Page.Dictation);
            this.OpenUsersPage = new DelegateCommand(arg => this.CurrentPage = Page.Users);
            this.OpenDictionariesPageCommand = new DelegateCommand(arg => this.CurrentPage = Page.Dictionaries);
            this.OpenEditCommonDictionaryPageCommand = new DelegateCommand(arg => this.CurrentPage = Page.EditCommonWordListPage);
            this.OpenEditVerbPrepositionDictionaryPageCommand = new DelegateCommand(arg => this.CurrentPage = Page.EditVerbPrepositionListPage);
        }

        #endregion

        private void SetStartPage() => this.CurrentPage = Page.Dictionaries;
        
    }
}
