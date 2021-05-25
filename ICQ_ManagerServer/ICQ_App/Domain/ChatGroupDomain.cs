using System;
using System.Collections.Generic;
using System.Linq;
using ICQ_AppDomain.Const;
using ICQ_AppDomain.Entities;

namespace ICQ_AppDomain.Domain
{
    public class ChatGroupDomain : IChatGroupDomain
    {
        private IList<GroupChat> _groupList = new List<GroupChat>();
        private const string pipeSeparator = "\\";
        private IResponse _response;

        public ChatGroupDomain(IResponse response)
        {
            _response = response;
        }


        public IResponse AddUserInGroup(string[] parameter, IUser user)
        {

            if (parameter.Length <= 2)
            {
                return _response.MountMessage($"You need register user for create a group, user the command {CommandConst.CREATEUSER} 'username'", isSuccesMessage: false);
            }

            var group_identifier = parameter[1];
            var user_identifier = parameter[3];

            var group = _groupList.FirstOrDefault(p => p.NameGroup == parameter[1]);
            if (group == null)
            {

                return _response.MountMessage($"Group hasn't exist, if you want create a group, use the command: {CommandConst.CREATEGROUP} {group_identifier} {pipeSeparator} {group_identifier}", isSuccesMessage: false);
            }
            else
            {
                _groupList.Where(p => p.NameGroup == group_identifier).FirstOrDefault().UsersInGroup.Add(user);
                return _response.MountMessage($"You have been added to the group {group_identifier}: Send you first message: {pipeSeparator} {user_identifier} {pipeSeparator} {group_identifier}");
            }

        }

        public IResponse CreateNewGroup(string[] parameter, IUser user)
        {

            if (parameter.Length <= 2)
            {
                return _response.MountMessage($"You need register user for create a group, user the command {CommandConst.CREATEUSER} 'username'", isSuccesMessage: false);
            }

            var group_identifier = parameter[1];
            var user_identifier = parameter[3];

            var group = _groupList.FirstOrDefault(p => p.NameGroup == group_identifier);
            if (group != null)
            {

                return _response.MountMessage($"Group has exist, if you want join, use the command: {CommandConst.CONNECTTOGROUP} {group_identifier} {pipeSeparator} {user_identifier}", isSuccesMessage: false);
            }


            _groupList.Add(new GroupChat(group_identifier, user));

            return _response.MountMessage($"You created and were registed in {group_identifier}: Send you first message: {pipeSeparator} {user_identifier} {pipeSeparator}{group_identifier}");

        }

        public IResponse SendBroadcastGroupMessage(string[] parameter)
        {

            var group_identifier = parameter[parameter.Length - 1];
            var user_identifier = parameter[parameter.Length - 2];
            var userlist = _groupList.Where(p => p.NameGroup == parameter[parameter.Length - 1]).FirstOrDefault().UsersInGroup.Where(p => p.UserIdentifier != user_identifier).ToList();
            return _response.MountMessage($"Message to the group {user_identifier}: {parameter[1].Replace("\\*/", " ")} {pipeSeparator} {user_identifier} {pipeSeparator} {group_identifier}", null, true, userlist);



        }



        public IResponse GetAllGroup()
        {
            string listGroup = string.Empty;

            if (_groupList.Any())
                return _response.MountMessage("Group not exits:", isSuccesMessage: false);

            foreach (var group in _groupList)
            {
                listGroup += group.NameGroup + Environment.NewLine;
            }

            return _response.MountMessage("List of Group:" + Environment.NewLine + listGroup);

        }


        public IResponse LeaveGroup(string[] parameter, IUser user)
        {
            var group_identifier = parameter[parameter.Length - 1];
            var user_identifier = parameter[parameter.Length - 3];
            _groupList.Where(p => p.NameGroup == group_identifier).FirstOrDefault().UsersInGroup.Remove(user);
            var message = $"You left the group {group_identifier} {pipeSeparator} {user_identifier} {pipeSeparator}  ";
            return _response.MountMessage(message);
        }


    }
}
