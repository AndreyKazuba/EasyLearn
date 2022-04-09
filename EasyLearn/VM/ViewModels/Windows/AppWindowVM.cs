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
        public Command OpenDictationPage { get; set; }
        public Command OpenUsersPage { get; set; }
        public Command OpenDictionariesPageCommand { get; set; }
        public Command OpenEditCommonDictionaryPageCommand { get; set; }
        public Command OpenEditVerbPrepositionDictionaryPageCommand { get; set; }
        protected override void InitCommands()
        {
            this.OpenDictationPage = new Command(() => this.CurrentPage = Page.Dictation);
            this.OpenUsersPage = new Command(() => this.CurrentPage = Page.Users);
            this.OpenDictionariesPageCommand = new Command(() => this.CurrentPage = Page.Dictionaries);
            this.OpenEditCommonDictionaryPageCommand = new Command(() => this.CurrentPage = Page.EditCommonWordListPage);
            this.OpenEditVerbPrepositionDictionaryPageCommand = new Command(() => this.CurrentPage = Page.EditVerbPrepositionListPage);
        }

        #endregion

        private void SetStartPage() => this.CurrentPage = Page.Dictionaries;
        
    }
}
