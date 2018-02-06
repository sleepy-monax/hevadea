using Maker.Hevadea.Game.Entities;
using Maker.Rise;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Maker.Hevadea.Game
{
    public class World
    {
        public GameManager Game;
        public int Time;
        public List<Level> Levels = new List<Level>();
        public string PlayerSpawnLevel = "overworld";

        private SpriteBatch spriteBatch;
        private BlendState lightBlend = new BlendState() { ColorBlendFunction = BlendFunction.Add, ColorSourceBlend = Blend.DestinationColor, ColorDestinationBlend = Blend.Zero };
        public World()
        {
            spriteBatch = Engine.Graphic.CreateSpriteBatch();
        }

        public void SpawnPlayer(PlayerEntity player)
        {
            var level = GetLevel(PlayerSpawnLevel);
            level.SpawnEntity(player, level.Width / 2, level.Height / 2);
        }

       

        public Level GetLevel(string name)
        {
            return Levels.FirstOrDefault(l => l.Name == name);
        }

        public Level GetLevel(int id)
        {
            return Levels.FirstOrDefault(l => l.Id == id);
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

            //Engine.Graphic.SetRenderTarget(Engine.Graphic.RenderTarget[1]);

            Engine.Graphic.Begin(spriteBatch, false, camera.GetTransform());
            level.DrawTerrain(state, spriteBatch, gameTime);
            level.DrawEntities(state, spriteBatch, gameTime);
            level.DrawEntitiesOverlay(state, spriteBatch, gameTime);
            spriteBatch.End();

            return;
            
            Engine.Graphic.SetRenderTarget(Engine.Graphic.RenderTarget[0]);

            //Engine.Graphic.GetGraphicsDevice().Clear(Color.Blue * 0.1f);
            Engine.Graphic.GetGraphicsDevice().Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.LinearClamp, null, null, null, camera.GetTransform());
            level.DrawLightMap(state, spriteBatch, gameTime);
            spriteBatch.End();

            Engine.Graphic.SetRenderTarget(Engine.Scene.RenderTarget);

            Engine.Graphic.Begin(spriteBatch);

            spriteBatch.Draw(Engine.Graphic.RenderTarget[1], Engine.Graphic.GetResolutionRect(), Color.White);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, lightBlend);

            spriteBatch.Draw(Engine.Graphic.RenderTarget[0], Engine.Graphic.GetResolutionRect(), Color.White);

            spriteBatch.End();



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