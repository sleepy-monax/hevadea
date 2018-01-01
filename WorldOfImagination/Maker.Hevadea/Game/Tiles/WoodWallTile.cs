using Maker.Rise.Ressource;
using Maker.Hevadea.Game.Entities;

namespace Maker.Hevadea.Game.Tiles
{
    public class WoodWallTile : Tile
    {
        public WoodWallTile(byte id) : base(id)
        {
            Sprite = new Sprite(Ressources.tile_tiles, 6);
        }

        public override bool IsBlocking(Entity e, TilePosition pos)
        {
            return true;
        }
    }
}
