using Maker.Hevadea.Enum;
using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Tiles;
using Maker.Hevadea.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Maker.Hevadea.Game.Entities
{
    public class Entity
    {
        public Level Level { get; private set; }
        public World World { get; private set; }
        public GameScene Game { get; private set; }

        public float X { get; private set; }
        public float Y { get; private set; }
        public Point Origin = new Point(0,0);
        public int Width { get; set; } = 32;
        public int Height { get; set; } = 48;
        public Direction Facing { get; set; } = Direction.Down;

        public bool Removed { get; set; } = true;

        public Light Light { get; set; } = new Light();

        public Point Size => new Point(Width, Height);
        public Vector2 Position => new Vector2(X, Y);
        public Rectangle Bound => new Rectangle(Position.ToPoint(), Size);

        internal void Initialize(Level level, World world, GameScene game)
        {
            Level = level;
            World = world;
            Game = game;
        }

        public List<EntityComponent> Components { get; private set; } = new List<EntityComponent>();

        // Entity Components --------------------------------------------------

        public void AddComponent(EntityComponent component)
        {
            foreach (var e in Components)
            {
                if (e == component) return;
            }

            Components.Add(component);
            component.Owner = this;

            Components.Sort((a, b) => (0xff - a.Priority).CompareTo(0xff - b.Priority));
        }

        public T GetComponent<T>() where T : EntityComponent
        {
            foreach (var e in Components)
            {
                if (e is T component)
                {
                    return component;
                }
            }

            throw new Exception($"Component {nameof(T)} not found.");
        }

        public bool HasComponent<T>() where T : EntityComponent
        {
            foreach (var e in Components)
            {
                if (e is T)
                {
                    return true;
                }
            }

            return false;
        }

        // Health macanic -----------------------------------------------------

        public virtual int ComputeDamages(int damages)
        {
            return damages;
        }

        public void Remove()
        {
            Level.RemoveEntity(this);
        }




        // Movement and colisions ---------------------------------------------

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

        public virtual bool IsBlocking(Entity entity)
        {
            return false;
        }

        public bool IsColliding(Rectangle rect)
        {
            return IsColliding(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public bool IsColliding(Entity e)
        {
            return IsColliding(e.X, e.Y, e.Width, e.Height);
        }

        public bool IsColliding(float x, float y, int width1, int height1)
        {
            return X < x + width1 &&
                   X + Width > x &&
                   Y < y + height1 &&
                   Height + Y > y;
        }

        // Update and Draw
        public void Update(GameTime gameTime)
        {
            // always update component before the entity.
            foreach (var c in Components)
            {
                if (c is IUpdatableComponent updatable)
                {
                    updatable.Update(gameTime);
                }
            }

            OnUpdate(gameTime);
        }

        public virtual void OnUpdate(GameTime gameTime)
        {
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

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var c in Components)
            {
                if (c is IDrawableComponent drawable)
                {
                    drawable.Draw(spriteBatch, gameTime);
                }
            }

            OnDraw(spriteBatch, gameTime);
        }

        public virtual void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.FillRectangle(new Rectangle(X, Y, Width, Height), Color.Red);
        }

        public TilePosition GetTilePosition()
        {
            return new TilePosition((int)(X / ConstVal.TileSize), (int)(Y / ConstVal.TileSize));
        }
    }
}