using System;
namespace ICQ_AppDomain.Domain.Entities.Interface
{
    public interface IDataReceiver
    {
        public string Message { get;  }
        public object Socket { get;  }


    }
}
