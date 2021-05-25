using System;
using ICQ_ManagerServer.Domain;
using ICQ_ManagerServer.Infrastructure;
using ICQ_ManagerServer.Interface;
using ICQ_ManagerServer.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace ICQ_ManagerServer
{
    public class IoCRegister
    {

        public ServiceProvider InitIoC()
        {
            return new ServiceCollection()

                .AddSingleton<IChatManagerService, ChatManager>()
                 .AddSingleton<IChatGroupDomain, ChatGroupDomain>()
                 .AddSingleton<IChatUserDomain, ChatUserDomain>()
                 .AddSingleton<IResponse, Response>()
                .AddSingleton<IICQWebSockert, WebSocketServices>()
                .BuildServiceProvider();
        }
    }
}
