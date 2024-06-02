using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;
using MES.Models.table;

namespace MES.message
{
    internal class AddDispatcherMessage : ValueChangedMessage<TaskModel?>
    {
        private TaskModel? task;

        public TaskModel? Task
        {
            get { return task; }
            set { task = value; }
        }

        public AddDispatcherMessage(TaskModel task)
            : base(task)
        {
            this.Task = task;
        }
    }
}
