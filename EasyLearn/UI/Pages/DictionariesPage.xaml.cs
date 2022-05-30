using System;
using System.Windows.Controls;
using System.Windows.Input;
using EasyLearn.VM.ViewModels.Pages;

namespace EasyLearn.UI.Pages
{
    public partial class DictionariesPage : Page
    {
        #region Events
        public static event Action? DictionaryTypeComboBoxEnterDown;
        public static event Action? DictionaryNameTextBoxEnterDown;
        #endregion

        public DictionariesPage(DictionariesPageVM viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        #region UI event handlers
        private void OnDictionaryNameTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && DictionaryNameTextBoxEnterDown is not null)
                DictionaryNameTextBoxEnterDown();
        }
        private void OnDictionaryTypeComboBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && DictionaryTypeComboBoxEnterDown is not null)
                DictionaryTypeComboBoxEnterDown();
        }
        #endregion
    }
}
