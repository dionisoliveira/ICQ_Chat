using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ICQ_AppDomain.Entities
{
    public interface IUser
    {
        string UserIdentifier { get; set; }
        object ConnectionSocket { get; set; }
        IList<Group> GroupConnectionList { get; set; }
    }
}