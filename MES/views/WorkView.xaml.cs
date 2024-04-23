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
    /// WorkView.xaml 的交互逻辑
    /// </summary>
    public partial class WorkView : UserControl
    {
        public WorkView()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is WorkViewModel vm && sender is Grid g && g.DataContext is IconModel icon)
            {
                vm.SwitchPageCommand.Execute(icon.IconKind);
            }
        }
    }
}
