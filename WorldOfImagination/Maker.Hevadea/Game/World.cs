using Maker.Rise;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.LevelGen;
using Maker.Hevadea.Game.LevelGen.Features.Overworld;
using Maker.Rise.Components;
using Maker.Rise.Utils;

namespace Maker.Hevadea.Game
{
    public class World
    {
        public Player Player;
        public Level[] Levels;
        private SpriteBatch spriteBatch;
        private SpriteBatch lightSpriteBatch;
        private BlendState lightBlend;

        private RenderTarget2D SceneRenderTaget;
        private RenderTarget2D lightRenderTaget;
        public Camera Camera;
        public int Time;

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

        public World()
        {
            Levels = new Level[1];
            spriteBatch = new SpriteBatch(Engine.Graphic.GraphicsDevice);
            lightSpriteBatch = new SpriteBatch(Engine.Graphic.GraphicsDevice);
            Camera = new Camera();

            lightBlend = new BlendState()
            {
                ColorBlendFunction = BlendFunction.Add,
                ColorSourceBlend = Blend.DestinationColor,
                ColorDestinationBlend = Blend.Zero
            };

            SceneRenderTaget = new RenderTarget2D(Engine.Graphic.GraphicsDevice, Engine.Graphic.GetWidth(), Engine.Graphic.GetHeight());
            lightRenderTaget = new RenderTarget2D(Engine.Graphic.GraphicsDevice, Engine.Graphic.GetWidth(), Engine.Graphic.GetHeight());
        }

        public void Draw(GameTime gameTime, bool showDebug = true, bool renderTiles = true, bool renderEntity = true)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap, null, Engine.CommonRasterizerState, null, Camera.GetTransform());
            lightSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.LinearWrap, null, Engine.CommonRasterizerState, null, Camera.GetTransform());
            lightSpriteBatch.FillRectangle(new Rectangle((int)Camera.X - Camera.GetWidth() / 2, (int)Camera.Y - Camera.GetHeight() / 2, Camera.GetWidth(), Camera.GetHeight()), Levels[Player.CurrentLevel].AmbiantLight);

            Levels[Player.CurrentLevel].Draw(spriteBatch, lightSpriteBatch, Camera, gameTime, showDebug, renderTiles, renderEntity);

            Engine.Graphic.GraphicsDevice.SetRenderTarget(SceneRenderTaget);
            spriteBatch.GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.End();

            Engine.Graphic.GraphicsDevice.SetRenderTarget(lightRenderTaget);
            spriteBatch.GraphicsDevice.Clear(Color.Transparent);
            lightSpriteBatch.End();

            Engine.Graphic.GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin();
            spriteBatch.Draw(SceneRenderTaget, Vector2.Zero, Color.White);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, lightBlend);
            spriteBatch.Draw(lightRenderTaget, Vector2.Zero, Color.White);
            spriteBatch.End();

        }

        public void Update(GameTime gameTime)
        {
            Levels[Player.CurrentLevel].Update(gameTime);
            Time++;
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

        public static World Generate(int seed)
        {
            World world = new World();

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

            world.Player = new Player()
            {
                //Position = new EntityPosition(0, 0)
                Position = new EntityPosition((world.Levels[0].W / 2) * ConstVal.TileSize, (world.Levels[0].H / 2) * ConstVal.TileSize)
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
