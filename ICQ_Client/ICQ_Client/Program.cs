using System;
using ICQ_Client.Infrastructure;

namespace ICQ_Client
{
    class Program
    {
        static void Main(string[] args)
        {
             new WebSocketClient().InitClient("127.0.0.1", 8085);
        }
    }
}
