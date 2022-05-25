using System;
using System.Windows.Controls;
using System.Windows.Input;
using EasyLearn.VM.ViewModels.Pages;

namespace EasyLearn.UI.Pages
{
    public partial class EditCommonDictionaryPage : Page
    {
        public static event Action? AddingWindowRussianValueTextBoxEnterDown;
        public static event Action? AddingWindowEnglishValueTextBoxEnterDown;
        public static event Action? AddingWindowRussianUnitTypeComboBoxEnterDown;
        public static event Action? AddingWindowEnglishUnitTypeComboBoxEnterDown;
        public static event Action? AddingWindowCommentValueTextBoxEnterDown;
        public static event Action? AddingWindowExampleRussianValueTextBoxEnterDown;
        public static event Action? AddingWindowExampleEnglishValueTextBoxEnterDown;
        public static event Action? UpdateWindowExampleRussianValueTextBoxEnterDown;
        public static event Action? UpdateWindowExampleEnglishValueTextBoxEnterDown;

        public EditCommonDictionaryPage(EditCommonDictionaryPageVM viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
        private void OnRussianValueTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && AddingWindowRussianValueTextBoxEnterDown is not null)
                AddingWindowRussianValueTextBoxEnterDown();
        }
        private void OnEnglishValueTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && AddingWindowEnglishValueTextBoxEnterDown is not null)
                AddingWindowEnglishValueTextBoxEnterDown();
        }
        private void OnRussianUnitTypeComboBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && AddingWindowRussianUnitTypeComboBoxEnterDown is not null)
                AddingWindowRussianUnitTypeComboBoxEnterDown();
        }
        private void OnEnglishUnitTypeComboBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && AddingWindowEnglishUnitTypeComboBoxEnterDown is not null)
                AddingWindowEnglishUnitTypeComboBoxEnterDown();
        }
        private void OnCommentValueTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && AddingWindowCommentValueTextBoxEnterDown is not null)
                AddingWindowCommentValueTextBoxEnterDown();
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
