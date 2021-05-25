using ICQ_AppDomain;
using ICQ_AppDomain.Domain;
using ICQ_AppDomain.Entities;
using ICQ_ManagerServer.Domain;
using ICQ_ManagerServer.Interface;
using ICQ_WebSocketAdapter;
using Microsoft.Extensions.DependencyInjection;


namespace ICQ_ManagerServer
{
    public class IoCRegister
    {

        public ServiceProvider InitIoC()
        {
            var container = new ServiceCollection();

            WebSocketDependency.AddIoC(container);

            var provider = container
                .AddSingleton<IChatManagerService, ChatManager>()
                 .AddSingleton<IChatGroupDomain, ChatGroupDomain>()
                 .AddSingleton<IChatUserDomain, ChatUserDomain>()
                 .AddSingleton<IResponse, Response>()

                .BuildServiceProvider();

            return provider;
        }
    }
}
