using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfPicDB.BusinessLayer;
using WpfPicDB.DataAccessLayer;
using WpfPicDB.ViewModels;
using WpfPicDB.Views;

namespace WpfPicDB
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _provider;

        public IServiceProvider ServiceProvider => _provider;

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<PhotographerBL>();
            services.AddSingleton<PictureBL>();
            services.AddSingleton<FileDAL>();
            services.AddSingleton<DatabaseDAL>();
            services.AddSingleton<EditPhotographerVM>();
            services.AddSingleton<ListPhotographerVM>();
            services.AddSingleton<ImageTabVM>();
            services.AddSingleton<ImageScrollerVM>();
            services.AddSingleton<ImagePreviewVM>();
            services.AddSingleton<TaskbarVM>();
            services.AddSingleton<ExportVM>();
            services.AddSingleton<MainWindowVM>();
            services.AddSingleton<MainWindow>();            
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _provider = services.BuildServiceProvider();
            MainWindow mainwindow = _provider.GetRequiredService<MainWindow>();
            mainwindow.Show();
        }
    }
}
