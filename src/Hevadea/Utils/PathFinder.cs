using Hevadea.Framework.Utils;
using Hevadea.GameObjects.Tiles;
using Hevadea.Worlds;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Utils
{
    public static class PathFinder
    {
        public class Node
        {
            public List<Node> Neighbour { get; set; }

            public int X { get; }
            public int Y { get; }

            public bool Blocked { get; }
            public bool Visited { get; set; } = false;

            public float GlobalGoal { get; set; } = float.MaxValue;
            public float LocalGoal { get; set; } = float.MaxValue;
            public Node Parent { get; set; }


            public Node(int x, int y)
            {
                X = x;
                Y = y;
            }

            public float DistanceTo(Node node)
            {
                return Mathf.Distance(X, Y, node.X, node.Y);
            }
        }


        public static bool Path(this Level level, out List<Node> result, TilePosition start, TilePosition end, int openSetThreshold = 1000)
        {
            result = new List<Node>();

            Node[,] nodes = new Node[level.Width, level.Height];

            Node startNode = new Node(start.X, start.Y);
            Node endNode = new Node(end.X, end.Y);
            Node currentNode = startNode;

            nodes[start.X, start.Y] = startNode;
            nodes[end.X, end.Y] = endNode;

            startNode.LocalGoal = 0.0f;
            startNode.GlobalGoal = startNode.DistanceTo(endNode);

            List<Node> openSet = new List<Node>();
            openSet.Add(startNode);

            Node GetNode(int x, int y)
            {
                var n = nodes[x, y];

                if (n == null)
                {
                    n = new Node(x, y);
                    nodes[x, y] = n;
                }

                return n;
            }

            List<Node> GetNeighbour(Node node)
            {
                if (node.Neighbour != null) return node.Neighbour;

                List<Node> r = new List<Node>();

                if (node.X > 0)
                    r.Add(GetNode(node.X - 1, node.Y));

                if (node.X < level.Width - 1)
                    r.Add(GetNode(node.X + 1, node.Y));

                if (node.Y > 0)
                    r.Add(GetNode(node.X, node.Y - 1));

                if (node.Y < level.Height - 1)
                    r.Add(GetNode(node.X, node.Y + 1));

                node.Neighbour = r;
                return r;
            }

            while (openSet.Count != 0 && currentNode != endNode)
            {
                openSet.Sort((a, b) => a.GlobalGoal.CompareTo(b.GlobalGoal));

                while (openSet.Count != 0 && openSet.First().Visited)
                {
                    openSet.Pop();
                }

                if (openSet.Count == 0 || openSet.Count > openSetThreshold) return false;

                currentNode = openSet.First();
                currentNode.Visited = true;

                foreach (var n in GetNeighbour(currentNode))
                {
                    if (!n.Visited && !n.Blocked) openSet.Add(n);

                    float localGoal = currentNode.LocalGoal + currentNode.DistanceTo(n);

                    if (localGoal < n.LocalGoal)
                    {
                        n.Parent = currentNode;
                        n.LocalGoal = localGoal;
                        n.GlobalGoal = n.LocalGoal + n.DistanceTo(endNode);
                    }

                }
            }

            while (currentNode.Parent != null)
            {
                result.Push(currentNode);
                currentNode = currentNode.Parent;
            }

            return true;
        }
    }
}
