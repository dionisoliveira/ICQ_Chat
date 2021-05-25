using System;
using System.Collections.Generic;
using ICQ_AppDomain;

namespace ICQ_ManagerServer.Interface
{
    public interface IChatManagerService
    {
        IResponse ConnectionStabilished();
        IResponse ProcessMessage(string message, object connectionSocket);
      
    }
}
