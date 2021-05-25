using System;
namespace ICQ_App
{
    public static class ApplicationDependency
    {
        public static class WebSocketDependency
        {

            public static void AddIoC(this IServiceCollection services)
            {
                services.AddScoped<IICQWebSocket, ICQWebSocket>();
            }
        }
    }
}
