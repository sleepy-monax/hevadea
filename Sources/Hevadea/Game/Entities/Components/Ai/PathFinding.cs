using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Hevadea.Game.Registry;
using Hevadea.Game.Tiles;
using Hevadea.Game.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.Game.Entities.Components.Ai
{
    public class PathFinder
    {
        public class Node
        {
            public bool IsWalkable { get; }
            public int X { get; }
            public int Y { get; }
            public float Penalty { get; }

            public int gCost;
            public int hCost;
            public int fCost => gCost + hCost;
            public Node Parent;

            public Node(bool isWalkable, int x, int y) : this(isWalkable ? 1f: 0f, x, y){}
            
            public Node(float penalty, int x, int y)
            {
                IsWalkable = penalty != 0.0f;
                Penalty = penalty;
                X = x;
                Y = y;
            }

        }
        
        private Node[,] _nodes;
        private Level _level;
        private Entity _entity;
        
        public List<EntityBlueprint> IngnoredEntity { get; set; } = new List<EntityBlueprint>();
        public int MaxSearchDistance;
        
        public PathFinder(Level level, Entity entity, int maxSearch)
        {
            _nodes = new Node[level.Width,level.Height];
            _level = level;
            _entity = entity;
            MaxSearchDistance = maxSearch;
        }

        public List<Node> GetPath(TilePosition start, TilePosition end)
        {
            var startNode = GetNode(start.X, start.Y);
            var targetNode = GetNode(end.X, end.Y);

            var openSet = new List<Node>();
            var closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                var currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                    {
                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    return RetracePath(startNode, targetNode);
                }

                foreach (var neighbour in GetNeighbours(currentNode.X, currentNode.Y, start))
                {
                    if (!neighbour.IsWalkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + (int)Mathf.DistanceManhattan(currentNode.X, currentNode.Y, neighbour.X, neighbour.Y) * (int)(10.0f * neighbour.Penalty);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = (int)Mathf.DistanceManhattan(neighbour.X, neighbour.Y, targetNode.X, targetNode.Y);
                        neighbour.Parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }

            return null;
        }

        private Node GetNode(int tx, int ty)
        {
            if (_nodes[tx, ty] == null)
            {
                _nodes[tx, ty] = new Node(_level.GetTile(tx, ty).Tag<Tags.Solide>()?.CanPassThrought(_entity) ?? true, tx, ty);
            }

            return _nodes[tx, ty];
        }
        
        private List<Node> GetNeighbours(int tx, int ty, TilePosition start)
        {
            var neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if ((x == 0 && y == 0) )
                        continue;


                    int checkX = tx + x;
                    int checkY = ty + y;

                    if (checkX >= 0 && 
                        checkX < _level.Width &&
                        checkY >= 0 &&
                        checkY < _level.Height &&
                        Mathf.Distance(checkX, checkY, start.X, start.Y) < MaxSearchDistance)
                    {
                        neighbours.Add(GetNode(checkX, checkY));
                    }
                    
                }
            }

            return neighbours;
        }
        
                
        private static List<Node> RetracePath(Node startNode, Node endNode)
        {
            var path = new List<Node>();
            var currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            path.Reverse();
            return path;
        }
        
        public static void DrawPath(SpriteBatch sb, List<Node> path, Color color)
        {
            Node lastNode = null;
            foreach (var n in path)
            {
                if (lastNode != null)
                {
                    sb.DrawLine(lastNode.X * 16 + 8, lastNode.Y * 16 + 8, n.X * 16 + 8, n.Y * 16 + 8, color);
                }
                sb.FillRectangle(new Rectangle(n.X * 16 + 6, n.Y * 16 + 6, 4, 4), color);
                lastNode = n;
            }
        }
        
       
    }
}