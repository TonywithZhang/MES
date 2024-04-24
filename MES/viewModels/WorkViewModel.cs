using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MES.model;
using MES.views;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace MES.viewModels
{
    internal partial class WorkViewModel : ObservableObject
    {
        #region 属性
        public ObservableCollection<IconModel> Icons { get; private set; } = [
            new IconModel(){IconKind = "BagSuitcase", IsActive = true, Desc = "生产调度"},
            new IconModel(){IconKind = "Cached", IsActive = false, Desc = "生产过程监控"},
            new IconModel(){IconKind = "Cog", IsActive = false, Desc = "质量管理"},
            new IconModel(){IconKind = "Devices", IsActive = false, Desc = "设备管理"},
            new IconModel(){IconKind = "ChartBellCurve", IsActive = false, Desc = "报表统计"},
            ];
        [ObservableProperty]
        private object? page;
        #endregion

        #region 私有字段
        private readonly IServiceProvider services = App.Current.Services;
        #endregion

        #region 构造函数
        public WorkViewModel()
        {
            Page = services.GetService<ProductionPage>();
        }
        #endregion

        #region 私有函数
        [RelayCommand]
        private void SwitchPage(string _page)
        {
            Page = _page switch
            {
                "BagSuitcase"    => services.GetService<ProductionPage>(),
                "Cached"         => services.GetService<ProcessPage>(),
                "Cog"            => services.GetService<QualityPage>(),
                "Devices"        => services.GetService<DevicePage>(),
                "ChartBellCurve" => services.GetService<ChartPage>(),
                _                => services.GetService<ProductionPage>(),
            };
            foreach (var item in Icons)
            {
                item.IsActive = false;
            }
            Icons.First(c => c.IconKind == _page).IsActive = true;
        }
        #endregion
    }
}
