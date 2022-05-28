using EasyLearn.Infrastructure.Helpers;
using EasyLearn.Infrastructure.UIConstants;
using System;
using System.Globalization;

namespace EasyLearn.Infrastructure.ValueConverters
{
    public class UserViewColorConverter : ValueConverter<UserViewColorConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isCurrent = (bool)value;
            return isCurrent ? ColorCodes.EasyGreen.GetBrushByHex() : ColorCodes.MainColorLight.GetBrushByHex();
        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
