using System.Collections.Generic;
using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Registry;
using Maker.Hevadea.Game.Tiles;

namespace Maker.Hevadea.WorldGenerator.WorldFeatures
{
    public class StairCaseFeature : WorldFeature
    {
        public override string GetName() => "staires";
        private float _progress = 0f;
        
        private LevelGenerator _from;
        private LevelGenerator _to;
        
        public List<Tile> CanbegeneratedOn { get; set; } = new List<Tile>();
        public int TryCount { get; set; } = 50; 
        
        public StairCaseFeature(LevelGenerator from, LevelGenerator to)
        {
            _from = from;
            _to = to;
        }
        
        public override float GetProgress()
        {
            return _progress;
        }

        public override void Apply(Generator gen, World world)
        {
            var from = world.GetLevel(_from.LevelName);
            var to = world.GetLevel(_to.LevelName);
            
            for (var i = 0; i < TryCount; i++)
            {
                var x = gen.Random.Next(0, gen.Size);
                var y = gen.Random.Next(0, gen.Size);

                from.FillRectangle(x, y, 3, 3, TILES.WOOD_FLOOR);
                from.Rectangle(x - 1, y - 1, 5, 5, TILES.WOOD_WALL);
                to.FillRectangle(x, y, 3, 3, TILES.WOOD_FLOOR);
                to.Rectangle(x - 1, y - 1, 5, 5, TILES.WOOD_WALL);
                
                from.SpawnEntity(new StairsEntity(false, to.Id), x + 1, y + 1);
                to.SpawnEntity(new StairsEntity(true, from.Id), x + 1, y + 1);
            }
        }

        public bool CheckSpace(Level level, int x, int y, int sx, int sy)
        {
            bool result = true;
            return result;
        }
    }
}