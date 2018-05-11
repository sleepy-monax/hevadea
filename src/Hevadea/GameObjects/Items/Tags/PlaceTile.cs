using Hevadea.GameObjects.Tiles;
using Hevadea.Utils;
using Hevadea.Worlds.Level;

namespace Hevadea.GameObjects.Items.Tags
{
    public class PlaceTile : PlacableItemTag
    {
        private Tile _tile;
        
        public PlaceTile(Tile tile)
        {
            _tile = tile;
        }
        
        public override void Place(Level level, TilePosition tile, Direction facing)
        {
            level.SetTile(tile, _tile);
            level.ClearTileData(tile);
        }
    }
}