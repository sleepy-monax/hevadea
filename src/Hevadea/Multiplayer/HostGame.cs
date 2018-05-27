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
        public string _address;
        public int _port;
        public int _slots;

        public HostGame(string address, int port, int slots)
        {
            _address = address;
            _port = port;
            _slots = slots;
        }

        public void Start()
        {
            Server = new Server(_address, _port, true);

            var dispacher = new PacketDispacher<PacketType>(Server);
            dispacher.RegisterHandler(PacketType.LOGIN, HandleLOGIN);

            Server.Start(25, _slots);
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

        public void SendWorld(ConnectedClient client)
        {
            client.Send(Packets.World(World));

            foreach (var level in World.Levels)
            {
                client.Send(Packets.Level(level));

                foreach (var chunk in level.Chunks)
                {
                    var progress = (chunk.X * level.Chunks.GetLength(1) + chunk.Y) / (float)level.Chunks.Length;

                    Logger.Log<Game>($"Sending '{level.Name}' {(int)(progress * 100)}%");
                    client.Send(Packets.Chunk(chunk));
                }
            }
        }
    }
}
