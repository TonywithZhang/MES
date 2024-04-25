using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MES.DataTransaction.database;
using MES.DataTransaction.file;
using MES.model;
using Microsoft.Win32;

namespace MES.viewModels
{
    internal partial class ChartPageViewModel: ObservableObject
    {
        #region 属性
        [ObservableProperty]
        private IEnumerable<ChartModel> items = [];
        #endregion

        #region 构造函数
        public ChartPageViewModel()
        {
            InitializeItems();
        }

        #endregion

        #region 命令
        [RelayCommand]
        private async Task ExportTable(string command)
        {
            var dialog = new OpenFolderDialog();
            if (dialog.ShowDialog() == true)
            {
                switch (command)
                {
                    case "导出生产计划表":
                        await ExcelUtils.ExportTableData(await ProjectService.GetProjects(), command, $"{dialog.FolderName}\\");
                        break;
                    case "导出生产任务表":
                        await ExcelUtils.ExportTableData(await ProjectService.GetTasks(), command, $"{dialog.FolderName}\\");
                        break;
                    case "导出设备信息表":
                        await ExcelUtils.ExportTableData(await DeviceService.GetDevices(), command, $"{dialog.FolderName}\\");
                        break;
                    case "导出质量数据表":
                        await ExcelUtils.ExportTableData(await QualityService.GetQualities(), command, $"{dialog.FolderName}\\");
                        break;
                    default:
                        break;
                }
            }
            
        }
        #endregion

        #region 私有方法
        private void InitializeItems()
        {
            Items = [
                new ChartModel("ProductionIconDrawingImage", "导出生产计划表"),
                new ChartModel("ProcessIconDrawingImage", "导出生产任务表"),
                new ChartModel("DeviceIconDrawingImage", "导出设备信息表"),
                new ChartModel("QualityIconDrawingImage", "导出质量数据表"),
                ];
        }
        #endregion
    }
}
