using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Game.Tiles;
using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.Hevadea.Game.Entities
{

    public enum Direction { Up = 0, Right = 1, Down = 2, Left = 3 }

    public class Entity
    {
        public Level Level;
        public World World;


        public int  X            { get; set; } = 0;
        public int  Y            { get; set; } = 0;
        public int  Width        { get; set; } = 32;
        public int  Height       { get; set; } = 48 ;

        public int  Health       { get; set; } = 1;
        public int  MaxHealth    { get; set; } = 1;

        public bool Removed      { get; set; } = true;
        public bool NoClip       { get; set; } = false;
        public bool IsInvincible { get; set; } = true;

        public Light Light       { get; set; } = new Light();

        public Point Size => new Point(Width, Height);
        public Point Position => new Point(X, Y);
        public Rectangle Bound => new Rectangle(Position, Size);
        

        internal void Initialize(Level level, World world)
        {
            Level = level;
            World = world;
        }

        // Health macanic ---------------------------------------------------

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

        public virtual bool Move(int accelerationX, int accelerationY)
        {
            if (accelerationX != 0 || accelerationY != 0)
            {
                if (MoveInternal(accelerationX, 0) | MoveInternal(0, accelerationY))
                {
                    var pos = GetTilePosition();
                    Level.GetTile(pos.X, pos.Y).SteppedOn(this, pos);
                    return true;
                } 
            }


            return false;
        }

        protected bool MoveInternal(int aX, int aY)
        {

            // TODO: Check colisions...
            var onTilePosition = GetTilePosition();

            if (X + aX + Width >= Level.Width * ConstVal.TileSize) aX = 0;
            if (Y + aY + Height >= Level.Height * ConstVal.TileSize) aY = 0;
            if (X + aX < 0) aX = 0;
            if (Y + aY < 0) aY = 0;

            for (int ox = -1; ox < 2; ox++)
            {
                for (int oy = -1; oy < 2; oy++)
                {
                    var t = new TilePosition(onTilePosition.X + ox, onTilePosition.Y + oy);

                    if (Level.GetTile(t.X, t.Y).IsBlocking(this, t) & !NoClip)
                    {

                        if (Tile.IsColiding(t, X, Y + aY, Width, Height))
                        {
                            aY = 0;
                        }

                        if (Tile.IsColiding(t, X + aX, Y, Width, Height))
                        {
                            aX = 0;
                        }

                        if (Tile.IsColiding(t, X + aX, Y + aY, Width, Height))
                        {
                            aX = 0;
                            aY = 0;
                        }
                    }

                    foreach (var e in Level.GetEntityOnTile(t.X, t.Y))
                    {
                        if (e != this && e.IsBlocking(this) && !NoClip)
                        {
                            if (e.IsColliding(X, Y + aY, Width, Height))
                            {
                                aY = 0;
                            }

                            if (e.IsColliding(X + aX, Y, Width, Height))
                            {
                                aX = 0;
                            }

                            if (e.IsColliding(X + aX, Y + aY, Width, Height))
                            {
                                aX = 0;
                                aY = 0;
                            }
                        }
                    }
                }
            }

            if (aX == 0 && aY == 0)
            {
                return false;
            }
            
            X += aX;
            Y += aY;

            return true;
        }

        public virtual bool IsBlocking(Entity Entity)
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
            return this.X < x + width1 &&
                   this.X + Width > x &&
                   this.Y < y + height1 &&
                   Height + this.Y > y;
        }

        // Update and Draw
        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(new Rectangle(X, Y, Width, Height), Color.Red);
        }

        public TilePosition GetTilePosition()
        {
            return new TilePosition((X / ConstVal.TileSize), (Y / ConstVal.TileSize));
        }
    }
}
