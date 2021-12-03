using Hevadea.Tiles;
using Hevadea.Tiles.Components;
using Hevadea.Utils;

namespace Hevadea.Entities.Components.AI.Actions
{
    public static class HelperAction
    {
        private static bool WalkablePredicat(Tile tile)
        {
            return !tile.HasTag<LiquideTile>() && !tile.HasTag<SolideTile>();
        }

        public static bool MoveTo(this Agent ag, Coordinates coords, float speed = 1f, bool usePathFinding = false,
            int maxDistance = 16)
        {
            if (usePathFinding)
            {
                if (ag.Owner.Level.GetPath(out var path, ag.Owner.Coordinates.AsNode(), coords.AsNode(),
                    WalkablePredicat))
                    foreach (var n in path)
                        ag.MoveTo(new Coordinates(n.X, n.Y), speed);
                else
                    return false;
            }
            else
            {
                ag.ActionQueue.Enqueue(new ActionMoveToLocation(coords, speed));
            }

            return true;
        }
    }
}