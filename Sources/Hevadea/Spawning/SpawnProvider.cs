using System.Collections.Generic;
using Hevadea.Entities;
using Hevadea.Registry;
using Hevadea.Tiles;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.Spawning
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
