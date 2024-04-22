using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.message
{
    internal class LoginSuccessMessage : ValueChangedMessage<string>
    {
        #region 属性
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        #endregion
        #region 构造函数
        public LoginSuccessMessage(string message) : base(message)
        {
            this.message = message;
        }
        #endregion

    }
}
