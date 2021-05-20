using System;
using System.Collections.Generic;

namespace ICQ_ManagerServer.Model
{
    public class GroupChat
    {
        public Guid Id { get; set; }
        public string NameGroup { get; set; }
        public List<User> UsersInGroup { get; set; } = new List<User>();


        public GroupChat(string nameGroup, User user)
        {
            NameGroup = nameGroup;
            UsersInGroup.Add(user);
        }

        public void AddUserInGroup(User user)
        {
            UsersInGroup.Add(user);
        }

    }
}
