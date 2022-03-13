using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace EasyLearn.Infrastructure.ValueConverters
{
    public abstract class ValueConverter<TValueConverter> : MarkupExtension, IValueConverter
        where TValueConverter : class, new()
    {
        private static TValueConverter? valueConverter;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return valueConverter ?? (valueConverter = new TValueConverter());
        }

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
