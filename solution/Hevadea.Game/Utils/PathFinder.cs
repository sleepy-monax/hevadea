using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Tiles;
using Hevadea.Worlds;
using System;
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

            public bool Walkable { get; set; } = true;
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

        private static Node GetNode(this Node[,] nodes, Level level, int x, int y, Predicate<Tile> walkable)
        {
            var n = nodes[x, y];

            if (n == null)
            {
                n = new Node(x, y);
                n.Walkable = walkable(level.GetTile(x, y));
                nodes[x, y] = n;
            }

            return n;
        }

        public static List<Node> GetNeighbour(this Node[,] nodes, Level level, Node node, Predicate<Tile> walkable)
        {
            if (node.Neighbour != null) return node.Neighbour;

            var r = new List<Node>();

            if (node.X > 0)
                r.Add(nodes.GetNode(level, node.X - 1, node.Y, walkable));

            if (node.X < level.Width - 1)
                r.Add(nodes.GetNode(level, node.X + 1, node.Y, walkable));

            if (node.Y > 0)
                r.Add(nodes.GetNode(level, node.X, node.Y - 1, walkable));

            if (node.Y < level.Height - 1)
                r.Add(nodes.GetNode(level, node.X, node.Y + 1, walkable));

            node.Neighbour = r;
            return r;
        }

        public static bool GetPath(this Level level, out List<Node> result, Node startNode, Node endNode,
            Predicate<Tile> walkable, int openSetThreshold = 1000)
        {
            result = new List<Node>();

            var nodes = new Node[level.Width, level.Height];

            var currentNode = startNode;

            nodes[startNode.X, startNode.Y] = startNode;
            nodes[endNode.X, endNode.Y] = endNode;

            startNode.LocalGoal = 0.0f;
            startNode.GlobalGoal = startNode.DistanceTo(endNode);

            var openSet = new List<Node>();
            openSet.Add(startNode);

            while (openSet.Count != 0 && currentNode != endNode)
            {
                openSet.Sort((a, b) => a.GlobalGoal.CompareTo(b.GlobalGoal));

                while (openSet.Count != 0 && openSet.First().Visited) openSet.Pop();

                if (openSet.Count == 0 || openSet.Count > openSetThreshold) return false;

                currentNode = openSet.First();
                currentNode.Visited = true;

                foreach (var n in nodes.GetNeighbour(level, currentNode, walkable))
                {
                    if (!n.Visited && n.Walkable) openSet.Add(n);

                    var localGoal = currentNode.LocalGoal + currentNode.DistanceTo(n);

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