using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ICQ_AppDomain.Entities
{
    public class User : IUser
    {
        public string UserIdentifier { get; set; }

        public object ConnectionSocket { get; set; }

        //This list is return when user stabilish connection with server.
        public IList<Group> GroupConnectionList { get; set; }
    }
}
