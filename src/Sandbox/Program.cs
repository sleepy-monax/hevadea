using Hevadea.Framework.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    public class Program
    {
        static void Main(string[] args)
        {

            if (Console.ReadKey().Key == ConsoleKey.C)
            {
                doClient();
            }
            else
            {
                doServer();
            }


        }


        static void doClient()
        {
            Client client = new Client(true);
            client.Connect("127.0.0.1", 7777, 16);
            client.Send(new PacketBuilder().WriteStringASCII("Hello world!").GetBuffer());
        }

        static void doServer()
        {
            Server server = new Server("127.0.0.1", 7777, true);

            server.DataReceived = (connection, data) => 
            {
                Console.WriteLine(new PacketBuilder(data).ReadStringASCII());
            };

            server.Start();
        }
    }
}
