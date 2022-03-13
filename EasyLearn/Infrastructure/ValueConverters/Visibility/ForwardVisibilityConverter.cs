using System;
using System.Globalization;
using System.Windows;

namespace EasyLearn.Infrastructure.ValueConverters
{
    public class ForwardVisibilityConverter : ValueConverter<ForwardVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isVisible = (bool)value;
            return isVisible ? Visibility.Visible : Visibility.Hidden;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
