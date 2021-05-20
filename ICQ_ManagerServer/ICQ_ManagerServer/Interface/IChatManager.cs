using System;
using System.Collections.Generic;
using ICQ_ManagerServer.Model;

namespace ICQ_ManagerServer.Interface
{
    public interface IChatManager
    {
        ReturnMessage UserCreate(string user_identifier, object connectionSocket);

        ReturnMessage GetGroupInServer();




        ReturnMessage ConnectionStabilished();

        ReturnMessage CreateNewGroup(string[] parameter);

        ReturnMessage UserConnectToGroup(string group_identifier);

        ReturnMessage SendBroadcastGroupMessage(string[] parameter);

        ReturnMessage ProcessMessage(string message, object connectionSocket);

        ReturnMessage GetUserInServer();
    }
}
