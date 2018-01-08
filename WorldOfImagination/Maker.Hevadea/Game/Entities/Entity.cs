using Maker.Hevadea.Enum;
using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Game.Tiles;
using Maker.Rise.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Maker.Hevadea.Game.Entities
{
    public class Entity
    {
        public Level Level;
        public World World;

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; set; } = 32;
        public int Height { get; set; } = 48;
        public Direction Facing { get; set; } = Direction.Down;

        public int Health { get; set; } = 1;
        public int MaxHealth { get; set; } = 1;

        public bool Removed { get; set; } = true;
        public bool NoClip { get; set; } = false;
        public bool IsInvincible { get; set; } = true;

        public Light Light { get; set; } = new Light();

        public Point Size => new Point(Width, Height);
        public Point Position => new Point(X, Y);
        public Rectangle Bound => new Rectangle(Position, Size);

        internal void Initialize(Level level, World world)
        {
            Level = level;
            World = world;
        }

        private readonly List<EntityComponent> components = new List<EntityComponent>();

        // Entity components --------------------------------------------------

        public void AddComponent(EntityComponent component)
        {
            foreach (var e in components)
            {
                if (e == component) return;
            }

            components.Add(component);
            component.Owner = this;

            components.Sort((a, b) => (0xff - a.Priority).CompareTo(0xff - b.Priority));
        }

        public T GetComponent<T>() where T : EntityComponent
        {
            foreach (var e in components)
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
            foreach (var e in components)
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

        // Entity get hurt by a other entity (ex: Zombie)
        public virtual void Hurt(Entity entity, int damages, Direction attackDirection)
        {
            if (!IsInvincible)
            {
                Health = Math.Max(0, Health - damages);

                if (Health == 0)
                {
                    Die();
                }
            }
        }

        // Entity get hurt by a tile (ex: lava)
        public virtual void Hurt(Tile tile, int damages, int tileX, int tileY)
        {
            if (!IsInvincible)
            {
                Health = Math.Max(0, Health - ComputeDamages(damages));

                if (Health == 0)
                {
                    Die();
                }
            }
        }


        // The mob is heal by a mod (healing itself)
        public virtual void Heal(Entity entity, int damages, Direction attackDirection)
        {
            Health = Math.Min(MaxHealth, Health + damages);
        }

        // The entity in heal b
        public virtual void Heal(Tile tile, int damages, int tileX, int tileY)
        {
            Health = Math.Min(MaxHealth, Health + damages);
        }

        public void Remove()
        {
            Removed = true;
        }

        public virtual void Die()
        {
            Remove();
        }

        public virtual void Interacte(Mob mob, Item item, Direction attackDirection)
        {
        }

        // Movement and colisions ---------------------------------------------

        public virtual void SetPosition(int x, int y)
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

        public bool IsColliding(int x, int y, int width1, int height1)
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
            foreach (var c in components)
            {
                c.Update(gameTime);
            }

            OnUpdate(gameTime);
        }

        public virtual void OnUpdate(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var c in components)
            {
                c.Draw(spriteBatch, gameTime);
            }

            OnDraw(spriteBatch, gameTime);
        }

        public virtual void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(new Rectangle(X, Y, Width, Height), Color.Red);
        }

        public TilePosition GetTilePosition()
        {
            return new TilePosition(X / ConstVal.TileSize, Y / ConstVal.TileSize);
        }
    }
}