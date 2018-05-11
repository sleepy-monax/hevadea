using Hevadea.Framework.Graphic.Particles;
using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Tiles;
using Hevadea.GameObjects.Tiles.Renderers;
using Hevadea.Registry;
using Hevadea.Storage;
using System.Collections.Generic;

namespace Hevadea.Worlds.Level
{
    public partial class Level
    {
        private GameManager _game;
        private World _world;
        private List<Entity>[,] _entitiesOnTiles;

        public TileConection[,] CachedTileConnection;
        public Minimap Minimap;

        public int Id { get; set; }
        public LevelProperties Properties { get; }
        public string Name { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public List<Entity> Entities { get; set; }
        public Tile[] Tiles { get; set; }
        public Dictionary<string, object>[] TilesData { get; set; }

        public ParticleSystem ParticleSystem { get; }

        public Level(LevelStorage levelStorage) : this(LEVELS.GetProperties(levelStorage.Type), levelStorage.Width, levelStorage.Height)
        {
            Name = levelStorage.Name;
            Id = levelStorage.Id;
            TilesData = levelStorage.TilesData;
        }

        public Level(LevelProperties properties, int width, int height)
        {
            Properties = properties;
            Width = width;
            Height = height;
            ParticleSystem = new ParticleSystem();

            Tiles = new Tile[Width * Height];
            TilesData = new Dictionary<string, object>[Width * Height];
            Entities = new List<Entity>();
            _entitiesOnTiles = new List<Entity>[Width, Height];
            CachedTileConnection = new TileConection[Width, Height];

            Minimap = new Minimap(this);

            for (var x = 0; x < Width; x++)
                for (var y = 0; y < Height; y++)
                {
                    _entitiesOnTiles[x, y] = new List<Entity>();
                    TilesData[x + y * Width] = new Dictionary<string, object>();
                }
        }
    }
}