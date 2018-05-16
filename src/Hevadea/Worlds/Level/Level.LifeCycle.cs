using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Entities.Components.States;
using Hevadea.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Hevadea.Worlds
{
    public partial class Level
    {

		public bool IsInitialized { get; private set; } = false;

        public void Initialize(World world, GameManager game)
        {
            _world = world;
            _game = game;
            foreach (var c in Chunks) foreach (var e in c.Entities) e.Initialize(this, world, _game);

            IsInitialized = true;
        }

        public void Update(RenderState state, GameTime gameTime)
        {
            for (var i = 0; i < Width * Height / 50; i++)
            {
                var tx = Rise.Rnd.Next(Width);
                var ty = Rise.Rnd.Next(Height);
                var tile = GetTile(tx, ty);
                tile.Update(new TilePosition(tx, ty), GetTileDataAt(tx, ty), this, gameTime);
            }

            ParticleSystem.Update(gameTime);

            foreach (var e in state.AliveEntities) e.Update(gameTime);
        }

        public RenderState GetRenderState(Camera camera)
        {
            var entitiesOnScreen = new List<Entity>();
            var focusEntity = new Point((int)camera.X / GLOBAL.Unit, (int)camera.Y / GLOBAL.Unit);
            var dist = new Point(camera.GetWidth() / 2 / GLOBAL.Unit + 1,
                camera.GetHeight() / 2 / GLOBAL.Unit);

            var state = new RenderState
            {
                RenderBegin = new Point(Math.Max(0, focusEntity.X - dist.X),
                    Math.Max(0, focusEntity.Y - dist.Y - 1)),

                RenderEnd = new Point(Math.Min(Width, focusEntity.X + dist.X + 1),
                    Math.Min(Height, focusEntity.Y + dist.Y + 6))
            };

            for (var tx = state.RenderBegin.X; tx < state.RenderEnd.X; tx++)
			{
                for (var ty = state.RenderBegin.Y; ty < state.RenderEnd.Y; ty++)
				{
					entitiesOnScreen.AddRange(GetEntitiesAt(tx, ty));
				}
			}

            entitiesOnScreen.Sort((a, b) => (a.Y + a.SortingOffset).CompareTo(b.Y + b.SortingOffset));

            state.AliveEntities = entitiesOnScreen;

            return state;
        }

        public void DrawTerrain(RenderState state, SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (var ty = state.RenderBegin.Y; ty < state.RenderEnd.Y; ty++)
            {
                for (var tx = state.RenderBegin.X; tx < state.RenderEnd.X; tx++)
                {
                    GetTile(tx, ty).Draw(spriteBatch, new TilePosition(tx, ty), GetTileDataAt(tx, ty), this, gameTime);
                }
            }

            ParticleSystem.Draw(spriteBatch, gameTime);
        }

        public void DrawEntities(RenderState state, SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var e in state.AliveEntities) e.Draw(spriteBatch, gameTime);
        }

        public void DrawEntitiesOverlay(RenderState state, SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var e in state.AliveEntities)
            {
                e.DrawOverlay(spriteBatch, gameTime);

                if (Rise.ShowDebug)                
                    spriteBatch.PutPixel(e.Position, Color.Magenta);                
            }
        }

        public void DrawLightMap(RenderState state, SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var e in state.AliveEntities)
            {
                var light = e.GetComponent<Light>();

                if (light != null && light.On)
                    spriteBatch.Draw(Ressources.ImgLight,
                        new Rectangle((int)e.X - light.Power, (int)e.Y - light.Power,
                            light.Power * 2, light.Power * 2), light.Color);
            }
        }
    }
}