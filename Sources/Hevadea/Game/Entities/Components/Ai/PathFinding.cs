using System.Collections.Generic;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Hevadea.Game.Tiles;
using Hevadea.Game.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities.Components.Ai
{
    public class PathFinder
    {
        public class Node
        {
            public bool IsWalkable;
            public int X;
            public int Y;
            public float Penalty;

            // calculated values while finding path
            public int gCost;
            public int hCost;
            public Node parent;

            // create the node
            // _price - how much does it cost to pass this tile. less is better, but 0.0f is for non-walkable.
            // _gridX, _gridY - tile location in grid.
            public Node(bool isWalkable, int x, int y) : this(isWalkable ? 1f: 0f, x, y){}
            
            public Node(float price, int x, int y)
            {
                IsWalkable = price != 0.0f;
                Penalty = price;
                X = x;
                Y = y;
            }

            public int fCost
            {
                get
                {
                    return gCost + hCost;
                }
            }
        }
        
        private Node[,] _cache;
        private Level _level;
        
        public PathFinder(Level level)
        {
            _cache = new Node[level.Width,level.Height];
            _level = level;
        }
        
        private Node GetNode(int tx, int ty)
        {
            if (_cache[tx, ty] == null)
            {
                _cache[tx, ty] = new Node(!(_level.GetTile(tx, ty).HasTag<Tags.Solide>() || _level.GetEntityOnTile(tx, ty).Count > 0), tx, ty);
            }

            return _cache[tx, ty];
        }
        
        private List<Node> GetNeighbours(int tx, int ty)
        {
            var neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = tx + x;
                    int checkY = ty + y;

                    if (checkX >= 0 && checkX < _level.Width && checkY >= 0 && checkY < _level.Height)
                    {
                        neighbours.Add(GetNode( checkX, checkY));
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
                currentNode = currentNode.parent;
            }
            path.Reverse();
            return path;
        }
        
        private static int GetDistance(Node nodeA, Node nodeB)
        {
            return Mathf.Abs(nodeA.X - nodeB.X) + Mathf.Abs(nodeA.Y - nodeB.Y);
        }

        public static void DrawPath(SpriteBatch sb, List<Node> path, Color color)
        {
            foreach (var n in path)
            {
                sb.FillRectangle(new Rectangle(n.X * 16 + 4, n.Y * 16 + 4, 8, 8), color);
            }
        }
        
        public List<Node> PathFinding(TilePosition start, TilePosition end)
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

                foreach (var neighbour in GetNeighbours(currentNode.X, currentNode.Y))
                {
                    if (!neighbour.IsWalkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour) * (int)(10.0f * neighbour.Penalty);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }

            return null;
        }
    }
}