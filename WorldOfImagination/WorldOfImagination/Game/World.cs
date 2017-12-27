using Maker.Rise;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WorldOfImagination.Game.Entities;
using WorldOfImagination.Game.LevelGen;
using WorldOfImagination.Game.LevelGen.Features.Overworld;

namespace WorldOfImagination.Game
{
    public class World
    {
        public Player Player;
        public Level[] Levels;
        private SpriteBatch spriteBatch;
        public Camera Camera;
        public RiseGame Game;

        public Level this[int index]
        {
            get
            {
                return Levels[index];
            }
            set
            {
                Levels[index] = value;
            }
        }

        public World(RiseGame game)
        {
            Game = game;
            Levels = new Level[1];
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            Camera = new Camera(Game);
        }

        public void Draw(GameTime gameTime, bool showDebug = true, bool renderTiles = true, bool renderEntity = true)
        {

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap, null, Game.RasterizerState, null, Camera.GetTransform());

            Levels[Player.CurrentLevel].Draw(spriteBatch, Camera, gameTime, showDebug, renderTiles, renderEntity);

            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            Levels[Player.CurrentLevel].Update(gameTime);
        }

        // Static functions ---------------------------------------------------
        // Load, Save and generate a new World.

        public void Initialize()
        {
            foreach (var l in Levels)
            {
                l.Initialize(this);
            }

            Camera.FocusEntity = Player;
        }

        public static World Generate(int seed, RiseGame Game)
        {
            World world = new World(Game);

            world[0] = new Generator
            {
                Seed = seed,
                Features =
                {
                    new OverworldBaseTerrain(),
                    new AbandonedHouseFeature(),
                    new TreeFeature()
                }

            }.Generate();

            world.Player = new Player(Game)
            {
                Position = new EntityPosition(
                    (world.Levels[0].W / 2) * ConstVal.TileSize,
                    (world.Levels[0].H / 2) * ConstVal.TileSize)
            };

            world[0].AddEntity(world.Player);

            return world;
        }

        public static void Save(World world, string saveFolder)
        {

        }

        public static World Load(string saveFolder, RiseGame Game)
        {
            return null;
        }
    }
}
