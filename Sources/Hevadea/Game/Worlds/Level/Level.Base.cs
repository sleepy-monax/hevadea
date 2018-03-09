using Hevadea.Framework.Graphic.Particles;
using Hevadea.Game.Entities;
using System.Collections.Generic;

namespace Hevadea.Game.Worlds
{
    public partial class Level
    {
        private GameManager _game;
        private World _world;
        private int[] _tiles;
        private Dictionary<string, object>[] _tilesData;
        private List<Entity>[,] _entitiesOnTiles;
        
        public int Id { get; set; }
        public string Name { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public List<Entity> Entities;
        public LevelProperties Properties { get; }
        public ParticleSystem ParticleSystem { get; }

        public Level(LevelProperties properties, int width, int height)
        {
            Properties = properties;
            Width = width;
            Height = height;
            ParticleSystem = new ParticleSystem();
            
            _tiles = new int[Width * Height];
            _tilesData = new Dictionary<string, object>[Width * Height];
            Entities = new List<Entity>();
            _entitiesOnTiles = new List<Entity>[Width, Height];
            
            for (var x = 0; x < Width; x++)
            for (var y = 0; y < Height; y++)
            {
                _entitiesOnTiles[x, y] = new List<Entity>();
                _tilesData[x + y * Width] = new Dictionary<string, object>();
            }
        }
    }
}