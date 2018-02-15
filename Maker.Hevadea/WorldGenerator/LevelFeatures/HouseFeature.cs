using System;
using System.Collections.Generic;
using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Registry;
using Maker.Hevadea.Game.Tiles;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.WorldGenerator.LevelFeatures
{
    public class HouseFeature : LevelFeature
    {
        public Tile Wall     { get; set; } = TILES.WOOD_WALL;
        public Tile Floor    { get; set; } = TILES.WOOD_FLOOR;
        public Point MinSize { get; set; } = new Point(4, 4);
        public Point MaxSize { get; set; } = new Point(7, 5);
        public List<Tile> CanBePlacedOn { get; set; } = new List<Tile>(){ TILES.GRASS, TILES.ROCK };
        
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
                var px = gen.Random.Next(0, gen.Size);
                var py = gen.Random.Next(0, gen.Size);
                var sx = gen.Random.Next(MinSize.X, MaxSize.X + 1);
                var sy = gen.Random.Next(MinSize.Y, MaxSize.Y + 1);

                var canBePlaced = true;
                
                for (var x = px; x < px + sx; x++)
                {
                    for (var y = py; y < py + sy; y++)
                    {
                        canBePlaced &= CanBePlacedOn.Contains(level.GetTile(x, y));
                    }   
                }

                if ( canBePlaced )
                {
                    Console.WriteLine($"House generated {px}:{py}");
                    for (var x = 0; x < sx; x++)
                    {
                        for (var y = 0; y < sy; y++)
                        {
                            if (x == 0 || y == 0 || x == sx - 1 || y == sy - 1)
                                level.SetTile(px + x, py + y, Wall);
                            else
                                level.SetTile(px + x, py + y, Floor);
                        }   
                    }  
                }

                _progress = i / 50f;
            }
        }
    }
}