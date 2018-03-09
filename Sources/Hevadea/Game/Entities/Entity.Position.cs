using Hevadea.Game.Tiles;
using Microsoft.Xna.Framework;

namespace Hevadea.Game.Entities
{
    public partial class Entity
    {
        public Vector2 Position => new Vector2(X, Y);


        public Tile GetTileOnMyPosition()
        {
            return Level.GetTile(GetTilePosition());
        }

        public TilePosition GetTilePosition()
        {
            return new TilePosition((int) (X / ConstVal.TileSize), (int) (Y / ConstVal.TileSize));
        }

        public TilePosition GetFacingTile()
        {
            var dir = Facing.ToPoint();
            var pos = GetTilePosition();
            
            return new TilePosition(dir.X + pos.X, dir.Y + pos.Y);
        }
    }
}