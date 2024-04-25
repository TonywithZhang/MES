using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MES.DataTransaction.database;
using MES.DataTransaction.socket;
using MES.message;
using MES.views;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace MES.viewModels
{
    internal partial class MainViewModel : ObservableObject
    {
        #region 属性
        [ObservableProperty]
        private object? page;
        #endregion

        #region 构造函数
        public MainViewModel()
        {
            this.Page = App.Current.Services.GetService<LoginView>();
            AcceptMessage();
            StartWebSocket();
        }
        #endregion

        #region 私有函数
        private void AcceptMessage()
        {
            WeakReferenceMessenger
                .Default
                .Register<LoginSuccessMessage>(this,
                    (_, _) => 
                    Page = App.Current.Services.GetService<WorkView>()
                );
        }

        private void StartWebSocket()
        {
            try
            {
                WebSocketService.ConfigureWebSocket();
            }
            catch (Exception)
            {
                Trace.WriteLine("websocket start failed");
            }
        }
        #endregion
    }
}
