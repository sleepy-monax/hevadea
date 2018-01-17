using Maker.Hevadea.Enum;
using Maker.Hevadea.Game.Entities;
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
        private RenderTarget2D lightRT;
        private RenderTarget2D worldRT;
        private BlendState lightBlend = new BlendState() { ColorBlendFunction = BlendFunction.Add, ColorSourceBlend = Blend.DestinationColor, ColorDestinationBlend = Blend.Zero };

        public Camera Camera;
        public GameScene Game;
        public int Time;

        public Level this[Levels l]
        {
            get { return Levels[(int) l]; }
            set { Levels[(int) l] = value; }
        }

        public Level this[int index]
        {
            get { return Levels[index]; }
            set { Levels[index] = value; }
        }

        public World()
        {
            Levels = new Level[2];
            spriteBatch = Engine.Graphic.CreateSpriteBatch();
            lightRT = Engine.Graphic.CreateRenderTarget();
            worldRT = Engine.Graphic.CreateRenderTarget();
            Camera = new Camera();
        }

        public void Draw(GameTime gameTime)
        {

            var state = Player.Level.GetRenderState(Camera);

            Engine.Graphic.SetRenderTarget(worldRT);

            Engine.Graphic.Begin(spriteBatch, false, Camera.GetTransform());
            Player.Level.DrawTerrain(state, spriteBatch, gameTime);
            Player.Level.DrawEntities(state, spriteBatch, gameTime);
            Player.Level.DrawEntitiesOverlay(state, spriteBatch, gameTime);
            spriteBatch.End();

            Engine.Graphic.SetRenderTarget(lightRT);

            //Engine.Graphic.GetGraphicsDevice().Clear(Color.Blue * 0.1f);
            Engine.Graphic.GetGraphicsDevice().Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.LinearClamp, null, null, null, Camera.GetTransform());
            Player.Level.DrawLightMap(state, spriteBatch, gameTime);
            spriteBatch.End();

            Engine.Graphic.SetRenderTarget(null);

            Engine.Graphic.Begin(spriteBatch);

            spriteBatch.Draw(worldRT, Engine.Graphic.GetResolutionRect(), Color.White);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, lightBlend);

            spriteBatch.Draw(lightRT, Engine.Graphic.GetResolutionRect(), Color.White);

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