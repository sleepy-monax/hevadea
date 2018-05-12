using Hevadea.Framework.Graphic.Particles;

namespace Hevadea.Worlds
{
    public partial class Level
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public LevelProperties Properties { get; }

        public Chunk[,] Chunks;

        public ParticleSystem ParticleSystem { get; }
        public Minimap Minimap;

        private GameManager _game;
        private World _world;

        public Level(LevelProperties properties, int width, int height)
        {
            Properties = properties;
            Width = width;
            Height = height;
            ParticleSystem = new ParticleSystem();
            Minimap = new Minimap(this);

            Chunks = new Chunk[width / 16, height / 16];

            for (int x = 0; x < width / 16; x++)
            {
                for (int y = 0; y < height / 16; y++)
                {
                    Chunks[x, y] = new Chunk();
                }
            }
        }
    }
}