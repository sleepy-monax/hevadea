using Hevadea.Tiles;
using Hevadea.Utils;
using Hevadea.Worlds;

namespace Hevadea.Items.Tags
{
    public class PlaceTile : PlacableItemTag
    {
        private Tile _tile;

        public PlaceTile(Tile tile)
        {
            _tile = tile;
        }

        public override void Place(Level level, Coordinates tile, Direction facing)
        {
            level.ClearTileDataAt(tile);
            level.SetTile(tile, _tile);
        }
    }
}