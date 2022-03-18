using EasyLearn.Data.Enums;
using System.Windows;
using System.Windows.Controls;

namespace EasyLearn.VM.ViewModels.ExpandedElements
{
    public class DictionaryUserControl : UserControl
    {
        public static readonly DependencyProperty IdProperty;
        public static readonly DependencyProperty DictionaryTypeProperty;
        static DictionaryUserControl()
        {
            IdProperty = DependencyProperty.Register(nameof(Id), typeof(int), typeof(DictionaryUserControl));
            DictionaryTypeProperty = DependencyProperty.Register(nameof(DictionaryType), typeof(DictionaryType), typeof(DictionaryUserControl));
        }
        public DictionaryUserControl(int id, DictionaryType dictionaryType, object dataContext)
        {
            Id = id;
            DataContext = dataContext;
            DictionaryType = dictionaryType;
        }
        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        public DictionaryType DictionaryType
        {
            get { return (DictionaryType)GetValue(DictionaryTypeProperty); }
            set { SetValue(DictionaryTypeProperty, value); }
        }
    }
}
