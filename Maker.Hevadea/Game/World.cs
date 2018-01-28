using Maker.Hevadea.Game.Entities;
using Maker.Rise;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Maker.Hevadea.Game
{
    public class World
    {
        public GameManager Game;
        public int Time;
        public List<Level> Levels = new List<Level>();
        public string PlayerSpawnLevel = "overworld";

        private SpriteBatch spriteBatch;
        private RenderTarget2D lightRT;
        private RenderTarget2D worldRT;
        private RenderTarget2D finalRT;
        private BlendState lightBlend = new BlendState() { ColorBlendFunction = BlendFunction.Add, ColorSourceBlend = Blend.DestinationColor, ColorDestinationBlend = Blend.Zero };

        public World()
        {
            spriteBatch = Engine.Graphic.CreateSpriteBatch();
            lightRT = Engine.Graphic.CreateFullscreenRenderTarget();
            worldRT = Engine.Graphic.CreateFullscreenRenderTarget();
            finalRT = Engine.Graphic.CreateFullscreenRenderTarget();
        }

        public void SpawnPlayer(PlayerEntity player)
        {
            var level = GetLevel(PlayerSpawnLevel);
            level.SpawnEntity(player, level.Width / 2, level.Height / 2);
        }

       

        public Level GetLevel(string name)
        {
            foreach (var l in Levels)
            {
                if (l.Name == name)
                {
                    return l;
                }
            }

            return null;
        }

        public Level GetLevel(int id)
        {
            foreach (var l in Levels)
            {
                if (l.Id == id)
                {
                    return l;
                }
            }

            return null;
        }

        public void AddLevel(Level level)
        {
            if (GetLevel(level.Id) == null)
            {
                Levels.Add(level);
            }
        }



        public void Draw(GameTime gameTime, Camera camera)
        {
            var level = camera.FocusEntity.Level;
            var state = level.GetRenderState(camera);

            Engine.Graphic.SetRenderTarget(worldRT);

            Engine.Graphic.Begin(spriteBatch, false, camera.GetTransform());
            level.DrawTerrain(state, spriteBatch, gameTime);
            level.DrawEntities(state, spriteBatch, gameTime);
            level.DrawEntitiesOverlay(state, spriteBatch, gameTime);
            spriteBatch.End();

            
            Engine.Graphic.SetRenderTarget(lightRT);

            //Engine.Graphic.GetGraphicsDevice().Clear(Color.Blue * 0.1f);
            Engine.Graphic.GetGraphicsDevice().Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.LinearClamp, null, null, null, camera.GetTransform());
            level.DrawLightMap(state, spriteBatch, gameTime);
            spriteBatch.End();

            Engine.Graphic.SetRenderTarget(null);

            Engine.Graphic.Begin(spriteBatch);

            spriteBatch.Draw(worldRT, Engine.Graphic.GetResolutionRect(), Color.White);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, lightBlend);

            spriteBatch.Draw(lightRT, Engine.Graphic.GetResolutionRect(), Color.White);

            spriteBatch.End();

            Engine.Graphic.SetRenderTarget(null);




        }



        public void Initialize(GameManager game)
        {
            Game = game;
            foreach (var l in Levels)
            {
                l.Initialize(this, game);
            }
        }

        public void Save()
        {

        }

        public void Load()
        {

        }
    }
}