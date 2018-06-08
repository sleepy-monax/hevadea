using Hevadea.GameObjects;
using Hevadea.WorldGenerator;
using Hevadea.WorldGenerator.LevelFeatures;
using Hevadea.WorldGenerator.WorldFeatures;

namespace Hevadea.Registry
{
    public static class GENERATOR
    {
        public static Generator DEFAULT;
        public static LevelGenerator OVERWORLD;
        public static LevelGenerator CAVE;

        public static void Initialize()
        {
            OVERWORLD = new LevelGenerator
            {
                Id = 0,
                Name = "overworld",
                Properties = LEVELS.SURFACE,
                Features =
                {
                    new Terrain
                    {
                        Layers =
                        {
                            new TerrainLayer
                            {
                                Priority = 0,
                                Tile = TILES.WATER,
                                Threashold = 0f,
                                Function = Functions.Flat(0f)
                            },
                            new TerrainLayer
                            {
                                Priority = 1,
                                Tile = TILES.GRASS,
                                Threashold = 1f,
                                Function = Functions.Island()
                            },
                            new TerrainLayer
                            {
                                Priority = 2,
                                Tile = TILES.SAND,
                                Threashold = 0.8f,
                                Function = Functions.Combine(Functions.Perlin(2, 0.5, 30), Functions.Island()),
                                TileRequired = {TILES.WATER}
                            },
                            new TerrainLayer
                            {
                                Priority = 3,
                                Tile = TILES.GRASS,
                                Threashold = 0.95f,
                                Function = Functions.Island(),
                                TileRequired = {TILES.WATER}
                            },
                            new TerrainLayer
                            {
                                Priority = 4,
                                Tile = TILES.ROCK,
                                Threashold = 1f,
                                Function = Functions.Combine(Functions.Perlin(2, 0.5, 15), Functions.Island()),
                                TileRequired = {TILES.GRASS}
                            }
                        }
                    },
                    new HouseFeature(),
                    new CompoundFeature("Adding animals...")
                    {
                        Content =
                        {
                            new PopulateFeature(EntityFactory.FISH)
                            {
                                Chance = 25,
                                CanBePlantOn = {TILES.WATER},
                                PlacingFunction = Functions.Perlin(1, 0.5, 10),
                                Threashold = 0.5f,
                            },
                            new PopulateFeature(EntityFactory.CHIKEN)
                            {
                                Chance = 100,
                                CanBePlantOn = {TILES.GRASS},
                                PlacingFunction = Functions.Perlin(1, 0.5, 10),
                                Threashold = 0.7f,
                            },
                        }
                    },
                    new CompoundFeature("Adding plants...")
                    {
                        Content =
                        {
                            new PopulateFeature(EntityFactory.TREE)
                            {
                                Chance = 3,
                                CanBePlantOn = {TILES.GRASS},
                                PlacingFunction = Functions.Perlin(2, 0.5, 15),
                                Threashold = 0.7f,
                            },
                            new PopulateFeature(EntityFactory.FLOWER)
                            {
                                Chance = 10,
                                CanBePlantOn = {TILES.GRASS},
                                PlacingFunction = Functions.Perlin(1,1,5),
                                Threashold = 0.5f
                            },
                            new PopulateFeature(EntityFactory.GRASS)
                            {
                                Chance = 3,
                                CanBePlantOn = {TILES.GRASS},
                                PlacingFunction = Functions.Perlin(1,1,5),
                                Threashold = 0.5f
                            }
                        }
                    }
                }
            };

            CAVE = new LevelGenerator
            {
                Id = 1,
                Name = "cave",
                Properties = LEVELS.UNDERGROUND,
                Features =
                {
                    new Terrain
                    {
                        Layers =
                        {
                            new TerrainLayer
                            {
                                Priority = 0,
                                Tile = TILES.ROCK,
                                Threashold = 0f,
                                Function = Functions.Flat(0f)
                            },
                            new TerrainLayer
                            {
                                Priority = 1,
                                Tile = TILES.DIRT,
                                Threashold = 1.1f,
                                Function = Functions.Perlin(2, 1, 30)
                            },
                            new TerrainLayer
                            {
                                Priority = 1,
                                Tile = TILES.WATER,
                                Threashold = 1.6f,
                                TileRequired = { TILES.DIRT },
                                Function = Functions.Perlin(3, 1, 20, 679d)
                            },
                            new TerrainLayer
                            {
                                Priority = 2,
                                Tile = TILES.IRON_ORE,
                                Threashold = 1.2f,
                                TileRequired = { TILES.ROCK},
                                Function = Functions.Perlin(2, 1, 3),
                            }
                        }
                    },
                    new BspDecorator
                    {
                        GenerateFloor = false,
                        GenerateWall = false,
                        GeneratePath = true,
                        Depth = 7,
                    },
                    new HouseFeature
                    {
                        CanBePlacedOn = { TILES.DIRT, TILES.ROCK },
                        Wall = TILES.ROCK
                    },
                    new PopulateFeature(EntityFactory.ZOMBIE)
                    {
                        Chance = 400,
                        CanBePlantOn = {TILES.DIRT},
                        PlacingFunction = Functions.Perlin(1, 0.5, 10),
                        Threashold = 0.7f,
                    },
                }
            };

            DEFAULT = new Generator
            {
                Size = 256,
                Seed = 0,
                LevelsGenerators = { OVERWORLD, CAVE },
                WorldFeatures = { new StairCaseFeature(OVERWORLD, CAVE), new SpawnAreaFeature() }
            };
        }
    }
}