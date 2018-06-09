using Hevadea.GameObjects.Tiles;
using Hevadea.GameObjects.Tiles.Components;
using Hevadea.Utils;

namespace Hevadea.GameObjects.Entities.Components.Ai.Actions
{
    public static class HelperAction
    {
        private static bool WalkablePredicat(Tile tile)
        {
            return !tile.HasTag<LiquideTile>() && !tile.HasTag<SolideTile>();
        }

        public static bool MoveTo(this Agent ag, TilePosition pos, float speed = 1f, bool usePathFinding = false, int maxDistance = 16)
        {
            if (usePathFinding)
            {
                if (ag.Owner.Level.GetPath(out var path, ag.Owner.GetTilePosition().AsNode(), pos.AsNode(), WalkablePredicat))
                {
                    foreach (var n in path)
                    {
                        ag.MoveTo(new TilePosition(n.X, n.Y), speed);
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                ag.ActionQueue.Enqueue(new ActionMoveToLocation(pos, speed));
            }

            return true;
        }
    }
}