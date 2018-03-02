using Hevadea.Game.Entities.Component;
using Hevadea.Game.Storage;
using Hevadea.Game.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities
{
    public partial class Entity
    {
        internal void Initialize(Level level, World world, GameManager game)
        {
            Level = level;
            World = world;
            Game = game;
        }
        
        public EntityStorage Save()
        {
            var store = new EntityStorage(Blueprint.Name);

            store.Set("X", X);
            store.Set("Y", Y);
            store.Set("Facing", (int) Facing);

            foreach (var c in _components)
                if (c is ISaveLoadComponent s)
                    s.OnGameSave(store);
            OnSave(store);

            return store;
        }

        public void Load(EntityStorage store)
        {
            X = store.GetFloat("X", X);
            Y = store.GetFloat("Y", Y);

            Facing = (Direction)(int)store.Get("Facing", (int) Facing);

            foreach (var c in _components)
                if (c is ISaveLoadComponent s)
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
                if (c is IUpdatableComponent updatable)
                    updatable.Update(gameTime);
            
            OnUpdate(gameTime);
            ParticleSystem.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var c in _components)
                if (c is IDrawableComponent drawable)
                    drawable.Draw(spriteBatch, gameTime);
            
            OnDraw(spriteBatch, gameTime);
            ParticleSystem.Draw(spriteBatch, gameTime);
        }

        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var c in _components)
                if (c is IDrawableOverlayComponent drawable)
                    drawable.DrawOverlay(spriteBatch, gameTime);
        } 
    }
}