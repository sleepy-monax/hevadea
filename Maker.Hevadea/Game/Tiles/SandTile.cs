using Maker.Rise.Ressource;

namespace Maker.Hevadea.Game.Tiles
{
    public class SandTile : Tile
    {
        public SandTile(byte id) : base(id)
        {
            Sprite = new Sprite(Ressources.tile_tiles, 3);
        }
    }
}