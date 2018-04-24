using System.Collections.Generic;
using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Entities.Blueprints;
using Hevadea.GameObjects.Tiles;
using Hevadea.Registry;
using Hevadea.Worlds;
using Hevadea.Worlds.Level;
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
