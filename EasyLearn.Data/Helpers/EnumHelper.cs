using EasyLearn.Data.Attributes;
using EasyLearn.Data.Enums;
using System;
using System.Linq;
using System.Windows.Media;

namespace EasyLearn.Data.Helpers
{
    public static class EnumHelper
    {
        public static string GetRussianValue(this UnitType unitType) => unitType.GetValue<UnitType, RussianTranslatonAttribute>();
        public static string GetEnglishValue(this UnitType unitType) => unitType.GetValue<UnitType, EnglishTranslationAttribute>();
        public static string GetRussianValue(this DictionaryType dictionaryType) => dictionaryType.GetValue<DictionaryType, RussianTranslatonAttribute>();
        public static Brush GetColor(this UnitType unitType)
        {
            string hex = unitType.GetValue<UnitType, UnitTypeColorCodeAttribute>();
            return new BrushConverter().ConvertFrom(hex) as SolidColorBrush ?? Brushes.Black;
        }
        public static string GetValue<T, TAttribute>(this T enumValue)
            where T : struct, Enum where TAttribute : ValueAttribute
        {
            object? attribute = typeof(T).GetField(enumValue.ToString())?.GetCustomAttributes(typeof(TAttribute), true).First();
            if (attribute is null)
                return String.Empty;
            return ((TAttribute)attribute).Value;
        }
    }
}
