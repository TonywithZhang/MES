using CommunityToolkit.Mvvm.ComponentModel;

namespace MES.viewModels
{
    partial class ProductionViewModel : ObservableObject
    {
        [ObservableProperty]
        private string title = "hello world";
    }
}
