using Hevadea.GameObjects.Tiles;
using Hevadea.Utils;
using System.Collections.Generic;

namespace Hevadea.GameObjects.Entities.Components.Ai.Actions
{
    public static class HelperAction
    {
        public static bool MoveTo(this Agent ag, TilePosition pos, float speed = 1f, bool usePathFinding = false, int maxDistance = 16)
        {
            if (usePathFinding)
            {
                if (ag.Owner.Level.Path(out var path, ag.Owner.GetTilePosition(), pos))
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