using Hevadea.Framework;
using Hevadea.Framework.Networking;
using Hevadea.Framework.Utils;
using Hevadea.GameObjects;
using Hevadea.GameObjects.Entities;
using System.Net.Sockets;

namespace Hevadea.Multiplayer
{
    public class HostGame : Game
    {
        public Server Server;

        public HostGame(string address, int port, int slots)
        {
            Server = new Server(address, port, true);

            var dispacher = new PacketDispacher<PacketType>(Server);
            dispacher.RegisterHandler(PacketType.LOGIN, HandleLOGIN);

            Server.Start(25, slots);
        }

        public void ShutdownSever()
        {
            Server.Stop();
        }

        public void HandleLOGIN(Socket socket, byte[] data)
        {
            var client = Server.GetClientFrom(socket);

            new PacketBuilder(data)
                .Ignore(sizeof(int))
                .ReadStringUTF8(out var userName)
                .ReadInteger(out var token)
                .ReadStringUTF8(out var gameInfo);

            Logger.Log<Game>($"User '{userName}' login with token '{token}'...");

            if (token == 0)
            {
                token = Rise.Rnd.NextInt();
                Logger.Log<Game>($"{userName}' token is now {token}!");
            }

            client.Send(Packets.Token(token));
            SendWorld(client);

            var newPlayer = (Player)EntityFactory.PLAYER.Construct();
            World.SpawnPlayer(newPlayer);
            client.Send(Packets.Join(newPlayer));
        }
    }
}
