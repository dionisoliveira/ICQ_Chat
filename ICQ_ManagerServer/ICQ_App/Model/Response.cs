using System;
using System.Collections.Generic;

namespace ICQ_ManagerServer.Model
{
    public class Response : IResponse
    {
        public string Message { get; set; }
        public object ClientSocket { get; set; }
        public bool IsBroadCast { get; set; }
        public List<IUser> UsersBroadcastMessage { get; set; }
        public bool IsSucessMessage { get; set; }



        public Response(string message = default(string), object clientSocket = default(object), bool isBroadCast = default(bool), List<IUser> users = null)
        {

            Message = message;
            ClientSocket = clientSocket;
            IsBroadCast = isBroadCast;
            UsersBroadcastMessage = users;
        }

        public IResponse MountMessage(string message = default(string), object clientSocket = default(object), bool isBroadCast = default(bool), List<IUser> users = null, bool isSuccesMessage = true)
        {
            Message = message;
            ClientSocket = clientSocket;
            IsBroadCast = isBroadCast;
            UsersBroadcastMessage = users;
            IsSucessMessage = isSuccesMessage;
            return this;

        }
    }
}
