using System;
using System.Globalization;

namespace EasyLearn.Infrastructure.ValueConverters
{
    public class ReverseConverter : ValueConverter<ReverseConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => !(bool)value;
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => !(bool)value;
    }
}
