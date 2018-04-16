using System.Collections.Generic;
using Hevadea.GameObjects.Tiles;
using Hevadea.Utils;

namespace Hevadea.GameObjects.Entities.Components.Ai.Actions
{
    public static class HelperAction
    {
        public static bool MoveTo(this Agent ag, TilePosition pos, float speed = 1f, bool usePathFinding = false, int maxDistance = 16)
        {
            if (usePathFinding)
            {
                List<PathFinder.Node> path;
                path = new PathFinder(ag.Owner.Level, ag.Owner, maxDistance).GetPath(ag.Owner.GetTilePosition(), pos);

                if (path == null)
                {
                    return false;
                }
                
                foreach (var n in path)
                {
                    ag.MoveTo(new TilePosition(n.X, n.Y), speed);
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