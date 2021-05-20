using System;
using ICQ_ManagerServer.Domain;
using ICQ_ManagerServer.Infrastructure;
using ICQ_ManagerServer.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace ICQ_ManagerServer
{
    public class IoCRegister
    {

        public  ServiceProvider InitIoC()
        {
            return new ServiceCollection()

                .AddSingleton<IChatManager, ChatManager>()
                .AddSingleton<IWebSockertService, WebSocketServices>()
                .BuildServiceProvider();
        }
    }
}
