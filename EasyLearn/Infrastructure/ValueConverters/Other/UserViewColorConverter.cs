using EasyLearn.Infrastructure.Helpers;
using System;
using System.Globalization;

namespace EasyLearn.Infrastructure.ValueConverters
{
    public class UserViewColorConverter : ValueConverter<UserViewColorConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isCurrent = (bool)value;
            return isCurrent ? ColorHelper.GetBrushByHex("#70db70") : ColorHelper.GetBrushByHex("#9999ff");
        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
