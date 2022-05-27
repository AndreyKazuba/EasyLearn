using System;
using System.Globalization;
using EasyLearn.Infrastructure.Enums;   

namespace EasyLearn.Infrastructure.ValueConverters
{
    public class DictationDirectionConverter : ValueConverter<DictationDirectionConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (int)value == 1;
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (bool)value ? (DictationDirection)1 : (DictationDirection)0;
    }
}