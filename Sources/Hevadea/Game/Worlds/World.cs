using System.Collections.Generic;
using System.Linq;
using Hevadea.Framework;
using Hevadea.Game.Entities.Creatures;
using Hevadea.Game.Registry;
using Hevadea.Game.Storage;
using Maker.Rise;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
                new DayStage("Dawn", 30, new Color(217, 151, 179)),
                new DayStage("Day", 5 * 60, Color.White),
                new DayStage("Dusk", 30, new Color(217, 151, 179)),
                new DayStage("Night", 5 * 30, Color.Blue * 0.1f)
                )
            { Transition = 30 };
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
            if (GetLevel(level.Id) == null) Levels.Add(level);
        }


        public void Draw(GameTime gameTime, Camera camera)
        {
            var level = camera.FocusEntity.Level;
            var state = level.GetRenderState(camera);
            var cameraTransform = camera.GetTransform();
            
            Rise.Graphic.SetRenderTarget(Rise.Graphic.RenderTarget[1]);

            _spriteBatch.Begin(samplerState: SamplerState.LinearWrap, transformMatrix: cameraTransform);
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