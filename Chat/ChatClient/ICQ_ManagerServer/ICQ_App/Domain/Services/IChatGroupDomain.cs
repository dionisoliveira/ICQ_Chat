

using ICQ_AppDomain;
using ICQ_AppDomain.Entities;

namespace ICQ_AppDomain.Domain
{
    public interface IChatGroupDomain
    {
        IResponse AddUserInGroup(string[] parameter, IUser user);
        IResponse CreateNewGroup(string[] parameter, IUser user);
        IResponse GetAllGroup();
        IResponse SendBroadcastGroupMessage(string[] parameter);
        IResponse LeaveGroup(string[] parameter, IUser user);
    }
}