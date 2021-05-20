using ICQ_ManagerServer.Model;

namespace ICQ_ManagerServer.Domain
{
    public interface IChatUserDomain
    {
        IResponse CreateUser(string user_identifier, object socket);
        IResponse GetAllUser();
        IUser GetUser(string user_identifier);
    }
}