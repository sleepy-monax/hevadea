using Maker.Rise.Ressource;

namespace Maker.Hevadea.Game.Tiles
{
    public class WoodFloorTile : Tile
    {
        public WoodFloorTile(byte id) : base(id)
        {
            Sprite = new Sprite(Ressources.tile_tiles, 5);
        }
    }
}
