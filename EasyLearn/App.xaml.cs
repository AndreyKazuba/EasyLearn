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
        public static TService GetService<TService>()
        {
            TService? service = ServiceProvider.GetService<TService>();
            if (service is not null)
                return service;
            else
                throw new Exception("There is no such service");
        }

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

            services.AddSingleton<DictationPageVM>();
            services.AddSingleton<UsersPageVM>();
            services.AddSingleton<DictionariesPageVM>();
            services.AddSingleton<EditCommonDictionaryPageVM>();
            services.AddSingleton<EditVerbPrepositionDictionaryPageVM>();

            services.AddTransient<IRussianUnitRepository, RussianUnitRepository>();
            services.AddTransient<IEnglishUnitRepository, EnglishUnitRepository>();

            services.AddTransient<IExamplesRepository, ExamplesRepository>();

            services.AddTransient<ICommonRelationRepository, CommonRelationsRepository>();
            services.AddTransient<IVerbPrepositionRepository, VerbPrepositionsRepository>();
            services.AddTransient<IIrregularVerbsRepository, IrregularVerbsRepository>();

            services.AddTransient<ICommonDictionaryRepository, CommonDictionaryRepository>();
            services.AddTransient<IVerbPrepositionDictionaryRepository, VerbPrepositionDictionaryRepository>();

            services.AddTransient<IEasyLearnUserRepository, EasyLearnUsersRerository>();

            return services.BuildServiceProvider();
        }
    }
}
