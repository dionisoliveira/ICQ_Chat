using System;
using System.Collections.Generic;
using ICQ_ManagerServer.Interface;
using ICQ_ManagerServer.Model;
using System.Linq;
using ICQ_ManagerServer.Const;

namespace ICQ_ManagerServer.Domain
{
    public class ChatManager : IChatManager
    {
        private List<User> _userList = new List<User>();
        private List<GroupChat> _groupList = new List<GroupChat>();



        public ReturnMessage ProcessMessage(string message, object connectionSocket)
        {
            try
            {
                var command = message.Trim().Split(" ");

                switch (command[0].ToUpper().Trim())
                {
                    case CommandConst.CREATEUSER:
                        return UserCreate(command[1], connectionSocket);


                    case CommandConst.CREATEGROUP:
                        return CreateNewGroup(command);
                    case CommandConst.CONNECTTOGROUP:
                        return AddUserInGroup(command);


                    case CommandConst.CONNECTIONSTABILISHE:
                        return ConnectionStabilished();

                    case CommandConst.MESSAGEGROUP:
                        return SendBroadcastGroupMessage(command);

                    case CommandConst.LISTALL:
                        return GetUserInServer();

                    default:
                        return new ReturnMessage() { Message = $"The command {command[0]} is invalid. User 'Helper' for show command" };

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Attention error when execute the command {message}");
                return new ReturnMessage() { Message = "Ocorreu um erro ao executar a opção" };
            }
        }

        private ReturnMessage GroupCommand(string[] parameter)
        {

            switch (parameter[1].ToUpper().Trim())
            {



                case CommandConst.LISTALL:
                    return GetGroupInServer();

                case CommandConst.CONNECTIONSTABILISHE:
                    return ConnectionStabilished();






                default:
                    return SendBroadcastGroupMessage(parameter);

            }
        }

        /// <summary>
        /// Return all users registred in server chat
        /// </summary>
        /// <returns></returns>
        public ReturnMessage GetUserInServer()
        {
            string listUsers = string.Empty;

            foreach (var user in _userList)
            {
                listUsers += user.UserIdentifier + Environment.NewLine;
            }

            if (_userList.Any())
                return new ReturnMessage() { Message = "Users online:" + Environment.NewLine + listUsers };



            return new ReturnMessage() { Message = "Not exist users register" };
        }


        public ReturnMessage GetGroupInServer()
        {
            string listGroup = string.Empty;

            foreach (var group in _groupList)
            {
                listGroup += group.NameGroup + Environment.NewLine;
            }

            if (_userList.Any())
                return new ReturnMessage() { Message = "List of Group:" + Environment.NewLine + listGroup };



            return new ReturnMessage() { Message = "Not exist the group" };
        }


        public ReturnMessage UserConnectToGroup(string group_identifier)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Show connection stabilished 
        /// </summary>
        /// <param name="connectionSocket"></param>
        /// <returns></returns>
        public ReturnMessage ConnectionStabilished()
        {
            return new ReturnMessage() { Message = $"Online" };
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
                return new ReturnMessage() { ClientSocket = connetionSocker, Message = $"That is, user created \\ {user_identifier}" };
            }

        }

        public ReturnMessage AddUserInGroup(string[] parameter)
        {

            if (parameter.Length <= 2)
            {
                return new ReturnMessage() { Message = $"You need register user for create a group, user the command {CommandConst.CREATEUSER} 'nameuser'" };
            }

            var group_identifier = parameter[1];
            var user_identifier = parameter[3];

            var group = _groupList.FirstOrDefault(p => p.NameGroup == parameter[1]);
            if (group == null)
            {

                return new ReturnMessage() { Message = $"Group not exist exist, if you want create, use the command: {CommandConst.CREATEGROUP} {group_identifier} \\ {group_identifier} " };
            }
            else
            {
                _groupList.Where(p => p.NameGroup == group_identifier).FirstOrDefault().UsersInGroup.Add(_userList.Where(p => p.UserIdentifier == user_identifier).FirstOrDefault());
                return new ReturnMessage() { Message = $"You are register in Group {group_identifier}: Send you first message: \\ {user_identifier} \\ {group_identifier}" };
            }
        }



        public ReturnMessage CreateNewGroup(string[] parameter)
        {

            if (parameter.Length <= 2)
            {
                return new ReturnMessage() { Message = $"You need register user for create a group, user the command {CommandConst.CREATEUSER} 'nameuser'" };
            }

            var group_identifier = parameter[1];
            var user_identifier = parameter[3];
            var group = _groupList.FirstOrDefault(p => p.NameGroup == group_identifier);
            if (group != null)
            {

                return new ReturnMessage() { Message = $"Group has exist, if you wan't join, use the command: JG {group_identifier} \\ {user_identifier} " };
            }
            else
            {
                _groupList.Add(new GroupChat(group_identifier, _userList.Where(p => p.UserIdentifier == user_identifier).FirstOrDefault()));
                return new ReturnMessage() { Message = $"You are register in Group {group_identifier}: Send you first message: \\ {user_identifier} \\ {group_identifier}" };
            }
        }

        public ReturnMessage SendBroadcastGroupMessage(string[] parameter)
        {
            var group_identifier = parameter[parameter.Length - 1];
            var user_identifier = parameter[parameter.Length - 1];
            return new ReturnMessage() { Message = $"You receive message {user_identifier}: {parameter[1]} \\ {user_identifier} \\ {group_identifier}", IsBroadCast = true, UsersBroadcastMessage = _groupList.Where(p => p.NameGroup == parameter[parameter.Length - 1]).FirstOrDefault().UsersInGroup };
        }


    }
}
