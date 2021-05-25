using ICQ_AppDomain.Entities;

namespace ICQ_AppDomain
{
    public interface IChatUserDomain
    {
        IResponse CreateUser(string user_identifier, object socket);
        IResponse GetAllUser();
        IUser GetUser(string user_identifier);
        IResponse DMUser(string user_identifier, string message, IUser userMessage);
    }
}