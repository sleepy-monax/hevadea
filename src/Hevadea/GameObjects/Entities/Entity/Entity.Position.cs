using Hevadea.GameObjects.Tiles;
using Hevadea.Utils;
using Microsoft.Xna.Framework;

namespace Hevadea.GameObjects.Entities
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
            return new TilePosition((int)(X / Game.Unit), (int)(Y / Game.Unit));
        }

        public TilePosition GetFacingTile()
        {
            var dir = Facing.ToPoint();
            var pos = GetTilePosition();

            return new TilePosition(dir.X + pos.X, dir.Y + pos.Y);
        }
    }
}