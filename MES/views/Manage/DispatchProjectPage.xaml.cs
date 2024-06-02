using System.Windows;
using MES.viewModels.Manage;

namespace MES.views.Manage
{
    /// <summary>
    /// DispatchProjectPage.xaml 的交互逻辑
    /// </summary>
    public partial class DispatchProjectPage : Window
    {
        public DispatchProjectPage()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as DispatchProjectViewModel;
            vm!.SendDispatcherCommand.Execute(null);
            Close();
        }
    }
}
