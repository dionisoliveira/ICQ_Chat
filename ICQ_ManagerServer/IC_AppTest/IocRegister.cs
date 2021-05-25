using System;
using ICQ_AppDomain;
using ICQ_AppDomain.Adpters;
using ICQ_AppDomain.Domain;
using ICQ_AppDomain.Entities;
using ICQ_ManagerServer.Domain;
using ICQ_ManagerServer.Interface;
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
                .AddSingleton<IICQWebSocket, ICQWebSocket>()
                .BuildServiceProvider();
        }
    }
}
