using System;
using System.Collections.Generic;
using System.Linq;
using ICQ_AppDomain.Const;
using ICQ_AppDomain.Domain.Entities.Interface;
using ICQ_AppDomain.Entities;

namespace ICQ_AppDomain.Domain
{
    public class ChatGroupDomain : IChatGroupDomain
    {

        #region Private
        private IList<IGroupChat> _groupList = new List<IGroupChat>();
        private const string pipeSeparator = "\\";
       

        #endregion

        #region Construtor
        public ChatGroupDomain()
        {
         
        }
        #endregion


        #region Method
        public IResponse ProcessChatData(IDataReceiver messageIntput, IUser user)
        {
            var command = messageIntput.Message.Trim().Split(" ");
           
            switch (command[0].ToUpper().Trim())
            {


                case CommandConst.CREATEGROUP:
                    return this.CreateNewGroup(messageIntput, user);


                case CommandConst.CONNECTTOGROUP:
                    return this.AddUserInGroup(messageIntput, user);



                case CommandConst.GROUPCOMMAND:
                    return GroupCommand(messageIntput, user);


                case CommandConst.LISTALLGROUP:
                    return this.GetAllGroup(messageIntput);

                default:
                    return new Response(message: $"The command {command[0]} is invalid. User 'Helper' for show command", clientSocket: messageIntput.Socket);

            }



        }

        private IResponse GroupCommand(IDataReceiver messageIntput, IUser user)
        {
            try
            {
                var command = messageIntput.Message.Trim().Split(" ");
                switch (command[1].ToUpper().Trim())
                {
                    case CommandConst.LISTALLGROUP:
                        return this.GetAllGroup(messageIntput);

                    case CommandConst.EXITGROUP:
                        return this.LeaveGroup(messageIntput, user);
                    default:
                        return this.SendBroadcastGroupMessage(messageIntput);

                }
            }
            catch
            {
                throw;
            }
        }

        public IResponse AddUserInGroup(IDataReceiver messageIntput, IUser user)
        {
            var parameter = messageIntput.Message.Trim().Split(" ");
            if (parameter.Length <= 2)
            {
                return new Response($"You need register user for create a group, user the command {CommandConst.CREATEUSER} 'username'", isSuccesMessage: false,clientSocket: messageIntput.Socket);
            }

            var group_identifier = parameter[1];
            var user_identifier = parameter[3];

            var group = _groupList.FirstOrDefault(p => p.NameGroup == parameter[1]);
            if (group == null)
            {

                return new Response($"Group hasn't exist, if you want create a group, use the command: {CommandConst.CREATEGROUP} {group_identifier} {pipeSeparator} {group_identifier}", isSuccesMessage: false, clientSocket: messageIntput.Socket);
            }
            else
            {
                _groupList.Where(p => p.NameGroup == group_identifier).FirstOrDefault().UsersInGroup.Add(user);
                return new Response($"You have been added to the group {group_identifier}: Send you first message: {pipeSeparator} {user_identifier} {pipeSeparator} {group_identifier}", clientSocket: messageIntput.Socket);
            }

        }

        public IResponse CreateNewGroup(IDataReceiver messageIntput, IUser user)
        {
            var parameter = messageIntput.Message.Trim().Split(" ");
            if (parameter.Length <= 2)
            {
                return new Response($"You need register user for create a group, user the command {CommandConst.CREATEUSER} 'username'", isSuccesMessage: false, clientSocket: messageIntput.Socket);
            }

            var group_identifier = parameter[1];
            var user_identifier = parameter[3];

            var group = _groupList.FirstOrDefault(p => p.NameGroup == group_identifier);
            if (group != null)
            {

                return new Response($"Group has exist, if you want join, use the command: {CommandConst.CONNECTTOGROUP} {group_identifier} {pipeSeparator} {user_identifier}", isSuccesMessage: false, clientSocket: messageIntput.Socket);
            }


            _groupList.Add(new GroupChat(group_identifier, user));

            return new Response($"You created and were registed in {group_identifier}: Send you first message: {pipeSeparator} {user_identifier} {pipeSeparator}{group_identifier}", clientSocket: messageIntput.Socket);

        }

        public IResponse SendBroadcastGroupMessage(IDataReceiver messageIntput)
        {
            var parameter = messageIntput.Message.Trim().Split(" ");
            var group_identifier = parameter[parameter.Length - 1];
            var user_identifier = parameter[parameter.Length - 2];
            var userlist = _groupList.Where(p => p.NameGroup == parameter[parameter.Length - 1]).FirstOrDefault().UsersInGroup.Where(p => p.UserIdentifier != user_identifier).ToList();
            return new Response($"Message to the group {user_identifier}: {parameter[1].Replace("\\*/", " ")} {pipeSeparator} {user_identifier} {pipeSeparator} {group_identifier}", null, true, userlist);
        }

        public IResponse GetAllGroup(IDataReceiver messageIntput)
        {
            string listGroup = string.Empty;

            if (_groupList.Any())
                return new Response("Group not exits:", isSuccesMessage: false, clientSocket: messageIntput.Socket);

            foreach (var group in _groupList)
            {
                listGroup += group.NameGroup + Environment.NewLine;
            }

            return new Response("List of Group:" + Environment.NewLine + listGroup,clientSocket: messageIntput.Socket);

        }

        public IResponse LeaveGroup(IDataReceiver messageIntput, IUser user)
        {
            var parameter = messageIntput.Message.Trim().Split(" ");
            var group_identifier = parameter[parameter.Length - 1];
            var user_identifier = parameter[parameter.Length - 3];

            _groupList.Where(p => p.NameGroup == group_identifier).FirstOrDefault().UsersInGroup.Remove(user);

            var message = $"You left the group {group_identifier} {pipeSeparator} {user_identifier} {pipeSeparator}  ";
            return new Response(message,clientSocket: messageIntput.Socket);
        }

        #endregion


    }
}
