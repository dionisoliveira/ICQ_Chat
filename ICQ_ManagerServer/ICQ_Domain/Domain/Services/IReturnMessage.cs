using System.Collections.Generic;
using ICQ_AppDomain.Entities;

namespace ICQ_AppDomain
{
    public interface IResponse
    {
        string Message { get; }
        object ClientSocket { get; }
        bool IsBroadCast { get; }
        IList<IUser> UsersBroadcastMessage { get; }
        bool IsSucessMessage { get; set; }
        IResponse MountMessage(string message = default(string), object clientSocket = default(object), bool isBroadCast = default(bool), IList<IUser> users = null,bool isSuccesMessage = true);
    }
}