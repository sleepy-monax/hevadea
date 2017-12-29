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
        public EntityPosition Position = new EntityPosition(0,0);
        public int Width = 32;
        public int Height = 48 ;
        public Level Level;
        public World World;

        public bool IsLightSource = false;
        public int LightLevel = 32;
        public Color LightColor = Color.SpringGreen;
        public bool Removed = true;
        public bool NoClip = false;

        internal void Init(Level level, World world)
        {
            Level = level;
            World = world;
        }

        // Health macanic ---------------------------------------------------
        public int Health = 1;
        public int MaxHealth = 1;
        public bool Invincible = true;

        public virtual int ComputeDamages(int damages)
        {
            return damages;
        }

        // Entity get hurt by a other entity (ex: Zombie)
        public virtual void Hurt(Entity entity, int damages, Direction attackDirection)
        {
            if (!Invincible)
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
            if (!Invincible)
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

        public virtual void Die()
        {
            Removed = true;
            Level.RemoveEntity(this);
        }

        public virtual void Interacte(Mob mob, Item item)
        {

        }

        // Movement and colisions ---------------------------------------------

        public virtual bool Move(int accelerationX, int accelerationY)
        {
            if (accelerationX != 0 || accelerationY != 0)
            {
                if (MoveInternal(accelerationX, 0) | MoveInternal(0, accelerationY))
                {
                    var pos = Position.ToTilePosition();
                    Level.GetTile(pos.X, pos.Y).SteppedOn(this, pos);
                    return true;
                } 
            }


            return false;
        }

        protected bool MoveInternal(int accelerationX, int accelerationY)
        {

            // TODO: Check colisions...
            var onTilePosition = Position.ToTilePosition();

            if (Position.X + accelerationX + Width >= Level.W * ConstVal.TileSize) accelerationX = 0;
            if (Position.Y + accelerationY + Height >= Level.H * ConstVal.TileSize) accelerationY = 0;
            if (Position.X + accelerationX < 0) accelerationX = 0;
            if (Position.Y + accelerationY < 0) accelerationY = 0;

            for (int ox = -1; ox < 2; ox++)
            {
                for (int oy = -1; oy < 2; oy++)
                {
                    var t = new TilePosition(onTilePosition.X + ox, onTilePosition.Y + oy);

                    if (!Level.GetTile(t.X, t.Y).CanPass(this, t) & !NoClip)
                    {

                        if (Tile.Colide(t, new EntityPosition(Position.X, Position.Y + accelerationY), Width, Height))
                        {
                            accelerationY = 0;
                        }
                        
                        if (Tile.Colide(t, new EntityPosition(Position.X + accelerationX, Position.Y), Width, Height))
                        {
                            accelerationX = 0;
                        }

                        if (Tile.Colide(t, new EntityPosition(Position.X + accelerationX, Position.Y + accelerationY), Width, Height))
                        {
                            accelerationX = 0;
                            accelerationY = 0;
                        }
                    }
                }
            }
            if (accelerationX == 0 && accelerationY == 0)
            {
                return false;
            }
            
            Position.X += accelerationX;
            Position.Y += accelerationY;

            return true;
        }

        public virtual bool IsBlocking(Entity Entity)
        {
            return false;
        }

        public bool Colide(Entity e)
        {
            return Colide(e.Position, e.Width, e.Height);
        }

        public bool Colide(EntityPosition p, int width1, int height1)
        {
            return Position.X < p.X + width1 &&
                   Position.X + Width > p.X &&
                   Position.Y < p.Y + height1 &&
                   Height + Position.Y > p.Y;
        }

        // Update and Draw
        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(new Rectangle(Position.X, Position.Y, Width, Height), Color.Red);
        }

        internal Rectangle ToRectangle()
        {
            return new Rectangle(Position.X, Position.Y, Width, Height);
        }
    }
}
