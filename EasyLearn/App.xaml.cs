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

            services.AddDbContext<EasyLearnDbContext>(options =>
            {
                options.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=EasyLearn2;Trusted_Connection=True;");
            });

            services.AddSingleton<AppWindow>();
            services.AddSingleton<AppWindowVM>();

            services.AddSingleton<UsersPage>();
            services.AddSingleton<DictationPage>();
            services.AddSingleton<ListsPage>();
            services.AddSingleton<EditListsPage>();

            services.AddSingleton<UsersPageVM>();
            services.AddSingleton<ListsPageVM>();
            services.AddSingleton<EditListPageVM>();

            services.AddTransient<IRussianUnitsRepository, RussianUnitsRepository>();
            services.AddTransient<IEnglishUnitsRepository, EnglishUnitsRepository>();

            services.AddTransient<IExamplesRepository, ExamplesRepository>();

            services.AddTransient<IRelationsRepository, RelationsRepository>();
            services.AddTransient<IVerbPrepositionsRepository, VerbPrepositionsRepository>();
            services.AddTransient<IIrregularVerbsRepository, IrregularVerbsRepository>();

            services.AddTransient<ICommonWordListsRepository, CommonWordListsRepository>();
            services.AddTransient<IVerbPrepositionListsRepository, VerbPrepositionListsRepository>();

            services.AddTransient<IEasyLearnUsersRerository, EasyLearnUsersRerository>();

            return services.BuildServiceProvider();
        }
    }
}
