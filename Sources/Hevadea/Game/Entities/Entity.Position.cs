using Hevadea.Game.Tiles;
using Microsoft.Xna.Framework;

namespace Hevadea.Game.Entities
{
    public partial class Entity
    {
                
        public Point Size => new Point(Width, Height);
        public Vector2 Position => new Vector2(X, Y);
        public Rectangle Bound => new Rectangle(Position.ToPoint(), Size);
        
        public bool IsColliding(Rectangle rect)
        {
            return Bound.Contains(rect);
        }

        public bool IsColliding(float x, float y, int width, int height)
        {
            return Bound.Contains(new Rectangle((int)x, (int)y, width, height));
        }
        
        public TilePosition GetTilePosition(bool onOrigine = false)
        {
            if (onOrigine)
                return new TilePosition((int) ((X + Origin.X) / ConstVal.TileSize),
                    (int) ((Y + Origin.Y) / ConstVal.TileSize));

            return new TilePosition((int) (X / ConstVal.TileSize), (int) (Y / ConstVal.TileSize));
        }

        public Tile GetTileOnMyOrigin()
        {
            return Level.GetTile(GetTilePosition(true));
        }

        public TilePosition GetFacingTile()
        {
            var dir = Facing.ToPoint();
            var pos = GetTilePosition(true);
            
            return new TilePosition(dir.X + pos.X, dir.Y + pos.Y);
        }
    }
}