using System;
namespace ICQ_Client.Interface
{
    public interface IWebSockerClient
    {
        void InitClient(string ipServer, int portServer);
    }
}
