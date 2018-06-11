using Hevadea.Framework;
using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.GameObjects.Entities.Components.States;
using Hevadea.Storage;

namespace Hevadea
{
    public class PlayerSession
    {
        public string Name { get; set; }
        public int Token { get; set; }
        public Player Entity { get; set; }
        public PlayerInputHandler InputHandler { get; set; }

        public bool HasJoined => _gameState != null;

        private GameState _gameState;

        public PlayerSession(string name, int token, Player entity)
        {
            Name = name;
            Token = token;
            Entity = entity;
            InputHandler = new PlayerInputHandler(entity);
        }

        public void Join(GameState gameState)
        {
            _gameState = gameState;
            _gameState.Players.Add(this);

            Logger.Log<PlayerSession>($"{Name} joint the game!");

            if (Entity.Removed)
            {
                if (Entity.X == 0f && Entity.Y == 0f)
                    _gameState.World.SpawnPlayer(Entity);
                else
                    _gameState.World.GetLevel(Entity.LastLevel).AddEntity(Entity);
            }
        }

        public void Respawn()
        {
            Entity.Remove();
            Entity.GetComponent<Health>().HealAll();
            _gameState.World.SpawnPlayer(Entity);
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