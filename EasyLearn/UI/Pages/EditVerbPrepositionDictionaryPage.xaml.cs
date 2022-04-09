using EasyLearn.VM.ViewModels.Pages;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace EasyLearn.UI.Pages
{
    public partial class EditVerbPrepositionDictionaryPage : Page
    {
        public static event Action? VerbValueTextBoxEnterDown;
        public static event Action? PrepositionValueTextBoxEnterDown;
        public static event Action? TranslationValueTextBoxEnterDown;
        public static event Action? CommentValueTextBoxEnterDown;
        public EditVerbPrepositionDictionaryPage(EditVerbPrepositionDictionaryPageVM viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
        private void OnVerbValueTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && VerbValueTextBoxEnterDown is not null)
                VerbValueTextBoxEnterDown();
        }
        private void OnPrepositionValueTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && PrepositionValueTextBoxEnterDown is not null)
                PrepositionValueTextBoxEnterDown();
        }
        private void OnTranslationValueKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && TranslationValueTextBoxEnterDown is not null)
                TranslationValueTextBoxEnterDown();
        }
        private void OnCommentValueTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && CommentValueTextBoxEnterDown is not null)
                CommentValueTextBoxEnterDown();
        }
    }
}
