using CommunityToolkit.Mvvm.ComponentModel;
using MES.DataTransaction.database;
using System.Windows;

namespace MES.viewModels
{
    internal partial class QualityPageViewModel : ObservableObject
    {
        #region 私有字段
        private readonly Application app = App.Current;
        private const int COUNT = 2_00;
        private readonly Random random = new();
        #endregion

        #region 属性
        [ObservableProperty]
        private IList<string> times = [];
        [ObservableProperty]
        public IList<Point> parts = [];
        [ObservableProperty]
        public IList<Point> lines = [];
        [ObservableProperty]
        public IList<Point> solders = [];
        #endregion

        #region 构造函数时
        public QualityPageViewModel()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    await InsertQuality();
                    await app.Dispatcher.BeginInvoke(() =>
                    {
                        ClearLists();
                        GenerateChartData();
                    });
                    await Task.Delay(2000);
                }
            });
        }
        #endregion

        #region 私有函数
        private void ClearLists()
        {
            Times.Clear();
            Parts.Clear();
            Lines.Clear();
            Solders.Clear();
        }
        private void GenerateChartData()
        {
            var now = DateTime.Now;

            var count = Enumerable.Range(0, COUNT).Reverse();

            Times = count.Select(c => now.AddMinutes(-c).ToShortTimeString()).ToList();
            Parts = count.Select(c => new Point(COUNT - c, (int)(random.NextDouble() * 20) + 50)).ToList();
            Lines = count.Select(c => new Point(COUNT - c, (int)(random.NextDouble() * 10) + 80)).ToList();
            Solders = count.Select(c => new Point(COUNT - c, (int)(random.NextDouble() * 30) + 10)).ToList();
        }

        private async Task InsertQuality()
        {
            await QualityService.InsertQualityModel(
                new Models.table.QualityModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    BadReason = "公差太大",
                    Name = "车门饰条",
                    Batch = "SF2491930103",
                    ProductNo = "ASD123123",
                    UpdateTime = DateTime.Now
                });
        }

        #endregion
    }
}
