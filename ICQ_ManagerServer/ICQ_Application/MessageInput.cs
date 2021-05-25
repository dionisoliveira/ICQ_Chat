using ICQ_AppDomain.Domain.Entities.Interface;

namespace ICQ_AppAplicattion
{
    public class DataInput : IDataReceiver
    {
        public string Message { get; set; }
        public object Socket { get; set; }
    }
}
