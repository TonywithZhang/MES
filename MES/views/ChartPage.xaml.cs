using MaterialDesignThemes.Wpf;
using MES.model;
using MES.Models.table;
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
    /// ChartPage.xaml 的交互逻辑
    /// </summary>
    public partial class ChartPage : UserControl
    {
        public ChartPage()
        {
            InitializeComponent();
        }

        private void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Card c && c.DataContext is ChartModel m && DataContext is ChartPageViewModel vm)
            {
                vm.ExportTableCommand.Execute(m.Content);
            }
        }
    }
}
