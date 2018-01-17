using Maker.Hevadea.Enum;
using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Storage;
using Maker.Hevadea.Game.Tiles;
using Maker.Hevadea.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Maker.Hevadea.Game.Entities
{
    public class Entity
    {
        public Level Level { get; private set; }
        public World World { get; private set; }
        public GameScene Game { get; private set; }
        public EntityComponentsManager Components { get; private set; }

        public float X          { get; private set; }
        public float Y          { get; private set; }
        public Point Origin     { get; set; }
        public int Width        { get; set; }
        public int Height       { get; set; }
        public Direction Facing { get; set; }

        public bool Removed     { get; set; } = true;

        public Point Size => new Point(Width, Height);
        public Vector2 Position => new Vector2(X, Y);
        public Rectangle Bound => new Rectangle(Position.ToPoint(), Size);

        public Entity()
        {
            Components = new EntityComponentsManager(this);
            Facing = Direction.Down;
            Origin = new Point(0, 0);
            Width = 32;
            Height = 32;
        }

        internal void Initialize(Level level, World world, GameScene game)
        {
            Level = level;
            World = world;
            Game = game;
        }

        public EntityStorage Save()
        {
            var store = new EntityStorage(this.GetType().FullName);

            store.Set("X", X);
            store.Set("Y", Y);
            store.Set("Width", Width);
            store.Set("Height", Height);
            store.Set("Facing", (int)Facing);

            Components.Save(store);
            OnSave(store);

            return store;
        }

        public void Load(EntityStorage store)
        {
            X = store.Get("X", X);
            Y = store.Get("Y", Y);
            Width = store.Get("Width", Width);
            Height = store.Get("Height", Height);

            Facing = (Direction)store.Get("Facing", (int)Facing);

            Components.Load(store);
            OnLoad(store);
        }

        public virtual void OnSave(EntityStorage store) {}
        public virtual void OnLoad(EntityStorage store) {}

        public void Remove()
        {
            Level.RemoveEntity(this);
        }

        public virtual bool IsBlocking(Entity entity)
        {
            return false;
        }

        public bool IsColliding(Rectangle rect)
        {
            return IsColliding(rect.X, rect.Y, rect.Width, rect.Height);
        }
        public bool IsColliding(float x, float y, int width1, int height1)
        {
            return X < x + width1 &&
                   X + Width > x &&
                   Y < y + height1 &&
                   Height + Y > y;
        }

        public void Update(GameTime gameTime)
        {
            Components.Update(gameTime);
            OnUpdate(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Components.Draw(spriteBatch, gameTime);
            OnDraw(spriteBatch, gameTime);
        }
        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        public virtual void OnUpdate(GameTime gameTime)
        {
        }
        public virtual void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Do nothing.
        }

        public TilePosition GetTilePosition()
        {
            return new TilePosition((int)(X / ConstVal.TileSize), (int)(Y / ConstVal.TileSize));
        }
        public virtual void SetPosition(float x, float y)
        {
            var oldPosition = GetTilePosition();

            X = x;
            Y = y;

            var pos = GetTilePosition();

            //TODO: make this smarter
            Level?.RemoveEntityFromTile(oldPosition, this);
            Level?.AddEntityToTile(pos, this);
        }
    }
}