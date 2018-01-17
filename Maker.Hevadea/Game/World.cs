using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.LevelGen;
using Maker.Hevadea.Scenes;
using Maker.Rise;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game
{
    public class World
    {
        public PlayerEntity Player;
        public Level[] Levels;
        private SpriteBatch spriteBatch;
        private BlendState lightBlend;

        private RenderTarget2D SceneRenderTaget;
        private RenderTarget2D lightRenderTaget;
        public Camera Camera;
        public GameScene Game;
        public int Time;

        public Level this[int index]
        {
            get { return Levels[index]; }
            set { Levels[index] = value; }
        }

        public World()
        {
            Levels = new Level[2];
            spriteBatch = Engine.Graphic.CreateSpriteBatch();
            Camera = new Camera();

            lightBlend = new BlendState()
            {
                ColorBlendFunction = BlendFunction.Add,
                ColorSourceBlend = Blend.DestinationColor,
                ColorDestinationBlend = Blend.Zero
            };

            SceneRenderTaget = Engine.Graphic.CreateRenderTarget();
            lightRenderTaget = Engine.Graphic.CreateRenderTarget();
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap, null, Engine.CommonRasterizerState,
                null, Camera.GetTransform());

            var state = Player.Level.GetRenderState(Camera);

            Player.Level.DrawTerrain(state, spriteBatch, gameTime);
            Player.Level.DrawEntities(state, spriteBatch, gameTime);
            Player.Level.DrawEntitiesOverlay(state, spriteBatch, gameTime);

            spriteBatch.End();
           
        }

        public void Update(GameTime gameTime)
        {
            foreach (var l in Levels)
            {
                l.Update(gameTime);
            }

            Time++;
        }

        // Static functions ---------------------------------------------------
        // Load, Save and generate a new World.

        public void Initialize(GameScene game)
        {
            Game = game;
            foreach (var l in Levels)
            {
                l.Initialize(this, game);
            }

            Camera.FocusEntity = Player;
        }

    }
}