using Hevadea.Game.Registry;
using Hevadea.Game.Tiles;
using Hevadea.Game.Worlds;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Hevadea.Game.Spawning
{
    public class SpawnProvider
    {
        private EntityBlueprint _blueprint;

        public bool CanSpawnAnyWhere => CanSpawnOn.Count == 0;
        public bool CanSpawnAnyTime => CanSpawnAt.Count == 0;

        public List<Tile> CanSpawnOn { get; set; } = new List<Tile>();
        public List<DayStage> CanSpawnAt { get; set; } = new List<DayStage>();

        public SpawnProvider(EntityBlueprint entityBlueprint)
        {
            _blueprint = entityBlueprint;
        }

        public void TrySpawn(Level level, Rectangle area)
        {

        }
    }
}
