using System;
using System.Globalization;

namespace EasyLearn.Infrastructure.ValueConverters
{
    public class ReverseConverter : ValueConverter<ReverseConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
