using ICQ_ManagerServer.Model;

namespace ICQ_ManagerServer.Domain
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