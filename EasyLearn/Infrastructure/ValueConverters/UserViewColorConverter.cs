using System;
using System.Globalization;
using System.Windows.Media;

namespace EasyLearn.Infrastructure.ValueConverters
{
    public class UserViewColorConverter : ValueConverter<UserViewColorConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isCurrent = (bool)value;
            return isCurrent ? new BrushConverter().ConvertFrom("#71da8d") : new BrushConverter().ConvertFrom("#99bbff");
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
