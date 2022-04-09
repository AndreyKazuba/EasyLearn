using System;
using System.Windows.Controls;
using System.Windows.Input;
using EasyLearn.VM.ViewModels.Pages;

namespace EasyLearn.UI.Pages
{
    public partial class EditCommonDictionaryPage : Page
    {
        public static event Action? RussianValueTextBoxEnterDown;
        public static event Action? EnglishValueTextBoxEnterDown;
        public static event Action? RussianUnitTypeComboBoxEnterDown;
        public static event Action? EnglishUnitTypeComboBoxEnterDown;
        public static event Action? CommentValueTextBoxEnterDown;

        public EditCommonDictionaryPage(EditCommonDictionaryPageVM viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
        private void OnRussianValueTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && RussianValueTextBoxEnterDown is not null)
                RussianValueTextBoxEnterDown();
        }
        private void OnEnglishValueTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && EnglishValueTextBoxEnterDown is not null)
                EnglishValueTextBoxEnterDown();
        }
        private void OnRussianUnitTypeComboBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && RussianUnitTypeComboBoxEnterDown is not null)
                RussianUnitTypeComboBoxEnterDown();
        }
        private void OnEnglishUnitTypeComboBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && EnglishUnitTypeComboBoxEnterDown is not null)
                EnglishUnitTypeComboBoxEnterDown();
        }
        private void OnCommentValueTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && CommentValueTextBoxEnterDown is not null)
                CommentValueTextBoxEnterDown();
        }
    }
}
