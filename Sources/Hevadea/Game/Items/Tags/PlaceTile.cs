using Hevadea.Game.Tiles;

namespace Hevadea.Game.Items.Tags
{
    public class PlaceTile : PlacableItemTag
    {
        private Tile _tile;
        
        public PlaceTile(Tile tile)
        {
            _tile = tile;
        }
        
        public override void Place(Level level, TilePosition tile)
        {
            level.SetTile(tile, _tile);
        }
    }
}