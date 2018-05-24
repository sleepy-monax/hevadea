using Hevadea.Framework.Networking;
using System;

namespace Sandbox
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (Console.ReadKey().Key == ConsoleKey.C)
            {
                DoClient();
            }
            else
            {
                DoServer();
            }
        }


        static void DoClient()
        {
            Client client = new Client(true);

            client.Connect("127.0.0.1", 7777, 16);

            var i = 0;

            while (true)
                client.Send(new PacketBuilder().WriteStringASCII($"Hello world {i++}!").Buffer);
        }

        static void DoServer()
        {
            Server server = new Server("127.0.0.1", 7777, true);

            server.DataReceived = (connection, data) => 
            {
                Console.Write(new PacketBuilder(data).ReadStringASCII());
            };

            server.Start();
        }
    }
}
