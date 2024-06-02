using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MES.message;
using MES.Models.project;
using MES.Models.table;

namespace MES.viewModels.Manage
{
    partial class DispatchProjectViewModel : ObservableObject
    {
        [ObservableProperty]
        private string name = "员工名称";

        [ObservableProperty]
        private string employeeNo = "员工编号";

        [ObservableProperty]
        private string taskName = "任务名称";

        [ObservableProperty]
        private DateTime startDate = DateTime.Now;

        [RelayCommand]
        private void SendDispatcher()
        {
            var patcher = new TaskModel
            {
                Id = Guid.NewGuid().ToString(),
                EmployeeNo = EmployeeNo,
                TaskName = TaskName,
                Name = Name,
                Time = StartDate
            };
            WeakReferenceMessenger.Default.Send(new AddDispatcherMessage(patcher));
        }
    }
}
