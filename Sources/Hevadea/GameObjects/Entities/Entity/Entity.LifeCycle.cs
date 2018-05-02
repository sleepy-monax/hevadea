using Hevadea.Framework;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.Storage;
using Hevadea.Utils;
using Hevadea.Worlds;
using Hevadea.Worlds.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities
{
    public partial class Entity
    {
        internal void Initialize(Level level, World world, GameManager game)
        {
            Level = level;
            World = world;
            Game = game;

            _components.Sort((a, b) => (0xff - a.Priority).CompareTo(0xff - b.Priority));

            if (Ueid == -1 && world != null)
            {
                Ueid = world.GetUeid();
            }
        }

        public EntityStorage Save()
        {
            var store = new EntityStorage(Blueprint.Name)
            {
                Type = Blueprint.Name,
                Ueid = Ueid
            };

            store.Set("X", X);
            store.Set("Y", Y);
            store.Set("Facing", (int)Facing);

            foreach (var c in _components)
                if (c is IEntityComponentSaveLoad s)
                    s.OnGameSave(store);
            OnSave(store);

            return store;
        }

        public void Load(EntityStorage store)
        {
            Ueid = store.Ueid;
            X = store.GetFloat("X", X);
            Y = store.GetFloat("Y", Y);

            Facing = (Direction)(int)store.Get("Facing", (int)Facing);

            foreach (var c in _components)
                if (c is IEntityComponentSaveLoad s)
                    s.OnGameLoad(store);

            OnLoad(store);
        }

        public void Remove()
        {
            Level.RemoveEntity(this);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var c in _components)
                if (c is IEntityComponentUpdatable updatable)
                    updatable.Update(gameTime);

            OnUpdate(gameTime);
            ParticleSystem.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var c in _components)
                if (c is IEntityComponentDrawable drawable)
                    drawable.Draw(spriteBatch, gameTime);

            Renderer.Render(this, spriteBatch, gameTime);
            OnDraw(spriteBatch, gameTime);

            if (Rise.ShowDebug)
                spriteBatch.DrawString(Ressources.FontHack, Ueid.ToString(), new Vector2(X, Y), Color.Gold, 0f, Vector2.Zero, 1f / 4f, SpriteEffects.None, 0f);

            ParticleSystem.Draw(spriteBatch, gameTime);
        }

        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var c in _components)
                if (c is IEntityComponentDrawableOverlay drawable)
                    drawable.DrawOverlay(spriteBatch, gameTime);
        }
    }
}