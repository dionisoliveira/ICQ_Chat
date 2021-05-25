using System;
using System.Collections.Generic;
using System.Linq;
using ICQ_AppDomain;
using ICQ_AppDomain.Const;
using ICQ_AppDomain.Domain.Entities.Interface;
using ICQ_AppDomain.Entities;

namespace ICQ_ManagerServer.Domain
{
    public class ChatUserDomain : IChatUserDomain
    {

        #region PRIVATE
        private List<User> _userList = new List<User>();
        #endregion

        #region CONSTRUTOR
        public ChatUserDomain()
        {

        }
        #endregion

        #region METHOD

        public IResponse ProcessUserData(IDataReceiver dataInput)
        {
            var command = dataInput.Message.Trim().Split(" ");

            switch (command[0].ToUpper().Trim())
            {
                case CommandConst.CREATEUSER:
                    return this.CreateUser(dataInput);


                case CommandConst.LISTALL:
                    return this.GetAllUser(dataInput);


                case CommandConst.DM:
                    return this.DMUser(dataInput);


                default:
                    return new Response(message: $"The command {command[0]} is invalid. User 'Helper' for show command", dataInput.Socket);

            }



        }


        public IResponse GetAllUser(IDataReceiver dataInput)
        {
            string listUsers = string.Empty;

            if (!_userList.Any())
                return new Response("Not exist users register", isSuccesMessage: false, clientSocket: dataInput.Socket);


            listUsers = string.Join(Environment.NewLine, _userList.Select(p => p.UserIdentifier));


            return new Response("Users online:" + Environment.NewLine + listUsers, clientSocket: dataInput.Socket);

        }


        public IResponse CreateUser(IDataReceiver dataInput)
        {
            var command = dataInput.Message.Trim().Split(" ");
            var user_identifier = command[1];
            var user = _userList.FirstOrDefault(p => p.UserIdentifier == user_identifier);
            string message = string.Empty;
            if (user != null)
            {
                user.ConnectionSocket = dataInput.Socket;
                message = "User is in use";
                return new Response(message: message, clientSocket: dataInput.Socket, false, isSuccesMessage: false);
            }
            else
            {
                _userList.Add(new User() { ConnectionSocket = dataInput.Socket, UserIdentifier = user_identifier });
                message = $"That is, user created \\ {user_identifier}";
                return new Response(message: message, clientSocket: dataInput.Socket);
            }
        }


        public IUser GetUser(IDataReceiver dataInput)
        {
            var command = dataInput.Message.Trim().Split(" ");
            return _userList.Where(p => p.UserIdentifier == command[command.Length - 1]).FirstOrDefault();
        }



        public IResponse DMUser(IDataReceiver dataInput)
        {
            var command = dataInput.Message.Trim().Split(" ");
            var message = command[3].ToUpper();
            var user = this.GetUser(dataInput);

            var directmessage = $"{user.UserIdentifier} send you: {message}";

            return new Response(message: directmessage, clientSocket: user.ConnectionSocket);

        }

        #endregion


    }
}
