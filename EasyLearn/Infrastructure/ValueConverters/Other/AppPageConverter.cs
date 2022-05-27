using System;
using System.Globalization;
using EasyLearn.Infrastructure.Enums;
using EasyLearn.Infrastructure.Exceptions;
using EasyLearn.UI.Pages;

namespace EasyLearn.Infrastructure.ValueConverters
{
    public class AppPageConverter : ValueConverter<AppPageConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Page page = (Page)value;

            switch (page)
            {
                case Page.Dictation:
                    return App.GetService<DictationPage>();
                case Page.Users:
                    return App.GetService<UsersPage>();
                case Page.Dictionaries:
                    return App.GetService<DictionariesPage>();
                case Page.EditCommonDictionaryPage:
                    return App.GetService<EditCommonDictionaryPage>();
                case Page.EditVerbPrepositionDictionaryPage:
                    return App.GetService<EditVerbPrepositionDictionaryPage>();
                default:
                    throw new Exception(ExceptionMessagesHelper.EnumElementNotImplementedInSwitch);
            }
        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
