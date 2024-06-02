using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using MES.message;
using MES.viewModels.Manage;

namespace MES.views.Manage
{
    /// <summary>
    /// AddMonitorPage.xaml 的交互逻辑
    /// </summary>
    public partial class AddMonitorPage : Window
    {
        public AddMonitorPage()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as AddMonitorViewModel;
            vm!.SendMonitorCommand.Execute(vm);
            Close();
        }
    }
}
