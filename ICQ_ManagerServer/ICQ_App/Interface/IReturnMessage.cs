using System.Collections.Generic;
using ICQ_ManagerServer.Model;

namespace ICQ_ManagerServer
{
    public interface IResponse
    {
        string Message { get; }
        object ClientSocket { get; }
        bool IsBroadCast { get; }
        List<IUser> UsersBroadcastMessage { get; }
        bool IsSucessMessage { get; set; }
        IResponse MountMessage(string message = default(string), object clientSocket = default(object), bool isBroadCast = default(bool), List<IUser> users = null,bool isSuccesMessage = true);
    }
}