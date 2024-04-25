using MaterialDesignThemes.Wpf;
using MES.model;
using MES.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MES.views
{
    /// <summary>
    /// DevicePage.xaml 的交互逻辑
    /// </summary>
    public partial class DevicePage : UserControl
    {
        public DevicePage()
        {
            InitializeComponent();
        }

        private void PackIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is PackIcon icon && icon.DataContext is DeviceDisplayModel m && DataContext is DevicePageViewModel vm)
            {
                if (m.Online)
                {
                    vm.PowerOffDeviceCommand.Execute(m.Id);
                }
                else
                vm.PowerOnDeviceCommand.Execute(m.Id);
            }
        }
    }
}
