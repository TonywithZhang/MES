using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MES.DataTransaction.bean;
using MES.DataTransaction.database;
using MES.Models.project;
using MES.Models.table;
using System.Collections.ObjectModel;

namespace MES.viewModels
{
    partial class ProductionViewModel : ObservableObject
    {
        #region 属性
        public ObservableCollection<ProjectModel> Projects { get; private set; } = [];
        public ObservableCollection<DispatcherModel> DispatcherInfos { get; private set; } = [];
        public ObservableCollection<MonitorModel> MonitorInfos { get; private set; } = [];

        [ObservableProperty]
        private string projectCount = string.Empty;
        #endregion

        #region 构造函数
        public ProductionViewModel()
        {
            new Action(async () => { (await ProjectService.GetProjects()).ForEach(p => Projects.Add(p));ProjectCount = Projects.Count().ToString(); }).Invoke();
            new Action(async() => (await ProjectService.GetTasks()).ToList().ForEach(t => DispatcherInfos.Add(new DispatcherModel(t.Name, t.Time)))).Invoke();
        }
        #endregion

        #region 私有方法
        [RelayCommand]
        private async Task AddProject()
        {
            var model = RandomModelAssembler.randomProjectModel();
            await ProjectService.InsertProject(model);
            Projects.Insert(0,model);
            ProjectCount = Projects.Count().ToString();
        }

        [RelayCommand]
        private async Task AddDispatcher()
        {
            var model = RandomModelAssembler.RandomDispatcher();
            await ProjectService.InsertTask(model);
            DispatcherInfos.Insert(0, new DispatcherModel($"将{model.TaskName}分配给{model.Name}生产",model.Time));
        }

        [RelayCommand]
        private void AddMonitor()
        {
            MonitorInfos.Insert(0, RandomModelAssembler.RandomMonitorModel());
        }
        #endregion
    }
}
