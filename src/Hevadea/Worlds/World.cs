using Hevadea.Framework;
using Hevadea.GameObjects.Entities;
using Hevadea.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Worlds
{
    public class World
    {
        readonly SpriteBatch _spriteBatch;

        readonly BlendState _lightBlend = new BlendState
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

		public static World Load(WorldStorage store)
		{
			World world = new World();

			world.DayNightCycle.Time = store.Time;
			world.PlayerSpawnLevel = store.PlayerSpawnLevel;

			return world;
		}

		public WorldStorage Save()
		{
			WorldStorage worldStorage = new WorldStorage();

			worldStorage.Time = DayNightCycle.Time;
			worldStorage.PlayerSpawnLevel = PlayerSpawnLevel;

            foreach (var l in Levels)
			{
				worldStorage.Levels.Add(l.Name);
			}

			return worldStorage;
		}

        public void SpawnPlayer(EntityPlayer player)
        {
            var level = GetLevel(PlayerSpawnLevel);
            level.AddEntityAt(player, level.Width / 2, level.Height / 2);
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

            _spriteBatch.Begin(SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp, transformMatrix: cameraTransform);
            level.DrawTerrain(state, _spriteBatch, gameTime);
            _spriteBatch.End();

            _spriteBatch.Begin(SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp, transformMatrix: cameraTransform);
            level.DrawEntities(state, _spriteBatch, gameTime);
            _spriteBatch.End();

            if (Rise.ShowGui)
            {
                _spriteBatch.Begin(SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp, transformMatrix: cameraTransform);
                level.DrawEntitiesOverlay(state, _spriteBatch, gameTime);
                _spriteBatch.End();
            }

            Rise.Graphic.SetRenderTarget(Rise.Graphic.RenderTarget[0]);

            var ambiantColor = level.Properties.AffectedByDayNightCycle
                ? DayNightCycle.GetAmbiantLight()
                : level.Properties.AmbiantLight;
            Rise.Graphic.Clear(ambiantColor);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive, SamplerState.LinearClamp, transformMatrix: cameraTransform);
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

        public int GetUeid()
        {
            var ueid = 0;

            do ueid = Rise.Rnd.Next();
            while (GetEntityByUeid(ueid) != null);

            return ueid;
        }

        public Entity GetEntityByUeid(int Ueid)
        {
            foreach (var l in Levels)
            {
                foreach (var c in l.Chunks)
                {
                    foreach (var e in c.Entities)
                    {
                        if (e.Ueid == Ueid) return e;
                    }
                }
            }

            return null;
        }

        public void Initialize(GameManager game)
        {
            Game = game;
            foreach (var l in Levels) l.Initialize(this, game);
        }
    }
}