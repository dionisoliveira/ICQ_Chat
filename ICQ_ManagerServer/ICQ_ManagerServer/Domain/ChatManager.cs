using System;
using System.Collections.Generic;
using ICQ_ManagerServer.Interface;
using ICQ_ManagerServer.Model;
using System.Linq;

namespace ICQ_ManagerServer.Domain
{
    public class ChatManager : IChatManager
    {
        private List<User> _userList;

        public void ListGroupInServer()
        {
            throw new NotImplementedException();
        }

        public void ProcessMessage(string message, object connectionSocket)
        {
            try
            {
                var command = message.Split("$$$");

                switch (int.Parse(command[0]))
                {
                    case 0:
                        UserCreate(command[1],connectionSocket);
                        break;

                    case 2:
                        UserCreateGroupConection(command[1]);
                        break;
                    default:

                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Attention error when execute the command {message}");
            }
        }

        public bool UserConnectToGroup(string group_identifier)
        {
            throw new NotImplementedException();
        }

        public string UserCreate(string user_identifier, object connetionSocker)
        {
            if (_userList.FirstOrDefault(p => p.UserIdentifier == user_identifier) == null)
            {
                return "Sorry , this user is alread";
            }
            _userList.Add(new User() { ConnectionSocket = connetionSocker, UserIdentifier = user_identifier });
            return "This is't, user created";
        }

        public bool UserCreateGroupConection(string group_identifier)
        {
            throw new NotImplementedException();
        }

        public bool UserSendGroupMessage(string grou_identifier, string message)
        {
            throw new NotImplementedException();
        }
    }
}
