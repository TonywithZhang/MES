using System.Windows;
using MES.viewModels.Manage;

namespace MES.views.Manage
{
    /// <summary>
    /// AddProductionPage.xaml 的交互逻辑
    /// </summary>
    public partial class AddProductionPage : Window
    {
        public AddProductionPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as AddProjectViewModel;
            vm!.SendProjectCommand.Execute(null);
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
