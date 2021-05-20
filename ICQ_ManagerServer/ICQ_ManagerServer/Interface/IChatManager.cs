using System;
namespace ICQ_ManagerServer.Interface
{
    public interface IChatManager
    {
        string UserCreate(string user_identifier, object connectionSocket);

        void ListGroupInServer(); // Command ls



        bool UserCreateGroupConection(string group_identifier);

        bool UserConnectToGroup(string group_identifier);

        bool UserSendGroupMessage(string grou_identifier, string message);

        void ProcessMessage(string message, object connectionSocket);


    }
}
