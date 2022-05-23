using EasyLearn.VM.ViewModels.Pages;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EasyLearn.UI.Pages
{
    public partial class EditVerbPrepositionDictionaryPage : Page
    {
        public static event Action? VerbValueTextBoxEnterDown;
        public static event Action? PrepositionValueTextBoxEnterDown;
        public static event Action? TranslationValueTextBoxEnterDown;
        public static event Action? ExampleRussianValueTextBoxEnterDown;
        public static event Action? ExampleEnglishValueTextBoxEnterDown;
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
        private void OnExampleRussianValueTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && ExampleRussianValueTextBoxEnterDown is not null)
                ExampleRussianValueTextBoxEnterDown();
        }
        private void OnExampleEnglishValueTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && ExampleEnglishValueTextBoxEnterDown is not null)
                ExampleEnglishValueTextBoxEnterDown();
        }
        private void WarningIcon_MouseEnter(object sender, MouseEventArgs e) => this.warningIcon.Foreground = Brushes.OrangeRed;
        private void WarningIcon_MouseLeave(object sender, MouseEventArgs e) => this.warningIcon.Foreground = Brushes.Orange;
    }
}
