using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace LoginTest.WeChatQRLogin
{
    public class WeChatHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}