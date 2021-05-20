using System;
using System.Collections.Generic;
using ICQ_ManagerServer.Interface;
using ICQ_ManagerServer.Model;
using System.Linq;

namespace ICQ_ManagerServer.Domain
{
    public class ChatManager : IChatManager
    {
        private List<User> _userList = new List<User>();

        public ReturnMessage ListGroupInServer()
        {
            throw new NotImplementedException();
        }

        public ReturnMessage ProcessMessage(string message, object connectionSocket)
        {
            try
            {
                var command = message.Split("\\");

                switch (Convert.ToInt16(command[0]))
                {
                    case 0:
                        return UserCreate(command[1],connectionSocket);
                       

                    case 2:
                        return UserCreateGroupConection(command[1]);
                      
                    case 9001:
                        return ConnectionStabilished(connectionSocket);
                       
                    default:
                        return default(ReturnMessage);
                      
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Attention error when execute the command {message}");
                return new ReturnMessage() { Message = "Ocorreu um erro ao executar a opção"};
            }
        }
        public List<User> GetUsers()
        {
            return _userList;
        }

        public ReturnMessage UserConnectToGroup(string group_identifier)
        {
            throw new NotImplementedException();
        }

        public ReturnMessage ConnectionStabilished(object connectionSocket)
        {
            return new ReturnMessage() {ClientSocket = connectionSocket,  Message = $"Online  {Environment.NewLine} Digite seu nome de usuario \\ 0" };
        }

        public ReturnMessage UserCreate(string user_identifier, object connetionSocker)
        {
            var user = _userList.FirstOrDefault(p => p.UserIdentifier == user_identifier);
            if (user != null)
            {
                user.ConnectionSocket = connetionSocker;
                return new ReturnMessage() { ClientSocket = connetionSocker, Message = "User is in use" };
            }
            else
            {
                _userList.Add(new User() { ConnectionSocket = connetionSocker, UserIdentifier = user_identifier });
                return new ReturnMessage() { ClientSocket = connetionSocker, Message = "That is, user created" };
            }
           
        }

        public ReturnMessage UserCreateGroupConection(string group_identifier)
        {
            throw new NotImplementedException();
        }

        public ReturnMessage UserSendGroupMessage(string grou_identifier, string message)
        {
            throw new NotImplementedException();
        }

       
    }
}
