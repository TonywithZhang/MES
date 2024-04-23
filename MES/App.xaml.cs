using MES.model;
using MES.viewModels;
using MES.views;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Windows;

namespace MES
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region 静态成员
        public new static App Current => (App)Application.Current;
        #endregion

        #region 属性
        public IServiceProvider Services { get; }
        #endregion

        #region 构造函数
        public App()
        {
            this.Services = ConfigureServices();
        }
        #endregion

        #region 静态方法
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            #region 注册对象
            services.AddSingleton<ILogger>(_ => new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log.txt").CreateLogger());

            services.AddSingleton<MainViewModel>();
            services.AddTransient<MainWindow>(sp => new MainWindow { DataContext = sp.GetService<MainViewModel>() });

            services.AddSingleton<LoginViewModel>();
            services.AddTransient<LoginView>(sp => new LoginView { DataContext = sp.GetService<LoginViewModel>() });

            services.AddSingleton<WorkViewModel>();
            services.AddTransient<WorkView>(sp => new WorkView { DataContext=sp.GetService<WorkViewModel>() });

            services.AddSingleton<ProductionViewModel>();
            services.AddTransient<ProductionPage>(sp => new ProductionPage { DataContext = sp.GetService<ProductionViewModel>() });
            #endregion



            return services.BuildServiceProvider();
        }
        #endregion

        #region 启动事件
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var window = Services.GetService<MainWindow>();
            window!.Show();
        }
        #endregion
    }

}
