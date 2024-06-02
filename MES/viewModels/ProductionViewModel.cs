using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MES.DataTransaction.bean;
using MES.DataTransaction.database;
using MES.message;
using MES.Models.project;
using MES.Models.table;
using MES.views.Manage;
using Microsoft.Extensions.DependencyInjection;

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
            InitializeMessenger();
            new Action(async () =>
            {
                (await ProjectService.GetProjects()).ForEach(p => Projects.Add(p));
                ProjectCount = Projects.Count().ToString();
            }).Invoke();
            new Action(
                async () =>
                    (await ProjectService.GetTasks())
                        .ToList()
                        .ForEach(t => DispatcherInfos.Add(new DispatcherModel(t.Name, t.Time)))
            ).Invoke();
        }
        #endregion

        #region 私有方法
        [RelayCommand]
        private void AddProject()
        {
            var page = App.Current.Services.GetService<AddProductionPage>();
            page!.Show();
        }

        [RelayCommand]
        private async Task AddDispatcher()
        {
            var model = RandomModelAssembler.RandomDispatcher();
            await ProjectService.InsertTask(model);
            DispatcherInfos.Insert(
                0,
                new DispatcherModel($"将{model.TaskName}分配给{model.Name}生产", model.Time)
            );
        }

        [RelayCommand]
        private void AddMonitor()
        {
            MonitorInfos.Insert(0, RandomModelAssembler.RandomMonitorModel());
        }

        private void InitializeMessenger()
        {
            WeakReferenceMessenger.Default.Register<AddProjectMessage>(
                this,
                async (_, m) =>
                {
                    await ProjectService.InsertProject(m.Project!);
                    Projects.Insert(0, m.Project!);
                    ProjectCount = Projects.Count.ToString();
                }
            );
        }
        #endregion
    }
}
