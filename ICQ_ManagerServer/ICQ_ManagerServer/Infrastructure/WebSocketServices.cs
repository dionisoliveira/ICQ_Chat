using System;
using System.Net;
using System.Net.Sockets;
using ICQ_ManagerServer.Interface;

namespace ICQ_ManagerServer.Infrastructure
{
    public class WebSocketServices : IWebSockertService
    {

        private TcpListener _server;
        private TcpClient _clientSocket;
        private IChatManager _chatManager;





        public WebSocketServices(IChatManager chatManager)
        {
            _chatManager = chatManager;

        }


        private void ListenerClient()
        {
            Console.WriteLine($"Listener await connection");
            while ((true))
            {
                try
                {

                    _clientSocket = _server.AcceptTcpClient();
                    byte[] bytesFrom = new byte[408300];

                    NetworkStream neworkStrem = _clientSocket.GetStream();
                    neworkStrem.Read(bytesFrom, 0, (int)_clientSocket.ReceiveBufferSize);
                    var messageProcess = System.Text.Encoding.ASCII.GetString(bytesFrom);


                    _chatManager.ProcessMessage(messageProcess, _clientSocket);

                }
                catch (Exception e)
                {
                    Console.WriteLine($"Fail run server:{ e.Message }");
                    throw;
                }
            }
        }

        public void InitServer(string ipServer, int portServer)
        {
            try
            {
                _clientSocket = default(TcpClient);

                _server = new TcpListener(IPAddress.Parse(ipServer), portServer);

                _server.Start();
                Console.WriteLine("Server is run");

                ListenerClient();

            }
            catch (Exception e)
            {
                Console.WriteLine($"Fail run server:{ e.Message }");
                throw;
            }
        }


    }
}
