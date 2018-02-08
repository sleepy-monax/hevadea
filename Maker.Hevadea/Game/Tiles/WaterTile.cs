using Maker.Hevadea.Game.Entities;
using Maker.Rise.Ressource;

namespace Maker.Hevadea.Game.Tiles
{
    public class WaterTile : Tile
    {
        public WaterTile(byte id) : base(id)
        {
            Sprite = new Sprite(Ressources.TileTiles, 4);
            //BackgroundDirt = false;
        }

        public override bool IsBlocking(Entity e, TilePosition pos)
        {
            return true;
        }
    }
}