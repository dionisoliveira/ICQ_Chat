using System;
using System.Collections.Generic;

namespace ICQ_AppDomain.Entities
{
    public class GroupChat : IGroupChat
    {
        public Guid Id { get; set; }
        public string NameGroup { get; set; }
        public IList<IUser> UsersInGroup { get; set; } = new List<IUser>();


        public GroupChat(string nameGroup, IUser user)
        {
            NameGroup = nameGroup;
            UsersInGroup.Add(user);
        }

        public void AddUserInGroup(IUser user)
        {
            UsersInGroup.Add(user);
        }

    }
}
