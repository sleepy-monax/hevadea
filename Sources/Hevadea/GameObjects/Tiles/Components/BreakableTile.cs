using Hevadea.Registry;
using Hevadea.Worlds;

namespace Hevadea.Tiles.Components
{
    public class BreakableTile : TileComponent
    {
        public Tile ReplacementTile { get; set; } = TILES.VOID;

        public void Break(TilePosition position, Level level)
        {
            level.SetTile(position, ReplacementTile);
            level.ClearTileData(position);
            AttachedTile.Tag<DroppableTile>()?.Drop(position, level);
        }
    }
}
