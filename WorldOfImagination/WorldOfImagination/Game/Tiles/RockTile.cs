using Maker.Rise.Ressource;
using Maker.Hevadea.Game.Entities;

namespace Maker.Hevadea.Game.Tiles
{
    public class RockTile : Tile
    {
        public RockTile(byte id) : base(id)
        {
            Sprite = new Sprite(Ressources.tile_tiles, 1);
        }

        public override bool CanPass(Level level, TilePosition pos, Entity e)
        {
            return false;
        }
    }
}
