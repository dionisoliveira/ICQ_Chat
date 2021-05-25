using System;
using System.Collections.Generic;

namespace ICQ_AppDomain.Entities
{
    public interface IGroupChat
    {
        Guid Id { get; set; }
        string NameGroup { get; set; }
        IList<IUser> UsersInGroup { get; set; }

        void AddUserInGroup(IUser user);
    }
}