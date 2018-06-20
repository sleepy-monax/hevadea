using Hevadea.Registry;
using Hevadea.Worlds;

namespace Hevadea.Tiles.Components
{
    public class BreakableTile : TileComponent
    {
        public Tile ReplacementTile { get; set; } = TILES.VOID;

        public void Break(Coordinates position, Level level)
        {
            level.SetTile(position, ReplacementTile);
            level.ClearTileDataAt(position);
            AttachedTile.Tag<DroppableTile>()?.Drop(position, level);
        }
    }
}