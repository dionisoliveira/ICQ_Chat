using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ICQ_Client.Interface;

namespace ICQ_Client.Infrastructure
{
    public class WebSocketClient : IWebSockerClient
    {
        private TcpClient _clientSocket;
        private StreamReader _serverStreamReader;
        private StreamWriter _serverStreamWrite;

        private int _maxRetryConnection = 3;
        private int _retryConnection = 0;
        private int _timeSleepToRetry = 60;

        byte[] buffer = new byte[408300];

        private string[] resultParse;
        private string useConnected;
        private string currentGroup;


        public void InitClient(string ipServer, int portServer)
        {


            while (_retryConnection < _maxRetryConnection)
            {
                try
                {
                    StartClient(ipServer, portServer);


                }
                catch (Exception e)
                {
                    _retryConnection += 1;
                    Console.WriteLine($"Tentando reconectar:{_retryConnection}");
                    Task.Delay(_timeSleepToRetry);
                    if (_retryConnection == _maxRetryConnection)
                    {
                        Console.WriteLine($"Não conseguimos reconectar no servidor: {Environment.NewLine} 1-Reconectar ;{Environment.NewLine} 2-Sair");
                        var item = Console.ReadLine();
                        if (Convert.ToInt16(item) == 1)
                            _retryConnection = 0;

                    }

                }
            }

        }

        private void StartClient(string ipServer, int portServer)
        {
            var decoder = Encoding.UTF8.GetDecoder();


            _clientSocket = new TcpClient(ipServer, portServer);
            _serverStreamReader = new System.IO.StreamReader(_clientSocket.GetStream());
            _serverStreamWrite = new System.IO.StreamWriter(_clientSocket.GetStream());

            _clientSocket.Client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiverDataCallback, buffer);



            //Start Connection
            SendData("9001 $");

            while (true)
            {
                var message = Console.ReadLine();

                if (!string.IsNullOrEmpty(useConnected) && !string.IsNullOrEmpty(currentGroup))
                {
                    message = message.Replace(" ", "\\*/");
                    SendData("GCOMMAND " + message.Trim() + " " + useConnected + " " + currentGroup);
                }
                else if (!string.IsNullOrEmpty(useConnected))

                    SendData(message + " " + useConnected + " " + currentGroup);
                else
                    SendData(message);

            }
        }


        private void ReceiverDataCallback(IAsyncResult _result)
        {
            try
            {
                byte[] byteData = _result.AsyncState as byte[];
                var messageProcess = Encoding.ASCII.GetString(byteData);
                messageProcess = messageProcess.Substring(0, messageProcess.IndexOf("$"));
               

                MountUserAndGroup(messageProcess);


                //Register new Callback 
                byte[] bufferLocal = new byte[408300];
                _clientSocket.Client.BeginReceive(bufferLocal, 0, bufferLocal.Length, SocketFlags.None, ReceiverDataCallback, bufferLocal);

            }
            catch
            {
                throw;

            }
        }

        private void MountUserAndGroup(string messageProcess)
        {
            resultParse = messageProcess.Split("\\");
            Console.WriteLine(resultParse[0]);

            if (string.IsNullOrEmpty(useConnected))
            {
                useConnected = resultParse.Length > 1 ? resultParse[1] : "";
            }
            else
            {
                currentGroup = resultParse.Length > 2 ? resultParse[resultParse.Length - 1].Trim() : "";
            }
        }


        public void SendData(string data)
        {

            var write = new StreamWriter(_clientSocket.GetStream());
            write.WriteLine(data + "$");
            write.Flush();

        }
    }
}
