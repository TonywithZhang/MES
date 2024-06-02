using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;
using MES.Models.project;

namespace MES.message
{
    internal class AddMonitorMessage : ValueChangedMessage<MonitorModel>
    {
        private MonitorModel monitor;

        public MonitorModel Monitor
        {
            get { return monitor; }
            set { monitor = value; }
        }

        public AddMonitorMessage(MonitorModel monitor)
            : base(monitor)
        {
            this.monitor = monitor;
        }
    }
}
