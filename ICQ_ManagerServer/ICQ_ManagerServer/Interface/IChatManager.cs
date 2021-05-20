using System;
using System.Collections.Generic;
using ICQ_ManagerServer.Model;

namespace ICQ_ManagerServer.Interface
{
    public interface IChatManager
    {
        ReturnMessage UserCreate(string user_identifier, object connectionSocket);

        ReturnMessage ListGroupInServer(); // Command ls


        ReturnMessage ConnectionStabilished(object connectionSocket);
        ReturnMessage UserCreateGroupConection(string group_identifier);

        ReturnMessage UserConnectToGroup(string group_identifier);

        ReturnMessage UserSendGroupMessage(string grou_identifier, string message);

        ReturnMessage ProcessMessage(string message, object connectionSocket);

        List<User> GetUsers();
    }
}
