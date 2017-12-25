using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WorldOfImagination.Game.Tiles;

namespace WorldOfImagination.Game.Entities
{

    public enum Direction { Up, Down, Left, Right }

    public class Entity
    {

        public EntityPosition Position = new EntityPosition(0,0);
        public int Width = 32;
        public int Height = 48 ;
        public Level Level;

        public bool Removed = true;
        public bool noclip = false;

        public void Init(Level level)
        {
            Level = level;
        }

        // Events
        public virtual void Hurt(Mob mob, int damages, Direction attackDirection)
        {

        }

        public virtual void Hurt(Tile tile, int damages, int tileX, int tileY)
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
            if (Position.X + accelerationX <= 0) accelerationX = 0;
            if (Position.Y + accelerationY <= 0) accelerationY = 0;

            for (int ox = -1; ox < 2; ox++)
            {
                for (int oy = -1; oy < 2; oy++)
                {
                    var t = new TilePosition(onTilePosition.X + ox, onTilePosition.Y + oy);

                    if (!Level.GetTile(t.X, t.Y).CanPass(Level, t, this) & !noclip)
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
    }
}
