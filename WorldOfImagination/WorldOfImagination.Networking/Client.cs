using System;
using System.Net;
using System.Net.Sockets;

namespace WorldOfImagination.Networking
{
    public class Client
    {
        TcpClient client;
        NetworkStream stream;

        public Client()
        {
            client = new TcpClient();
            connected = false;
        }

        bool connected;

        public void Connect(string a, int p)
        {
            if (connected == false)
            {
                client.Connect(IPAddress.Parse(a), p);
                if (client.Connected)
                {
                    connected = true;
                }
            }
        }

        public bool Connected()
        {
            return connected;
        }

        public void SendMessage(string s)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(s + "\r\n");
            stream = client.GetStream();

            stream.Write(data, 0, data.Length);
        }

        public string[] GetMessages()
        {
            if (client.Available != 0)
            {
                stream = client.GetStream();
                byte[] data = new byte[1024];
                int bytes = stream.Read(data, 0, data.Length);
                string rawData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                string[] messages = rawData.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                return messages;
            }
            return null;
        }

        public void Disconnect()
        {
            if (client.Connected)
            {
                stream.Close();
                client.Close();
            }
        }

        ~Client()
        {
            stream.Close();
            client.Close();
        }
    }
}