using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
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

                ListenerClient();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
        }


        private void ListenerClient()
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
                    ListenerClient();
                }
            }
        }

        private void NewListernerThread(TcpClient _clientSocket)
        {
            byte[] bytesFrom = new byte[408300];
            NetworkStream stream = _clientSocket.GetStream();
            StreamWriter _serverStreamWrite;
            while (true)
            {
                try
                {

                    if (_clientSocket.Connected)
                    {


                        _serverStreamWrite = new System.IO.StreamWriter(_clientSocket.GetStream());
                        stream.Read(bytesFrom, 0, (int)_clientSocket.ReceiveBufferSize);
                        var messageProcess = System.Text.Encoding.ASCII.GetString(bytesFrom);
                        messageProcess = messageProcess.Substring(0, messageProcess.IndexOf("$"));



                        var messagereturn = _chatManager.ProcessMessage(messageProcess, _serverStreamWrite);

                        if (messagereturn.IsBroadCast)
                        {
                            SendBroadcast(messagereturn);
                        }
                        else
                        {
                            if (messagereturn.ClientSocket == null)
                            {
                                SendMessage(messagereturn, _serverStreamWrite);
                            }
                            else
                            {
                                SendMessage(messagereturn, messagereturn.ClientSocket as StreamWriter);
                            }
                        }

                        _serverStreamWrite.Flush();

                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Finish Network:{ e.Message }");
                    //  _serverStreamWrite.Close();
                }


            }
        }

        private void SendBroadcast(IResponse returnMessage)
        {

            foreach (var socket in returnMessage.UsersBroadcastMessage)
            {

                try
                {
                    (socket.ConnectionSocket as StreamWriter).WriteLine(returnMessage.Message + "$");
                    (socket.ConnectionSocket as StreamWriter).Flush();
                }
                catch
                {

                }
            }
        }

        private void SendMessage(IResponse returnMessage, StreamWriter streamWriter)
        {

            streamWriter.WriteLine(returnMessage.Message + "$");
            streamWriter.Flush();

        }



    }
}
