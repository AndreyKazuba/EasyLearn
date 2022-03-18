using EasyLearn.Data.Enums;
using System.Windows;
using System.Windows.Controls;

namespace EasyLearn.VM.ViewModels.ExpandedElements
{
    public class DictionaryTypeComboBoxItem : ComboBoxItem
    {
        public static readonly DependencyProperty DictionaryTypeProperty;
        static DictionaryTypeComboBoxItem()
        {
            DictionaryTypeProperty = DependencyProperty.Register(nameof(DictionaryType), typeof(DictionaryType), typeof(DictionaryTypeComboBoxItem));
        }
        public DictionaryTypeComboBoxItem(string content, DictionaryType dictionaryType)
        {
            this.DictionaryType = dictionaryType;
            this.Content = content;
        }
        public DictionaryType DictionaryType
        {
            get { return (DictionaryType)GetValue(DictionaryTypeProperty); }
            set { SetValue(DictionaryTypeProperty, value); }
        }
    }
}
