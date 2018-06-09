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

        public Coordinates GetTilePosition()
        {
            return new Coordinates((int)(X / Game.Unit), (int)(Y / Game.Unit));
        }

        public Coordinates GetFacingTile()
        {
            var dir = Facing.ToPoint();
            var pos = GetTilePosition();

            return new Coordinates(dir.X + pos.X, dir.Y + pos.Y);
        }
    }
}