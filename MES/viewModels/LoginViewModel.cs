using CommunityToolkit.Mvvm.ComponentModel;

namespace MES.viewModels
{
    internal partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string title = "hello world";
    }
}
