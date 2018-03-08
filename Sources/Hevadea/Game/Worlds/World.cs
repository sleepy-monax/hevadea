using Hevadea.Framework;
using Hevadea.Game.Entities;
using Hevadea.Game.Registry;
using Hevadea.Game.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Game.Worlds
{
    public class World
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly BlendState _lightBlend = new BlendState
        {
            ColorBlendFunction = BlendFunction.Add,
            ColorSourceBlend = Blend.DestinationColor,
            ColorDestinationBlend = Blend.Zero
        };
        
        public GameManager Game;
        public List<Level> Levels = new List<Level>();
        public DayNightCycle DayNightCycle { get; }
        public string PlayerSpawnLevel = "overworld";

        public World()
        {
            _spriteBatch = Rise.Graphic.CreateSpriteBatch();
            DayNightCycle = new DayNightCycle(                
                new DayStage("Day", 600, Color.White),
                
                new DayStage("Dusk0", 30, new Color(187, 104, 50)),
                new DayStage("Dusk1", 30, new Color(125, 54, 48)),
                new DayStage("Dusk2", 30, new Color(75, 32, 32)),
                new DayStage("Dusk3", 30, new Color(25, 26, 25)),
                
                new DayStage("Night", 600, Color.Blue * 0.1f),

                new DayStage("Dawn0", 30, new Color(25, 26, 25)),
                new DayStage("Dawn1", 30, new Color(75, 32, 32)),
                new DayStage("Dawn2", 30, new Color(125, 54, 48)),
                new DayStage("Dawn3", 30, new Color(187, 104, 50))
                )
            { Transition = 30 };
        }

        public void SpawnPlayer(EntityPlayer player)
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
            if (GetLevel(level.Id) == null) Levels.Add(level);
        }


        public void Draw(GameTime gameTime, Camera camera)
        {
            var level = camera.FocusEntity.Level;
            var state = level.GetRenderState(camera);
            var cameraTransform = camera.GetTransform();
            
            Rise.Graphic.SetRenderTarget(Rise.Graphic.RenderTarget[1]);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: cameraTransform);
            level.DrawTerrain(state, _spriteBatch, gameTime);
            level.DrawEntities(state, _spriteBatch, gameTime);
            level.DrawEntitiesOverlay(state, _spriteBatch, gameTime);
            _spriteBatch.End();

            Rise.Graphic.SetRenderTarget(Rise.Graphic.RenderTarget[0]);


            var ambiantColor = level.Properties.AffectedByDayNightCycle
                ? DayNightCycle.GetAmbiantLight()
                : level.Properties.AmbiantLight;
            Rise.Graphic.Clear(ambiantColor);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.LinearClamp, transformMatrix: cameraTransform);
            level.DrawLightMap(state, _spriteBatch, gameTime);
            _spriteBatch.End();

            Rise.Graphic.SetDefaultRenderTarget();

            _spriteBatch.Begin();

            _spriteBatch.Draw(Rise.Graphic.RenderTarget[1], Rise.Graphic.GetBound(), Color.White);

            _spriteBatch.End();

            _spriteBatch.Begin(SpriteSortMode.Immediate, _lightBlend);

            _spriteBatch.Draw(Rise.Graphic.RenderTarget[0], Rise.Graphic.GetBound(), Color.White);

            _spriteBatch.End();
        }

        public void Initialize(GameManager game)
        {
            Game = game;
            foreach (var l in Levels) l.Initialize(this, game);
        }

        public WorldStorage Save()
        {
            var w = new WorldStorage();
            foreach (var l in Levels)
            {
                w.Levels.Add(l.Save());
            }

            
            w.Time = DayNightCycle.Time;
            w.PlayerSpawnLevel = PlayerSpawnLevel;
            return w;
        }

        public void Load(WorldStorage store)
        {
            DayNightCycle.Time = store.Time;
            PlayerSpawnLevel = store.PlayerSpawnLevel;
            
            foreach (var levelData in store.Levels)
            {
                var level = new Level(LEVELS.GetProperties(levelData.Type), levelData.Width, levelData.Height);
                level.Load(levelData);
                Levels.Add(level);
            }
        }
    }
}