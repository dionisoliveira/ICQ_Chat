using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ICQ_AppAplicattion;
using ICQ_AppDomain;
using ICQ_AppDomain.Adpters;
using ICQ_ManagerServer.Interface;

namespace ICQ_ManagerServer
{
    public class ICQWebSocket : IICQWebSocket
    {

        private TcpListener _server;
        private IChatManagerService _chatManager;
        private List<Thread> _listThask;
        private StreamWriter _serverStreamWrite;


        public ICQWebSocket(IChatManagerService chatManager)
        {
            _chatManager = chatManager;
            _listThask = new List<Thread>();

        }

        public void InitServer(string ipServer, int portServer)
        {
            try
            {

                _server = new TcpListener(IPAddress.Parse(ipServer), portServer);

                _server.Start();
                Console.WriteLine("Server is run");

                StartListenerServer();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
        }


        private void StartListenerServer()
        {
            Console.WriteLine($"Listener await connection");
            while ((true))
            {
                try
                {
                    var _clientSocket = _server.AcceptTcpClient();

                    var thread = new Thread(() => NewListernerThread(_clientSocket));
                    thread.Start();
                    _listThask.Add(thread);

                }
                catch (Exception e)
                {
                    Console.WriteLine($"Finish:{ e.Message }");
                    StartListenerServer();
                }
            }
        }

        private void NewListernerThread(TcpClient _clientSocket)
        {
            byte[] bytesFrom = new byte[408300];
            NetworkStream stream = _clientSocket.GetStream();
            StreamWriter _serverStreamWrite;
            Console.WriteLine($"Connected ");
            while (true)
            {
                try
                {

                    if (_clientSocket.Connected)
                    {
                        _serverStreamWrite = new StreamWriter(_clientSocket.GetStream());
                        stream.Read(bytesFrom, 0, (int)_clientSocket.ReceiveBufferSize);
                        var dataReceiver = System.Text.Encoding.ASCII.GetString(bytesFrom);
                        dataReceiver = dataReceiver.Substring(0, dataReceiver.IndexOf("$"));


                        var dataInput = new DataInput() { Message = dataReceiver, Socket = _serverStreamWrite };

                        Console.WriteLine(dataReceiver);

                        var resultData = _chatManager.ProcessDataReceiver(dataInput);

                        if (resultData.IsBroadCast)
                        {
                            SendBroadcast(resultData);
                        }
                        else
                        {
                            SendMessage(resultData);
                        }

                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Finish Network:{ e.Message }");
                }
            }
        }

        private void SendBroadcast(IResponse response)
        {

            foreach (var socket in response.UsersBroadcastMessage)
            {

                try
                {
                    (socket.ConnectionSocket as StreamWriter).WriteLine(response.Message + "$");
                    (socket.ConnectionSocket as StreamWriter).Flush();
                }
                catch
                {

                }
            }
        }

        private void SendMessage(IResponse response)
        {

            (response.Socket as StreamWriter).WriteLine(response.Message + "$");
            (response.Socket as StreamWriter).Flush();

        }



    }
}
