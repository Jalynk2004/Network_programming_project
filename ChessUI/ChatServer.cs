using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChessUI
{
    internal class ChatServer
    {
        private TcpListener server;
        private List<TcpClient> clients = new List<TcpClient>();
        private Thread listenThread;

        public ChatServer()
        {
            server = new TcpListener(IPAddress.Any, 8080);
            server.Start();
            listenThread = new Thread(ListenForClients);
            listenThread.Start();
            Console.WriteLine("Server is listening on port 8080...");
        }

        private void ListenForClients()
        {
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                clients.Add(client);

                string clientInfo = "New client connected from " + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString() + ":" + ((IPEndPoint)client.Client.RemoteEndPoint).Port.ToString();
                Console.WriteLine(clientInfo);

                Thread clientThread = new Thread(HandleClient);
                clientThread.Start(client);
            }
        }

        private void HandleClient(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();
            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                try
                {
                    bytesRead = clientStream.Read(message, 0, 4096);
                    if (bytesRead == 0)
                    {
                        break; // Client disconnected
                    }

                    string receivedMessage = Encoding.ASCII.GetString(message, 0, bytesRead);
                    BroadcastMessage(receivedMessage);
                    Console.WriteLine(receivedMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    break;
                }
            }

            clients.Remove(tcpClient);
            tcpClient.Close();
        }

        private void BroadcastMessage(string message)
        {
            foreach (TcpClient client in clients)
            {
                NetworkStream clientStream = client.GetStream();
                byte[] broadcastMessage = Encoding.ASCII.GetBytes(message);
                clientStream.Write(broadcastMessage, 0, broadcastMessage.Length);
            }
        }

        static void Main(string[] args)
        {
            ChatServer server = new ChatServer();
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}
