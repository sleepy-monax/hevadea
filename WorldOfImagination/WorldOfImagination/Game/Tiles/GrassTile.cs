using Maker.Rise.Ressource;

namespace Maker.Hevadea.Game.Tiles
{
    public class GrassTile : Tile
    {
        public GrassTile(byte id) : base(id)
        {
            Sprite = new Sprite(Ressources.tile_tiles, 2);
        }
    }
}
