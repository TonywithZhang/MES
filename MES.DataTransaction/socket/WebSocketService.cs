using System.Diagnostics;
using Fleck;

namespace MES.DataTransaction.socket
{
    public class WebSocketService
    {
        #region 静态字段
        private static readonly Dictionary<string, IWebSocketConnection> sockets = [];
        #endregion
        #region 静态方法
        public static void ConfigureWebSocket()
        {
            var server = new WebSocketServer("ws://0.0.0.0:8081");
            server.Start(socket =>
            {
                sockets.Add(
                    socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort,
                    socket
                );
                socket.OnOpen = () => Trace.WriteLine("connection established");
                socket.OnClose = () => Trace.WriteLine("connection interupted");
                socket.OnMessage = (message) =>
                {
                    Trace.WriteLine(message.ToString());
                };
            });
        }

        public static void PowerOffDevice(string deviceName)
        {
            var socket = sockets[deviceName];
            socket?.Send(new byte[] { 0x00 });
        }

        public static void PowerOnDevice(string deviceName)
        {
            var socket = sockets[deviceName];
            socket?.Send(new byte[] { 0x01 });
        }

        public static void QualityManagement()
        {
            var socket = sockets.Last();
            socket.Value.Send(new byte[] { 0x00 });
        }

        public static void ProgressMonitor()
        {
            var socket = sockets.Last();
            socket.Value.Send(new byte[] { 0x00 });
        }
        #endregion
    }
}
