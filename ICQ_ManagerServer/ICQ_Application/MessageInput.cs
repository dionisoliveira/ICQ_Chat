using ICQ_AppDomain.Domain.Entities.Interface;

namespace ICQ_AppAplicattion
{
    public class DataInput : IDataReceiver
    {
        public string Message { get;  }
        public object Socket { get;  }


        public DataInput(string message, object socket)
        {
            Message = message;
            Socket = socket;
        }
    }
}
