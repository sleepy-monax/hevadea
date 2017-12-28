using Maker.Rise.Ressource;
using WorldOfImagination.Game.Entities;

namespace WorldOfImagination.Game.Tiles
{
    public class WoodWallTile : Tile
    {
        public WoodWallTile(byte id) : base(id)
        {
            Sprite = new Sprite(Ressources.tile_tiles, 6);
        }

        public override bool CanPass(Level level, TilePosition pos, Entity e)
        {
            return false;
        }
    }
}
