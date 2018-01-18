using Maker.Hevadea.Game.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Add(EntityComponent component)
        {
            foreach (var e in Components)
            {
                if (e == component) return;
            }

            Components.Add(component);
            component.Owner = Owner;

            Components.Sort((a, b) => (0xff - a.Priority).CompareTo(0xff - b.Priority));
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
                    s.OnSave(store);
                }
            }
        }

        public void Load(EntityStorage store)
        {
            foreach (var c in Components)
            {
                if (c is ISaveLoadComponent s)
                {
                    s.OnLoad(store);
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
