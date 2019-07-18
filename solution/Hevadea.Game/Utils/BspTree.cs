using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Microsoft.Xna.Framework;
using System;

namespace Hevadea.Utils
{
    public class BspTreeNode
    {
        public bool HasChildrens => Item0 != null || Item1 != null;

        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }

        public BspTreeNode Parent { get; }
        public BspTreeNode Item0 { get; private set; }
        public BspTreeNode Item1 { get; private set; }

        public BspTreeNode(int x, int y, int width, int height, BspTreeNode parent = null)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Parent = parent;
        }

        public void SplitVecticaly(int where)
        {
            Split(where, true);
        }

        public void SplitHorizontaly(int where)
        {
            Split(where, false);
        }

        public void Split(int where, bool verticaly)
        {
            if (verticaly)
            {
                where = Mathf.Clamp(where, 0, Width);

                Item0 = new BspTreeNode(X, Y, where, Height, this);
                Item1 = new BspTreeNode(X + Item0.Width, Y, Width - Item0.Width, Height, this);
            }
            else
            {
                where = Mathf.Clamp(where, 0, Height);

                Item0 = new BspTreeNode(X, Y, Width, Height - where, this);
                Item1 = new BspTreeNode(X, Y + Item0.Height, Width, Height - Item0.Height, this);
            }
        }

        public void Clear()
        {
            Item0 = null;
            Item1 = null;
        }

        public Point GetCenter()
        {
            return new Point(X + Width / 2, Y + Height / 2);
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(X, Y, Width, Height);
        }
    }

    public class BspTree
    {
        public BspTreeNode Root { get; }

        public BspTree(int x, int y, int width, int height)
        {
            Root = new BspTreeNode(x, y, width, height);
        }

        public BspTree(int width, int height)
        {
            Root = new BspTreeNode(0, 0, width, height);
        }

        public static BspTree BuildBspTree(int x, int y, int width, int height, int depth, Random rnd = null)
        {
            var bsp = new BspTree(x, y, width, height);
            BuildLeaf(bsp.Root, depth, rnd);
            return bsp;
        }

        private static void BuildLeaf(BspTreeNode parent, int depth, Random rnd)
        {
            if (depth == 0) return;
            var gotoCount = 100;

            start:
            if (gotoCount == 0) return;
            gotoCount--;

            var vecticaly = rnd.Pick(true, false);
            var where = rnd.Next(vecticaly ? parent.Width / 4 : parent.Height / 4,
                vecticaly ? parent.Width - parent.Width / 4 : parent.Height - parent.Height / 4);

            parent.Split(where, vecticaly);

            if (vecticaly && (parent.Item0.Width / (float) parent.Item0.Height < 0.45
                              || parent.Item1.Width / (float) parent.Item1.Height < 0.45))
            {
                parent.Clear();
                goto start;
            }

            if (!vecticaly && (parent.Item0.Height / (float) parent.Item0.Width < 0.45
                               || parent.Item1.Height / (float) parent.Item1.Width < 0.45))
            {
                parent.Clear();
                goto start;
            }

            BuildLeaf(parent.Item0, depth - 1, rnd);
            BuildLeaf(parent.Item1, depth - 1, rnd);
        }
    }
}