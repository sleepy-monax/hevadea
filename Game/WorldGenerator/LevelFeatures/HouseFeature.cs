using Hevadea.Registry;
using Hevadea.Tiles;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Hevadea.WorldGenerator.LevelFeatures
{
    public class HouseFeature : LevelFeature
    {
        public Tile Wall { get; set; } = TILES.WOOD_WALL;
        public Tile Floor { get; set; } = TILES.WOOD_FLOOR;
        public Point MinSize { get; set; } = new Point(4, 4);
        public Point MaxSize { get; set; } = new Point(7, 5);
        public List<Tile> CanBePlacedOn { get; set; } = new List<Tile>() {TILES.GRASS, TILES.ROCK};

        private float _progress = 0f;

        public override string GetName()
        {
            return "Adding houses";
        }

        public override float GetProgress()
        {
            return _progress;
        }

        public override void Apply(Generator gen, LevelGenerator levelGen, Level level)
        {
            for (var i = 0; i < 50; i++)
            {
                var houseX = gen.Random.Next(0, gen.Size);
                var houseY = gen.Random.Next(0, gen.Size);
                var houseWidth = gen.Random.Next(MinSize.X, MaxSize.X + 1);
                var houseHeight = gen.Random.Next(MinSize.Y, MaxSize.Y + 1);

                if (level.IsValid(houseX, houseY, houseWidth, houseHeight, true, CanBePlacedOn))
                {
                    level.FillRectangle(houseX, houseY, houseWidth, houseHeight, Floor);
                    level.Rectangle(houseX, houseY, houseWidth, houseHeight, Wall);
                }

                _progress = i / 50f;
            }
        }
    }
}