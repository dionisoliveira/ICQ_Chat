using System;
using ICQ_ManagerServer.Infrastructure;
using ICQ_ManagerServer.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace ICQ_ManagerServer
{
    class Program
    {

        private static  ServiceProvider _container;

        static void Main(string[] args)
        {
            _container = new IoCRegister().InitIoC();
            StartServer();
        }


        private static void StartServer()
        {
            var server = _container.GetService<IWebSockertService>();
            server.InitServer("127.0.0.1",8085);
        }
    }
}
