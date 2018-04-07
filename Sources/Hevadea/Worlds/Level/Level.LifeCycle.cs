using System;
using System.Collections.Generic;
using System.Diagnostics;
using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Hevadea.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Worlds
{
    public partial class Level
    {
        private bool _isInitialized;

        public bool IsInitialized
        {
            get => _isInitialized;
            private set{
                Logger.Log<Level>($"IsInitialized = {value}");
                _isInitialized = value;
            }
        }

        public void Initialize(World world, GameManager game)
        {
            Logger.Log<Level>(LoggerLevel.Info, "Initializing level...");
            _world = world;
            _game = game;
            Logger.Log<Level>(LoggerLevel.Info, "Initializing entities...");
            foreach (var e in Entities) e.Initialize(this, world, _game);
            Logger.Log<Level>(LoggerLevel.Fine, "Done!");

            IsInitialized = true;
        }

        public void Update(LevelRenderState state, GameTime gameTime)
        {
            for (var i = 0; i < Width * Height / 50; i++)
            {
                var tx = Rise.Rnd.Next(Width);
                var ty = Rise.Rnd.Next(Height);
                var tile = GetTile(tx, ty);
                tile.Update(new TilePosition(tx, ty), TilesData[tx + ty * Width], this, gameTime);
            }

            ParticleSystem.Update(gameTime);
            
            foreach (var e in state.OnScreenEntities) e.Update(gameTime);
        }

        public LevelRenderState GetRenderState(Camera camera)
        {
            var entitiesOnScreen = new List<Entity>();
            var focusEntity = new Point((int) camera.X / Constant.TileSize, (int) camera.Y / Constant.TileSize);
            var dist = new Point(camera.GetWidth() / 2 / Constant.TileSize + 8,
                camera.GetHeight() / 2 / Constant.TileSize + 8);

            var state = new LevelRenderState
            {
                Begin = new Point(Math.Max(0, focusEntity.X - dist.X),
                    Math.Max(0, focusEntity.Y - dist.Y)),

                End = new Point(Math.Min(Width, focusEntity.X + dist.X),
                    Math.Min(Height, focusEntity.Y + dist.Y))
            };

            for (var tx = state.Begin.X; tx < state.End.X; tx++)
            for (var ty = state.Begin.Y; ty < state.End.Y; ty++)
                entitiesOnScreen.AddRange(_entitiesOnTiles[tx, ty]);

            entitiesOnScreen.Sort((a, b) => (a.Y + a.SortingOffset).CompareTo(b.Y + b.SortingOffset));

            state.OnScreenEntities = entitiesOnScreen;

            return state;
        }

        public void DrawTerrain(LevelRenderState state, SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (var ty = state.Begin.Y; ty < state.End.Y; ty++)
            {
                for (var tx = state.Begin.X; tx < state.End.X; tx++)
                {
                    GetTile(tx, ty).Draw(spriteBatch, new TilePosition(tx, ty), TilesData[tx + ty * Width], this, gameTime);
                }
            }
            
            ParticleSystem.Draw(spriteBatch, gameTime);
        }

        public void DrawEntities(LevelRenderState state, SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var e in state.OnScreenEntities) e.Draw(spriteBatch, gameTime);
        }

        public void DrawEntitiesOverlay(LevelRenderState state, SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var e in state.OnScreenEntities)
            {
                e.DrawOverlay(spriteBatch, gameTime);

                if (Rise.ShowDebug)
                {
                    spriteBatch.PutPixel(e.Position, Color.Magenta);
                }
            }
        }

        public void DrawLightMap(LevelRenderState state, SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var e in state.OnScreenEntities)
            {
                var light = e.GetComponent<Light>();

                if (light != null && light.On)
                    spriteBatch.Draw(Ressources.ImgLight,
                        new Rectangle((int) e.X - light.Power, (int) e.Y - light.Power,
                            light.Power * 2, light.Power * 2), light.Color);
            }
        }
    }
}