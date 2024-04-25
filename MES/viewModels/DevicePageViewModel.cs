using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MES.DataTransaction.database;
using MES.model;
using MES.Models.table;
using System.Windows;

namespace MES.viewModels
{
    internal partial class DevicePageViewModel : ObservableObject
    {
        #region 字段
        private readonly App app = App.Current;
        private readonly Dictionary<string, int> records = [];
        #endregion

        #region 属性
        [ObservableProperty]
        private IEnumerable<DeviceDisplayModel> devices = [];
        [ObservableProperty]
        private IEnumerable<Point> onlineDevices = [];
        [ObservableProperty]
        private IEnumerable<string> times = [];
        [ObservableProperty]
        private string onlineCount = "0";
        #endregion

        #region 构造函数
        public DevicePageViewModel()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    await app.Dispatcher.BeginInvoke(async () =>
                    {
                        await RefreshDevices();
                    });
                    await Task.Delay(60 * 1000);
                }
            });
        }
        #endregion

        #region 命令
        [RelayCommand]
        private async Task PowerOnDevice(string id)
        {
            await DeviceService.DeviceOnline(id);
            await RefreshDevices();
        }
        [RelayCommand]
        private async Task PowerOffDevice(string id)
        {
            await DeviceService.DeviceOff(id);
            await RefreshDevices();
        }
        #endregion

        #region 私有函数
        private async Task RefreshDevices()
        {
            var list = await DeviceService.GetDevices();
            Devices = list.Select(d => new DeviceDisplayModel
            {
                Name = d.Name,
                Id = d.Id,
                Duration = GetDuration(d),
                Online = d.Online,
                FirstTime = d.FirstTime,
                OnlineTime = d.OnLineTime
            });
            var count = Devices.Where(d => d.Online).Count();
            OnlineCount = count.ToString();
            if(records.Count < 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    records.Add(DateTime.Now.AddMinutes(-i).ToShortTimeString(), count);
                }
                var array = Enumerable.Range(0, 10);
                Times = array.Reverse().Select(s => DateTime.Now.AddMinutes(-s).ToShortTimeString()).ToArray();
                OnlineDevices = array.Select(d => new Point { X = d, Y = count }).ToArray();
            }
            else
            {
                RefreshRecords(count);
                var points = records.ToList();
                points.Sort((a, b) => a.Key.CompareTo(b.Key));
                Times = points.Select(s => s.Key);
                var counts = new List<Point>();
                for (int i = 0; i < points.Count; i++)
                {
                    var point = points[i];
                    counts.Add(new Point { X = i, Y = point.Value });
                }
                OnlineDevices = counts;
            }
        }

        private void RefreshRecords(int count)
        {
            var now = DateTime.Now;
            records[now.ToShortTimeString()] = count;
            if (records.Count > 200)
            {
                records.Remove(now.AddMinutes(-200).ToShortTimeString());
            }
        }

        private string GetDuration(DeviceModel device)
        {
            var online = (int)((DateTime.Now - device.OnLineTime).TotalMinutes);
            var offline = (int)((DateTime.Now - device.UpdateTime).TotalMinutes);
            var duration = device.Online ? online : offline;
            var state = device.Online ? "在线" : "离线";
            return $"已{state}{duration / (24 * 60)}天{(duration % (24 * 60)) / 60}小时{duration % 60}分钟";
        }
        #endregion
    }
}
