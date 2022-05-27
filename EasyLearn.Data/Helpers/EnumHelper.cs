using EasyLearn.Data.Attributes;
using EasyLearn.Data.Enums;
using System;
using System.Linq;
using System.Windows.Media;

namespace EasyLearn.Data.Helpers
{
    public static class EnumHelper
    {
        public static string GetRussianValue(this UnitType unitType) => unitType.GetStringValue<UnitType, RussianTranslationAttribute>();
        public static string GetEnglishValue(this UnitType unitType) => unitType.GetStringValue<UnitType, EnglishTranslationAttribute>();
        public static string GetRussianValue(this DictionaryType dictionaryType) => dictionaryType.GetStringValue<DictionaryType, RussianTranslationAttribute>();
        public static Brush GetColor(this UnitType unitType)
        {
            string hex = unitType.GetStringValue<UnitType, UnitTypeColorCodeAttribute>();
            return new BrushConverter().ConvertFrom(hex) as SolidColorBrush ?? Brushes.Black;
        }
        public static string GetStringValue<T, TAttribute>(this T enumValue)
            where T : struct, Enum where TAttribute : StringValueAttribute
        {
            object? attribute = typeof(T).GetField(enumValue.ToString())?.GetCustomAttributes(typeof(TAttribute), true).First();
            if (attribute is null)
                return string.Empty;
            return ((TAttribute)attribute).Value;
        }
        public static int GetInt32Value<T, TAttribute>(this T enumValue)
            where T : struct, Enum where TAttribute : Int32ValueAttribute
        {
            object? attribute = typeof(T).GetField(enumValue.ToString())?.GetCustomAttributes(typeof(TAttribute), true).First();
            if (attribute is null)
                return 0;
            return ((TAttribute)attribute).Value;
        }
        public static int GetAnswerSignificanceValue(this AnswerVariation answerVariation) => answerVariation.GetInt32Value<AnswerVariation, AnswerSignificanceAttribute>();
    }
}
