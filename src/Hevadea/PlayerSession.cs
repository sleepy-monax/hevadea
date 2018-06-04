using Hevadea.GameObjects;
using Hevadea.GameObjects.Entities;
using Hevadea.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea
{
    public class PlayerSession
    {
        public string Name { get; set; }
        public int Token { get; set; }
        public Player Entity { get; set; }
        public PlayerInputHandler InputHandler { get; set; }

        public bool HasJoined => _game != null;

        private Game _game;

        public PlayerSession(string name, int token, Player entity)
        {
            Name = name;
            Token = token;
            Entity = entity;
            InputHandler = new PlayerInputHandler(entity);
        }

        public void Join(Game game)
        {
            _game = game;

            if (Entity.Removed)
            {
                if (Entity.X == 0f && Entity.Y == 0f)
                    _game.World.SpawnPlayer(Entity);
                else
                    _game.World.GetLevel(Entity.LastLevel).AddEntity(Entity);
            }
        }

        public static PlayerSession Load(PlayerStorage store)
        {
            var session = new PlayerSession(store.Name, store.Token, (Player)store.Entity.ConstructEntity());
            return session;
        }

        public PlayerStorage Save()
        {
            var store = new PlayerStorage
            {
                Name = Name,
                Token = Token,
                Entity = Entity.Save()
            };

            return store;
        }
    }
}
