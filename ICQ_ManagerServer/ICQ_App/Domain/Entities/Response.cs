using System;
using System.Collections.Generic;

namespace ICQ_AppDomain.Entities
{
    public class Response : IResponse
    {
        public string Message { get; set; }
        public object Socket { get; set; }
        public bool IsBroadCast { get; set; }
        public IList<IUser> UsersBroadcastMessage { get; set; }
        public bool IsSucessMessage { get; set; }



       

        public Response(string message = default(string), object clientSocket = default(object), bool isBroadCast = default(bool), IList<IUser> users = null, bool isSuccesMessage = true)
        {
            Message = message;
            Socket = clientSocket;
            IsBroadCast = isBroadCast;
            UsersBroadcastMessage = users;
            IsSucessMessage = isSuccesMessage;
           

        }
    }
}
