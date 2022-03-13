using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLearn.Infrastructure.Enums;
using EasyLearn.UI.Pages;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable CS8603

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
                    return App.ServiceProvider.GetService<DictationPage>();
                case Page.Users:
                    return App.ServiceProvider.GetService<UsersPage>();
                case Page.Lists:
                    return App.ServiceProvider.GetService<ListsPage>();
                default: 
                    throw new Exception();
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
