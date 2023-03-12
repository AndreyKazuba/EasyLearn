using EasyLearn.Data.Enums;
using System.Windows;
using System.Windows.Controls;

namespace EasyLearn.VM.ViewModels.ExpandedElements
{
    public class DictionaryComboBoxItem : ComboBoxItem
    {
        public static readonly DependencyProperty DictionaryIdProperty;
        public static readonly DependencyProperty DictionaryTypeProperty;
        static DictionaryComboBoxItem()
        {
            DictionaryIdProperty = DependencyProperty.Register(nameof(DictionaryId), typeof(int), typeof(DictionaryComboBoxItem));
            DictionaryTypeProperty = DependencyProperty.Register(nameof(DictionaryType), typeof(DictionaryType), typeof(DictionaryComboBoxItem));
        }
        public DictionaryComboBoxItem(string content, int dictionaryId, DictionaryType dictionaryType, int order, bool disabled = false)
        {
            Content = content;
            DictionaryId = dictionaryId;
            DictionaryType = dictionaryType;
            IsEnabled = !disabled;
            Order = order;
        }
        public int DictionaryId
        {
            get { return (int)GetValue(DictionaryIdProperty); }
            set { SetValue(DictionaryIdProperty, value); }
        }
        public DictionaryType DictionaryType
        {
            get { return (DictionaryType)GetValue(DictionaryTypeProperty); }
            set { SetValue(DictionaryTypeProperty, value); }
        }
        public int Order { get; }
    }
}
