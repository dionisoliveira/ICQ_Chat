using System;
using System.Collections.Generic;
using ICQ_ManagerServer.Model;

namespace ICQ_ManagerServer.Interface
{
    public interface IChatManager
    {
        IResponse ConnectionStabilished();
        IResponse ProcessMessage(string message, object connectionSocket);
    }
}
