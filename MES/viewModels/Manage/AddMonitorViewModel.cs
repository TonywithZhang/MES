using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MES.message;
using MES.Models.project;

namespace MES.viewModels.Manage
{
    partial class AddMonitorViewModel : ObservableObject
    {
        [ObservableProperty]
        private string name = "姓名";

        [ObservableProperty]
        private string taskName = "项目名";

        [RelayCommand]
        private void SendMonitor()
        {
            var message = $"{Name}检查了{TaskName}的进度";
            var monitor = new MonitorModel(message, DateTime.Now);
            WeakReferenceMessenger.Default.Send(new AddMonitorMessage(monitor));
        }
    }
}
