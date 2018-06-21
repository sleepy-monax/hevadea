using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Systems
{
    public class GameSystem 
    {
        private List<Type> _requiredComponents = new List<Type>();

        public string Name { get; }
        public GameState State { get; private set; }
        public World World { get; private set; }
        public bool NotComponentsRequired => _requiredComponents.Count == 0;

        public GameSystem(string name)
        {
            Name = name;
        }

        public GameSystem(string name, params Type[] requiredComponents)
        {
            Name = name;

            if (requiredComponents.Any(c => !typeof(EntityComponent).IsAssignableFrom(c)))
                throw new Exception($"Type passed into EntitySystem is not an {nameof(EntityComponent)}!");

            _requiredComponents.AddRange(requiredComponents);
        }

        public void Initialize(GameState state, World world)
        {
            State = state;
            World = world;
        }

        public virtual void OnGameUpdate(GameTime gameTime)
        {

        }

        public virtual void OnGameDraw(GameTime gameTime)
        {

        }

        public virtual void OnLevelUpdate(Level level, GameTime gameTime)
        {

        }

        public virtual void OnLevelDraw(Level level, LevelSpriteBatchPool spriteBatchPool, GameTime gameTime)
        {

        }
    }
}
