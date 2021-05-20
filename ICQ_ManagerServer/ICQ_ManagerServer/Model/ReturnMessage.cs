using System;
using System.Collections.Generic;

namespace ICQ_ManagerServer.Model
{
    public class ReturnMessage
    {
        public object ClientSocket { get; set; }
        public string Message { get; set; }
        public bool IsBroadCast { get; set; }
        public List<User> UsersBroadcastMessage { get; set; }
    }
}
