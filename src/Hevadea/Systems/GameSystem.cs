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
    public class GameSystem<T0> : GameSystem 
        where T0 : EntityComponent 
    {
        public GameSystem(string name) : base(name, typeof(T0))
        { }
    }

    public class GameSystem<T0, T1> : GameSystem 
        where T0 : EntityComponent 
        where T1 : EntityComponent
    {
        public GameSystem(string name) : base(name, typeof(T0), typeof(T1))
        { }
    }

    public class GameSystem<T0, T1, T2> : GameSystem 
        where T0 : EntityComponent
        where T1 : EntityComponent
        where T2 : EntityComponent
    {
        public GameSystem(string name) : base(name, typeof(T0), typeof(T1), typeof(T2))
        { }
    }

    public class GameSystem<T0, T1, T2, T3> : GameSystem 
        where T0 : EntityComponent 
        where T1 : EntityComponent 
        where T2 : EntityComponent 
        where T3 : EntityComponent
    {
        public GameSystem(string name) : base(name, typeof(T0), typeof(T1), typeof(T2), typeof(T3))
        { }
    }

    public class GameSystem<T0, T1, T2, T3, T4> : GameSystem
        where T0 : EntityComponent
        where T1 : EntityComponent
        where T2 : EntityComponent 
        where T3 : EntityComponent 
        where T4 : EntityComponent
    {
        public GameSystem(string name) : base(name, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4))
        { }
    }

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
