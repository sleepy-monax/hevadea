using System.Collections.Generic;
using Hevadea.GameObjects;
using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Tiles;
using Hevadea.Worlds;

namespace Hevadea.WorldGenerator.WorldFeatures
{
    public class StairCaseFeature : WorldFeature
    {
		float _progress = 0f;
		LevelGenerator _from;
		LevelGenerator _to;
		      
        public List<Tile> CanbegeneratedOn { get; set; } = new List<Tile>();
        public int StairesCount { get; set; } = 5; 
        
        public StairCaseFeature(LevelGenerator from, LevelGenerator to)
        {
            _from = from;
            _to = to;
        }
        
		public override string GetName() => "staires";
        public override float GetProgress()
        {
            return _progress;
        }

        public override void Apply(Generator gen, World world)
        {
            var from = world.GetLevel(_from.LevelName);
            var to = world.GetLevel(_to.LevelName);
			int staireCount = 0;
            
			while (staireCount < StairesCount)
			{
				var x = gen.Random.Next(0, gen.Size);
                var y = gen.Random.Next(0, gen.Size);

                if (from.IsValid(x, y, 5, 5, true, new List<Tile> { TILES.ROCK })
                   && to.IsValid(x, y, 5, 5, true, new List<Tile> { TILES.ROCK, TILES.DIRT }))
                {
                    from.FillRectangle(x + 1, y + 1, 3, 3, TILES.DIRT);
                    to.FillRectangle(x + 1, y + 1, 3, 3, TILES.DIRT);

                    var downStaire = (EntityStairs)from.SpawnEntity(EntityFactory.STAIRES, x + 2, y + 2);
                    downStaire.GoUp = false;
                    downStaire.Destination = to.Id;

                    var upStaire = (EntityStairs)to.SpawnEntity(EntityFactory.STAIRES, x + 2, y + 2);
                    upStaire.GoUp = true;
                    upStaire.Destination = from.Id;

					staireCount++;
                }
			}
        }
    }
}