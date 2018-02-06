using Maker.Hevadea.Game.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Maker.Hevadea.Game.Entities.Component
{
    public sealed class EntityComponentsManager
    {
        public Entity Owner { get; private set; }
        public List<EntityComponent> Components { get; private set; } = new List<EntityComponent>();

        public EntityComponentsManager(Entity owner)
        {
            Owner = owner;
        }

        public T Add<T>(T component) where T : EntityComponent
        {
            if (Components.Any(e => e == component))
            {
                return null;
            }

            Components.Add(component);
            component.Owner = Owner;

            Components.Sort((a, b) => (0xff - a.Priority).CompareTo(0xff - b.Priority));

            return component;
        }

        public void Adds(params EntityComponent[] components)
        {
            foreach (var c in components)
            {
                Add(c);
            }
        }

        public T Get<T>() where T : EntityComponent
        {
            foreach (var e in Components)
            {
                if (e is T component)
                {
                    return component;
                }
            }

            return null;
        }

        public bool Has<T>() where T : EntityComponent
        {
            return Components.OfType<T>().Any();
        }


        public void Save(EntityStorage store)
        {
            foreach (var c in Components)
            {
                if (c is ISaveLoadComponent s)
                {
                    s.OnGameSave(store);
                }
            }
        }

        public void Load(EntityStorage store)
        {
            foreach (var c in Components)
            {
                if (c is ISaveLoadComponent s)
                {
                    s.OnGameLoad(store);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var c in Components)
            {
                if (c is IDrawableComponent drawable)
                {
                    drawable.Draw(spriteBatch, gameTime);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var c in Components)
            {
                if (c is IUpdatableComponent updatable)
                {
                    updatable.Update(gameTime);
                }
            }
        }

        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var c in Components)
            {
                if (c is IDrawableOverlayComponent drawable)
                {
                    drawable.DrawOverlay(spriteBatch, gameTime);
                }
            }
        }
    }
}
