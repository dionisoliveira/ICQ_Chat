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
        StreamReader _serverStreamReader;
        StreamWriter _serverStreamWrite;
        private int _maxRetryConnection = 3;
        private int _retryConnection = 0;
        private int _timeSleepToRetry = 60;
        public static int dataBufferSize = 408300;
        byte[] buffer = new byte[408300]; // Adapt the size based on what you want to do with the data
        char[] charBuffer = new char[408300];
        int bytesRead;
        private byte[] receiveBuffer = new byte[408300]; //
        private string[] resultParse;
        private string command;


        public void InitClient(string ipServer, int portServer)
        {


            //   while (_retryConnection < _maxRetryConnection)
            //  {
            try
            {

                var decoder = Encoding.UTF8.GetDecoder();


                _clientSocket = new TcpClient(ipServer, portServer);
                _serverStreamReader = new System.IO.StreamReader(_clientSocket.GetStream());
                _serverStreamWrite = new System.IO.StreamWriter(_clientSocket.GetStream());

                _clientSocket.Client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallback, buffer);



                SendMessage("9001 \\ $");

                while (true)
                {
                    var message = Console.ReadLine();

                    SendMessage(command + "\\" + message);

                }

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

            //  }
        }
        private void ReceiveCallback(IAsyncResult _result)
        {
            try
            {
                byte[] byteData = _result.AsyncState as byte[];
                var messageProcess = System.Text.Encoding.ASCII.GetString(byteData);
                messageProcess = messageProcess.Substring(0, messageProcess.IndexOf("$"));
                resultParse = messageProcess.Split("\\");
                Console.WriteLine(resultParse[0]);
                    command = "0";
               // command = resultParse.Length > 1 ? resultParse[1] : "";
                byte[] bufferLocal = new byte[408300]; // Adapt the size based on what you want to do with the data
                _clientSocket.Client.BeginReceive(bufferLocal, 0, bufferLocal.Length, SocketFlags.None, ReceiveCallback, bufferLocal);






            }
            catch (Exception e)
            {
                Console.WriteLine($"Não conseguimos reconectar no servidor: {Environment.NewLine} 1-Reconectar ;{Environment.NewLine} 2-Sair");
                byte[] outStrem = System.Text.Encoding.ASCII.GetBytes("9001 \\ $");



            }
        }

        private void ReceiverData(SocketAsyncEventArgs events)
        {

        }

        public void SendMessage(string message)
        {
            byte[] outStrem = System.Text.Encoding.ASCII.GetBytes(message + "$");
            var write = new StreamWriter(_clientSocket.GetStream());
            write.WriteLine(message + "$");
            write.Flush();




        }
    }
}
