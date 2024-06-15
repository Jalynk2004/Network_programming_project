using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections;
namespace ChessUI
{

    internal class ChatServer
    {
        public static byte[] Key = Encoding.UTF8.GetBytes("lmaoomalmaololma");
        public static byte[] IV = Encoding.UTF8.GetBytes("asdba981324gfht2");
        private TcpListener server;
        private List<TcpClient> clients = new List<TcpClient>();
        private Thread listenThread;
        public byte[] encryptAES(byte[] dataToEncrypt)
        {

            if (dataToEncrypt == null || dataToEncrypt.Length <= 0)
                throw new ArgumentNullException("dataToEncrypt");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            byte[] encrypted;

            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                        csEncrypt.FlushFinalBlock();
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }
        public byte[] DecryptAES(byte[] dataToDecrypt)
        {
            // Check arguments.
            if (dataToDecrypt == null || dataToDecrypt.Length <= 0)
                throw new ArgumentNullException("dataToDecrypt");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            byte[] decrypted;

            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(dataToDecrypt))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            decrypted = Encoding.UTF8.GetBytes(srDecrypt.ReadToEnd());
                        }
                    }
                }
            }

            // Return the decrypted bytes.
            return decrypted;
        }
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
                    byte[] MessageBytes = Encoding.UTF8.GetBytes(receivedMessage);
                    byte[] Encrypted_message_bytes = encryptAES(MessageBytes);
                    string Encryted_message = Encoding.UTF8.GetString(Encrypted_message_bytes);
                    BroadcastMessage(Encryted_message);
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
