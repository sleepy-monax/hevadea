using Maker.Rise.Ressource;

namespace Maker.Hevadea.Game.Tiles
{
    public class WaterTile : Tile
    {
        public WaterTile(byte id) : base(id)
        {
            Sprite = new Sprite(Ressources.tile_tiles, 4);
            //BackgroundDirt = false;
        }
    }
}