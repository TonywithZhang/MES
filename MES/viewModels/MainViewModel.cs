using CommunityToolkit.Mvvm.ComponentModel;
using MES.views;
using Microsoft.Extensions.DependencyInjection;

namespace MES.viewModels
{
    internal partial class MainViewModel : ObservableObject
    {
        #region 属性
        [ObservableProperty]
        private object? page;
        #endregion

        #region 构造函数
        public MainViewModel()
        {
            this.Page = App.Current.Services.GetService<LoginView>();
        }
        #endregion

    }
}
