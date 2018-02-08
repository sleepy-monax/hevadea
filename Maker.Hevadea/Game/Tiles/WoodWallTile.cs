using Maker.Hevadea.Game.Entities;
using Maker.Rise.Ressource;

namespace Maker.Hevadea.Game.Tiles
{
    public class WoodWallTile : Tile
    {
        public WoodWallTile(byte id) : base(id)
        {
            Sprite = new Sprite(Ressources.TileTiles, 6);
        }

        public override bool IsBlocking(Entity e, TilePosition pos)
        {
            return true;
        }
    }
}