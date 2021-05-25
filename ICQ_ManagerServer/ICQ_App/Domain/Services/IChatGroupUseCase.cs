using ICQ_AppDomain.Domain.Entities.Interface;
using ICQ_AppDomain.Entities;

namespace ICQ_AppDomain.Domain
{
    public interface IChatGroupDomain
    {
        IResponse ProcessChatData(IDataReceiver message, IUser user);
    }
}