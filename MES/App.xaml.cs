using System.Windows;
using MES.viewModels;
using MES.viewModels.Manage;
using MES.views;
using MES.views.Manage;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

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
            services.AddSingleton<ILogger>(_ =>
                new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File("log.txt")
                    .CreateLogger()
            );

            services.AddSingleton<MainViewModel>();
            services.AddTransient<MainWindow>(sp => new MainWindow
            {
                DataContext = sp.GetService<MainViewModel>()
            });

            services.AddSingleton<LoginViewModel>();
            services.AddTransient<LoginView>(sp => new LoginView
            {
                DataContext = sp.GetService<LoginViewModel>()
            });

            services.AddSingleton<WorkViewModel>();
            services.AddTransient<WorkView>(sp => new WorkView
            {
                DataContext = sp.GetService<WorkViewModel>()
            });

            services.AddSingleton<ProductionViewModel>();
            services.AddTransient<ProductionPage>(sp => new ProductionPage
            {
                DataContext = sp.GetService<ProductionViewModel>()
            });

            services.AddSingleton<ProcessPageViewModel>();
            services.AddTransient<ProcessPage>(sp => new ProcessPage
            {
                DataContext = sp.GetService<ProcessPageViewModel>()
            });

            services.AddSingleton<QualityPageViewModel>();
            services.AddTransient<QualityPage>(sp => new QualityPage
            {
                DataContext = sp.GetService<QualityPageViewModel>()
            });

            services.AddSingleton<DevicePageViewModel>();
            services.AddTransient<DevicePage>(sp => new DevicePage
            {
                DataContext = sp.GetService<DevicePageViewModel>()
            });

            services.AddSingleton<ChartPageViewModel>();
            services.AddTransient<ChartPage>(sp => new ChartPage
            {
                DataContext = sp.GetService<ChartPageViewModel>()
            });

            services.AddSingleton<AddProjectViewModel>();
            services.AddTransient<AddProductionPage>(sp => new AddProductionPage
            {
                DataContext = sp.GetService<AddProjectViewModel>()
            });

            services.AddSingleton<DispatchProjectViewModel>();
            services.AddTransient<DispatchProjectPage>(sp => new DispatchProjectPage
            {
                DataContext = sp.GetService<DispatchProjectViewModel>()
            });
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
