using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using EasyLearn.UI;
using EasyLearn.VM.Windows;
using EasyLearn.UI.Pages;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Data.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using EasyLearn.Data;
using EasyLearn.VM.ViewModels.Pages;

#pragma warning disable CS8602
#pragma warning disable CS8618
#pragma warning disable CS8600

namespace EasyLearn
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            ServiceProvider = ConfigureServices();
            AppWindow appWindow = ServiceProvider.GetService<AppWindow>();
            appWindow.Show();
        }

        private IServiceProvider ConfigureServices()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddDbContext<EasyLearnContext>(options =>
            {
                options.UseSqlServer(Config.ConnectionString);
            });

            services.AddSingleton<AppWindow>();
            services.AddSingleton<AppWindowVM>();

            services.AddSingleton<UsersPage>();
            services.AddSingleton<DictationPage>();
            services.AddSingleton<DictionariesPage>();
            services.AddSingleton<EditCommonDictionaryPage>();
            services.AddSingleton<EditVerbPrepositionDictionaryPage>();

            services.AddSingleton<UsersPageVM>();
            services.AddSingleton<DictionariesPageVM>();
            services.AddSingleton<EditCommonDictionaryPageVM>();
            services.AddSingleton<EditVerbPrepositionDictionaryPageVM>();

            services.AddTransient<IRussianUnitsRepository, RussianUnitsRepository>();
            services.AddTransient<IEnglishUnitsRepository, EnglishUnitsRepository>();

            services.AddTransient<IExamplesRepository, ExamplesRepository>();

            services.AddTransient<ICommonRelationsRepository, CommonRelationsRepository>();
            services.AddTransient<IVerbPrepositionRepository, VerbPrepositionsRepository>();
            services.AddTransient<IIrregularVerbsRepository, IrregularVerbsRepository>();

            services.AddTransient<ICommonDictionaryRepository, CommonDictionaryRepository>();
            services.AddTransient<IVerbPrepositionDictionaryRepository, VerbPrepositionDictionaryRepository>();

            services.AddTransient<IEasyLearnUserRerository, EasyLearnUsersRerository>();

            return services.BuildServiceProvider();
        }
    }
}
