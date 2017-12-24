using Maker.Rise.GameComponent.Ressource;

namespace WorldOfImagination.Game.Tiles
{
    public class GrassTile : Tile
    {
        public GrassTile(byte id) : base(id)
        {
            Sprite = new Sprite(Ressources.tile_tiles, 2);
        }
    }
}
