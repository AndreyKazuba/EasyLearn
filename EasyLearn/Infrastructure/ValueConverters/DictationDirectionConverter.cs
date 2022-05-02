using EasyLearn.Infrastructure.Enums;
using System;
using System.Globalization;

namespace EasyLearn.Infrastructure.ValueConverters
{
    public class DictationDirectionConverter : ValueConverter<DictationDirectionConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value == 1;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? (DictationDirection)1 : (DictationDirection)0;
        }
    }
}