using Hevadea.Framework.UI;
using Hevadea.Registry;
using Hevadea.Tiles;
using Hevadea.Utils;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using System;
using Hevadea.Framework;

namespace Hevadea.WorldGenerator.LevelFeatures
{
    public class BspDecorator : LevelFeature
    {
        public Spacing Padding { get; set; } = new Spacing(4);
        public int Depth { get; set; } = 3;

        public override string GetName()
        {
            return "Generating bsp";
        }

        public override float GetProgress()
        {
            return 0;
        }

        public bool GenerateWall { get; set; } = false;
        public bool GenerateFloor { get; set; } = false;
        public bool GeneratePath { get; set; } = false;

        public Tile Wall { get; set; } = TILES.ROCK;
        public Tile Floor { get; set; } = TILES.DIRT;

        public override void Apply(Generator gen, LevelGenerator levelGen, Level level)
        {
            var rnd = new Random(gen.Seed);
            var bound = Padding.Apply(new Rectangle(0, 0, level.Width, level.Height));
            var tree = BspTree.BuildBspTree((int)Padding.Left, (int)Padding.Top,
                level.Width - (int)Padding.Left - (int)Padding.Right,
                level.Height - (int)Padding.Top - (int)Padding.Bottom,
                Depth, rnd);

            Build(tree.Root, rnd, level);
        }

        private void Build(BspTreeNode node, Random rnd, Level level)
        {
            if (node.HasChildrens)
            {
                var c0 = node.Item0.GetCenter();
                var c1 = node.Item1.GetCenter();

                Build(node.Item0, rnd, level);
                Build(node.Item1, rnd, level);

                if (GeneratePath)
                    level.PlotLine(c0.X, c0.Y, c1.X, c1.Y, Floor);
            }
            else
            {
                if (GenerateFloor)
                    level.FillRectangle(node.X, node.Y, node.Width, node.Height, Floor);

                if (GenerateWall)
                    level.Rectangle(node.X, node.Y, node.Width, node.Height, Wall);
            }
        }
    }
}