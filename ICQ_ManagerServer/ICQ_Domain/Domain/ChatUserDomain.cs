using System;
using System.Collections.Generic;
using System.Linq;
using ICQ_AppDomain;
using ICQ_AppDomain.Entities;

namespace ICQ_ManagerServer.Domain
{
    public class ChatUserDomain : IChatUserDomain
    {

        private List<User> _userList = new List<User>();
        private IResponse _response;

        public ChatUserDomain(IResponse response)
        {
            _response = response;
        }

        public IResponse GetAllUser()
        {
            string listUsers = string.Empty;

            if (!_userList.Any())
                return _response.MountMessage("Not exist users register",isSuccesMessage:false);


            listUsers = string.Join(Environment.NewLine, _userList.Select(p => p.UserIdentifier));


            return _response.MountMessage("Users online:" + Environment.NewLine + listUsers);

        }


        public IResponse CreateUser(string user_identifier, object socket)
        {
            var user = _userList.FirstOrDefault(p => p.UserIdentifier == user_identifier);
            string message = string.Empty;
            if (user != null)
            {
                user.ConnectionSocket = socket;
                message = "User is in use";
                return _response.MountMessage(message: message, clientSocket: socket,false, isSuccesMessage: false);
            }
            else
            {
                _userList.Add(new User() { ConnectionSocket = socket, UserIdentifier = user_identifier });
                message = $"That is, user created \\ {user_identifier}";
                return _response.MountMessage(message: message, clientSocket: socket);
            }

         

        }


        public IUser GetUser(string user_identifier)
        {
            return _userList.Where(p => p.UserIdentifier == user_identifier).FirstOrDefault();
        }

        public IResponse DMUser(string user_identifier,string message,IUser userMessage)
        {
            var user = _userList.FirstOrDefault(p => p.UserIdentifier.ToUpper() == user_identifier.ToUpper());
            var directmessage = $"{userMessage.UserIdentifier} send you: {message}";

            return _response.MountMessage(message: directmessage, clientSocket: user.ConnectionSocket);
           
        }
    }
}
