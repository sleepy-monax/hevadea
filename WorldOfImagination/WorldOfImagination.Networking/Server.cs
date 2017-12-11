using System;
using System.Net;
using System.Net.Sockets;

namespace WorldOfImagination.Networking
{
    public class Server
    {
        TcpListener listener;

        public Server(string a, int p)
        {
            listener = new TcpListener(IPAddress.Parse(a), p);
            listener.Start();
            connected = false;
        }

        TcpClient client;
        NetworkStream stream;
        bool connected;

        public void CheckForNewConnection()
        {
            if (listener.Pending())
            {
                client = listener.AcceptTcpClient();
                stream = client.GetStream();
                connected = true;
            }
            else
            {
                connected = false;
            }
        }

        public bool Connected()
        {
            return connected;
        }

        public void SendMessage(string m)
        {
            byte[] msgBytes = System.Text.Encoding.ASCII.GetBytes(m + "\r\n");
            stream.Write(msgBytes, 0, msgBytes.Length);
        }

        public string[] GetMessages()
        {
            if (client.Available != 0)
            {
                stream = client.GetStream();
                byte[] data = new byte[1024];

                Int32 bytes = stream.Read(data, 0, data.Length);
                string rawData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                string[] messages = rawData.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                return messages;
            }
            else
            {
                return null;
            }
        }

        public void DisconnectLost()
        {
            if (!connected)
            {
                client.Close();
            }
        }

        public void Disconnect()
        {
            if (client.Connected)
            {
                client.Close();
            }
        }

        ~Server()
        {
            client.Close();
            listener.Stop();
        }
    }
}