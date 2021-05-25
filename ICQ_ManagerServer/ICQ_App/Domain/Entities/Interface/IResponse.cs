using System.Collections.Generic;
using ICQ_AppDomain.Entities;

namespace ICQ_AppDomain
{
    public interface IResponse
    {
        string Message { get; }
        object Socket { get;  }
        bool IsBroadCast { get; }
        IList<IUser> UsersBroadcastMessage { get; }
        bool IsSucessMessage { get; set; }
        
    }
}