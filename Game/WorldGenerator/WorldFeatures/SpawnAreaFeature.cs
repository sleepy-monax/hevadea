using Hevadea.Registry;
using Hevadea.Worlds;
using Hevadea.Framework.Extension;
using Microsoft.Xna.Framework;

namespace Hevadea.WorldGenerator.WorldFeatures
{
    public class SpawnAreaFeature : WorldFeature
    {
        public const int SPAWN_AREA_SIZE = 3;
        public string SpawnLevelName { get; set; } = "overworld";

        public override void Apply(Generator gen, World world)
        {
            var center = new Point(gen.Size / 2, gen.Size / 2);
            world.PlayerSpawnLevel = SpawnLevelName;

            var spawnLevel = world.GetLevel(SpawnLevelName);
            spawnLevel.ClearEntitiesAt(center.X - SPAWN_AREA_SIZE / 2, center.Y - SPAWN_AREA_SIZE / 2, SPAWN_AREA_SIZE, SPAWN_AREA_SIZE);
            spawnLevel.FillRectangle(center.X - SPAWN_AREA_SIZE / 2, center.Y - SPAWN_AREA_SIZE / 2, SPAWN_AREA_SIZE, SPAWN_AREA_SIZE, TILES.GRASS);

            for (int i = 0; i < 25; i++)
            {
                var xx = center.X + (int)(SPAWN_AREA_SIZE * gen.Random.NextFloatRange(1f));
                var yy = center.Y + (int)(SPAWN_AREA_SIZE * gen.Random.NextFloatRange(1f));

                spawnLevel.SetTile(xx, yy, TILES.GRASS);
            }

            spawnLevel.AddEntityAt(ENTITIES.TREE, new Coordinates(center.X + gen.Random.Pick(-1, 1), center.Y + gen.Random.Pick(-1, 1)));
        }

        public override string GetName()
        {
            return "Spawn area";
        }

        public override float GetProgress()
        {
            return 1f;
        }
    }
}