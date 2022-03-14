using System;
using System.Globalization;

namespace EasyLearn.Infrastructure.ValueConverters
{
    public class ForwardCollapseConverter : ValueConverter<ForwardCollapseConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isCollapsed = (bool)value;
            return isCollapsed ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
