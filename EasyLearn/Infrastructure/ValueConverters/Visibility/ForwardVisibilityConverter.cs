using System;
using System.Globalization;

namespace EasyLearn.Infrastructure.ValueConverters
{
    public class ForwardVisibilityConverter : ValueConverter<ForwardVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isVisible = (bool)value;
            return isVisible ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
