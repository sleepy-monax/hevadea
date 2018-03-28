using Hevadea.Framework.Graphic.Particles;
using Hevadea.Game.Entities;
using Hevadea.Game.Tiles.Renderers;
using System.Collections.Generic;
using Hevadea.Framework;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Worlds
{
    public partial class Level
    {
        private GameManager _game;
        private World _world;
        private List<Entity>[,] _entitiesOnTiles;
        
        public TileConection[,] CachedTileConnection;
        public Texture2D Map { get; set; }
        
        public int Id { get; set; }
        public LevelProperties Properties { get; }
        public string Name { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        
        public List<Entity> Entities { get; set; }
        public int[] Tiles { get; set; }
        public Dictionary<string, object>[] TilesData { get; set; }
        
        public ParticleSystem ParticleSystem { get; }
        
        public Level(LevelProperties properties, int width, int height)
        {
            Properties = properties;
            Width = width;
            Height = height;
            ParticleSystem = new ParticleSystem();
            
            Tiles = new int[Width * Height];
            TilesData = new Dictionary<string, object>[Width * Height];
            Entities = new List<Entity>();
            _entitiesOnTiles = new List<Entity>[Width, Height];
            CachedTileConnection = new TileConection[Width,Height];
            
            Map = new Texture2D(Rise.MonoGame.GraphicsDevice, width, height);
            Map.Clear(Color.Transparent);
            
            for (var x = 0; x < Width; x++)
            for (var y = 0; y < Height; y++)
            {
                _entitiesOnTiles[x, y] = new List<Entity>();
                TilesData[x + y * Width] = new Dictionary<string, object>();
            }
        }
    }
}