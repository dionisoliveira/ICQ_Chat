using System;
using ICQ_AppDomain.Adpters;
using ICQ_ManagerServer;
using Microsoft.Extensions.DependencyInjection;

namespace ICQ_WebSocketAdapter
{
    public static class WebSocketDependency
    {

        public static void AddIoC(this IServiceCollection services)
        {
            services.AddScoped<IICQWebSocket, ICQWebSocket>();
        }
    }
}
