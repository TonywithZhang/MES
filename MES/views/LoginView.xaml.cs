using MES.viewModels;
using System.Diagnostics;
using System.Windows.Controls;

namespace MES.views
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        #region 事件处理
        private void Storyboard_Completed(object sender, EventArgs e)
        {
            if (this.DataContext is LoginViewModel vm)
            {
                vm.TurnToMainCommand.Execute(vm);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckLoginEnable();
        }
        #endregion

        private void PasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender is PasswordBox box)
            {
                CheckLoginEnable(box.Password);
            }
        }

        private void CheckLoginEnable(string password = "")
        {
            if (this.DataContext is LoginViewModel vm)
            {
                vm.CheckInputCommand.Execute(password);
            }
        }
    }
}
