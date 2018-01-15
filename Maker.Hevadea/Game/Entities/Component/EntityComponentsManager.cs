using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Hevadea.Game.Entities.Component
{
    public class EntityComponentsManager
    {

        Entity Owner;
        private List<EntityComponent> components = new List<EntityComponent>();

        public EntityComponentsManager(Entity owner)
        {
            Owner = owner;
        }

        public void Add(EntityComponent component)
        {
            foreach (var e in components)
            {
                if (e == component) return;
            }

            components.Add(component);
            component.Owner = Owner;

            components.Sort((a, b) => (0xff - a.Priority).CompareTo(0xff - b.Priority));
        }

        public T Get<T>() where T : EntityComponent
        {
            foreach (var e in components)
            {
                if (e is T component)
                {
                    return component;
                }
            }

            return null;
        }

        public bool Has<T>()
        {
            foreach (var e in components)
            {
                if (e is T)
                {
                    return true;
                }
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var c in components)
            {
                if (c is IDrawableComponent drawable)
                {
                    drawable.Draw(spriteBatch, gameTime);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var c in components)
            {
                if (c is IUpdatableComponent updatable)
                {
                    updatable.Update(gameTime);
                }
            }
        }

        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var c in components)
            {
                if (c is IDrawableOverlayComponent drawable)
                {
                    drawable.DrawOverlay(spriteBatch, gameTime);
                }
            }
        }
    }
}
