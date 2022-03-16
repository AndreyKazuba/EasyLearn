using EasyLearn.Data.Enums;
using System.Windows;
using System.Windows.Controls;

namespace EasyLearn.VM.ViewModels.ExpandedElements
{
    public class UnitTypeComboBoxItem : ComboBoxItem
    {
        public static readonly DependencyProperty UnitTypeProperty;
        static UnitTypeComboBoxItem()
        {
            UnitTypeProperty = DependencyProperty.Register(nameof(UnitType), typeof(UnitType), typeof(UnitTypeComboBoxItem));
        }
        public UnitTypeComboBoxItem(string content, UnitType unitType)
        {
            this.Content = content;
            this.UnitType = unitType;
        }
        public UnitType UnitType
        {
            get { return (UnitType)GetValue(UnitTypeProperty); }
            set { SetValue(UnitTypeProperty, value); }
        }
    }
}
