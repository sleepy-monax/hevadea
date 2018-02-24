using Hevadea.Game.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Game.Entities.Component
{
    public sealed class EntityComponentsManager
    {
        private readonly List<EntityComponent> _components = new List<EntityComponent>();
        private readonly Entity _owner;

        public EntityComponentsManager(Entity owner)
        {
            _owner = owner;
        }

        public T Add<T>(T component) where T : EntityComponent
        {
            if (_components.Any(e => e == component)) return null;

            _components.Add(component);
            component.Owner = _owner;

            _components.Sort((a, b) => (0xff - a.Priority).CompareTo(0xff - b.Priority));

            return component;
        }

        public void Adds(params EntityComponent[] components)
        {
            foreach (var c in components) Add(c);
        }

        public T Get<T>() where T : EntityComponent
        {
            foreach (var e in _components)
                if (e is T component)
                    return component;

            return null;
        }

        public bool Has<T>() where T : EntityComponent
        {
            return _components.OfType<T>().Any();
        }


        public void Save(EntityStorage store)
        {
            foreach (var c in _components)
                if (c is ISaveLoadComponent s)
                    s.OnGameSave(store);
        }

        public void Load(EntityStorage store)
        {
            foreach (var c in _components)
                if (c is ISaveLoadComponent s)
                    s.OnGameLoad(store);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var c in _components)
                if (c is IDrawableComponent drawable)
                    drawable.Draw(spriteBatch, gameTime);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var c in _components)
                if (c is IUpdatableComponent updatable)
                    updatable.Update(gameTime);
        }

        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var c in _components)
                if (c is IDrawableOverlayComponent drawable)
                    drawable.DrawOverlay(spriteBatch, gameTime);
        }
    }
}