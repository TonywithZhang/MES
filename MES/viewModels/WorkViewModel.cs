using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.viewModels
{
    internal partial class WorkViewModel: ObservableObject
    {
        [ObservableProperty]
        private string title = "hello world";
    }
}
