using ICQ_AppDomain.Domain.Entities.Interface;
using ICQ_AppDomain.Entities;

namespace ICQ_AppDomain
{
    public interface IChatUserDomain
    {

        IResponse ProcessUserData(IDataReceiver dataInput);
        IUser GetUser(IDataReceiver dataInput);
    }
}