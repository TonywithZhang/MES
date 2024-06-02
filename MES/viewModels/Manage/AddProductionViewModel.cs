using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MES.message;

namespace MES.viewModels.Manage
{
    partial class AddProjectViewModel : ObservableObject
    {
        #region 属性
        [ObservableProperty]
        private string name = "请输入项目名称";

        [ObservableProperty]
        private DateTime startDate = DateTime.Now;

        [ObservableProperty]
        private DateTime endDate = DateTime.Now;

        [ObservableProperty]
        private string employee = "请输入负责人";

        [ObservableProperty]
        private string employeeNo = "请输入员工编号";
        #endregion

        #region 命令
        [RelayCommand]
        private void SendProject()
        {
            WeakReferenceMessenger.Default.Send(
                new AddProjectMessage(
                    new Models.table.ProjectModel
                    {
                        EmployeeNo = EmployeeNo,
                        EndTime = EndDate,
                        Id = Guid.NewGuid().ToString(),
                        Owner = Employee,
                        StartTime = StartDate,
                        Title = Name
                    }
                )
            );
        }
        #endregion
    }
}
