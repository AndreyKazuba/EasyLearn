using EasyLearn.VM.ViewModels.Pages;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EasyLearn.UI.Pages
{
    public partial class EditVerbPrepositionDictionaryPage : Page
    {
        public static event Action? AddingWindowVerbValueTextBoxEnterDown;
        public static event Action? AddingWindowPrepositionValueTextBoxEnterDown;
        public static event Action? AddingWindowTranslationValueTextBoxEnterDown;
        public static event Action? AddingWindowExampleRussianValueTextBoxEnterDown;
        public static event Action? AddingWindowExampleEnglishValueTextBoxEnterDown;
        public static event Action? UpdateWindowExampleRussianValueTextBoxEnterDown;
        public static event Action? UpdateWindowExampleEnglishValueTextBoxEnterDown;
        public EditVerbPrepositionDictionaryPage(EditVerbPrepositionDictionaryPageVM viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
        private void OnVerbValueTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && AddingWindowVerbValueTextBoxEnterDown is not null)
                AddingWindowVerbValueTextBoxEnterDown();
        }
        private void OnPrepositionValueTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && AddingWindowPrepositionValueTextBoxEnterDown is not null)
                AddingWindowPrepositionValueTextBoxEnterDown();
        }
        private void OnTranslationValueKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && AddingWindowTranslationValueTextBoxEnterDown is not null)
                AddingWindowTranslationValueTextBoxEnterDown();
        }
        private void OnAddingWindowExampleRussianValueTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && AddingWindowExampleRussianValueTextBoxEnterDown is not null)
                AddingWindowExampleRussianValueTextBoxEnterDown();
        }
        private void OnAddingWindowExampleEnglishValueTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && AddingWindowExampleEnglishValueTextBoxEnterDown is not null)
                AddingWindowExampleEnglishValueTextBoxEnterDown();
        }
        private void OnUpdateWindowExampleRussianValueTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && UpdateWindowExampleRussianValueTextBoxEnterDown is not null)
                UpdateWindowExampleRussianValueTextBoxEnterDown();
        }
        private void OnUpdateWindowExampleEnglishValueTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && UpdateWindowExampleEnglishValueTextBoxEnterDown is not null)
                UpdateWindowExampleEnglishValueTextBoxEnterDown();
        }
    }
}
