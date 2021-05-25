using ICQ_AppDomain;
using ICQ_AppDomain.Domain.Entities.Interface;

namespace ICQ_ManagerServer.Interface
{
    public interface IChatManagerService
    {
        IResponse ConnectionStabilished(IDataReceiver message);
        IResponse ProcessDataReceiver(IDataReceiver message);
      
    }
}
