﻿using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using WorldOfImagination.Game.Tiles;

namespace WorldOfImagination.Game.Entities
{

    public enum Direction { Up, Down, Left, Right }

    public class Entity
    {

        public EntityPosition Position = new EntityPosition(0,0);
        public int Width = 32;
        public int Height = 48 ;
        private Level Level;
        private World World;

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

        public virtual void Hurt(Mob mob, int damages, Direction attackDirection)
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

        public virtual void Hurt(Tile tile, int damages, int tileX, int tileY)
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

        public virtual void Heal(Mob mob, int damages, Direction attackDirection)
        {
            Health = Math.Min(MaxHealth, Health + damages);
        }

        public virtual void Heal(Tile tile, int damages, int tileX, int tileY)
        {
            Health = Math.Min(MaxHealth, Health + damages);
        }

        public virtual void Die()
        {

        }



        // Movement and colisions ---------------------------------------------

        public virtual bool Move(int accelerationX, int accelerationY)
        {
            if (accelerationX != 0 || accelerationY != 0)
            {
                var stopped = true;
                if (MoveInternal(accelerationX, accelerationY)) stopped = false;

                if (!stopped)
                {
                    var pos = Position.ToTilePosition();
                    Level.GetTile(pos.X, pos.Y).SteppedOn(Level, pos, this);
                }

                return !stopped;
            }


            return true;
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

                    if (!Level.GetTile(t.X, t.Y).CanPass(Level, t, this) & !NoClip)
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