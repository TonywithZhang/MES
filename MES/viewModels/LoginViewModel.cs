using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MES.DataTransaction.database;
using MES.message;
using MES.Models.table;
using System.Diagnostics;

namespace MES.viewModels
{
    internal partial class LoginViewModel : ObservableObject
    {
        #region 属性
        [ObservableProperty]
        private bool isLoginSuccess = false;
        [ObservableProperty]
        private string userName = string.Empty;
        [ObservableProperty]
        private string password = string.Empty;
        
        [ObservableProperty]
        private bool showIndicator = false;
        [ObservableProperty]
        private string buttonContent = "点击登录";
        [ObservableProperty]
        private bool buttonEnable = false;
        #endregion

        #region 命令
        [RelayCommand]
        private void TurnToMain()
        {
            WeakReferenceMessenger
                .Default
                .Send<LoginSuccessMessage>(new LoginSuccessMessage(string.Empty));
        }
        [RelayCommand]
        private void CheckInput(string pass)
        {
            ButtonEnable = !(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(pass));
        }

        [RelayCommand]
        private async Task Login()
        {
            ShowIndicator = true;
            await Task.Delay(1000);
            if (await UserService.GetUserByName(UserName) is UserModel model && model.Password == Password)
            {
                IsLoginSuccess = true;
            }
            IsLoginSuccess = true;
            ShowIndicator = false;
        }
        #endregion
    }
}
