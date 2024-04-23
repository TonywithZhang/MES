using CommunityToolkit.Mvvm.ComponentModel;

namespace MES.model
{
    internal partial class IconModel : ObservableObject
    {
        [ObservableProperty]
        private string iconKind = String.Empty;
        [ObservableProperty]
        private bool isActive = false;
        [ObservableProperty]
        private string desc = string.Empty;
    }
}
