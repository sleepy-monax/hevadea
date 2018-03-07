using System;
using System.Collections.Generic;
using System.Reflection;
using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Hevadea.Game.Entities;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Registry;
using Hevadea.Game.Storage;
using Hevadea.Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Worlds
{
    public partial class Level
    {
        public void Initialize(World world, GameManager game)
        {
            Logger.Log<Level>(LoggerLevel.Info, "Initializing level...");
            _world = world;
            _game = game;
            Logger.Log<Level>(LoggerLevel.Info, "Initializing entities...");
            foreach (var e in _entities) e.Initialize(this, world, _game);
            Logger.Log<Level>(LoggerLevel.Fine, "Done!");
        }

        public LevelStorage Save()
        {
            Logger.Log<Level>(LoggerLevel.Info, "Saving level...");
            var store = new LevelStorage
            {
                Width = Width,
                Height = Height,
                Tiles = _tiles,
                TilesData = _tilesData,
                Name = Name,
                Id = Id, 
                Type = Properties.Name
            };

            Logger.Log<Level>(LoggerLevel.Info, "Saving entities...");
            foreach (var e in _entities) store.Entities.Add(e.Save());

            Logger.Log<Level>(LoggerLevel.Fine, "Done!");
            return store;
        }

        public void Load(LevelStorage store)
        {
            var asm = Assembly.GetExecutingAssembly();
            foreach (var item in store.Entities)
            {
                var e = ENTITIES.GetBlueprint(item.Type).Build();
                e.Load(item);
                AddEntity(e);
            }

            Width = store.Width;
            Height = store.Height;
            _tiles = store.Tiles;
            _tilesData = store.TilesData;
            Name = store.Name;
            Id = store.Id;
        }

        public void Update(LevelRenderState state, GameTime gameTime)
        {
            for (var i = 0; i < Width * Height / 50; i++)
            {
                var tx = Rise.Random.Next(Width);
                var ty = Rise.Random.Next(Height);
                GetTile(tx, ty).Update(new TilePosition(tx, ty), _tilesData[tx + ty * Width], this, gameTime);
            }

            ParticleSystem.Update(gameTime);
            
            foreach (var e in state.OnScreenEntities) e.Update(gameTime);
        }

        public LevelRenderState GetRenderState(Camera camera)
        {
            var entitiesOnScreen = new List<Entity>();
            var focusEntity = new Point((int) camera.X / ConstVal.TileSize, (int) camera.Y / ConstVal.TileSize);
            var dist = new Point(camera.GetWidth() / 2 / ConstVal.TileSize + 4,
                camera.GetHeight() / 2 / ConstVal.TileSize + 4);

            var state = new LevelRenderState
            {
                Begin = new Point(Math.Max(0, focusEntity.X - dist.X),
                    Math.Max(0, focusEntity.Y - dist.Y + 1)),

                End = new Point(Math.Min(Width, focusEntity.X + dist.X + 1),
                    Math.Min(Height, focusEntity.Y + dist.Y + 6))
            };

            for (var tx = state.Begin.X; tx < state.End.X; tx++)
            for (var ty = state.Begin.Y; ty < state.End.Y; ty++)
                entitiesOnScreen.AddRange(_entitiesOnTiles[tx, ty]);

            entitiesOnScreen.Sort((a, b) => (a.Y + a.Origin.Y).CompareTo(b.Y + b.Origin.Y));

            state.OnScreenEntities = entitiesOnScreen;

            return state;
        }

        public void DrawTerrain(LevelRenderState state, SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (var tx = state.Begin.X; tx < state.End.X; tx++)
            for (var ty = state.Begin.Y; ty < state.End.Y; ty++)
                GetTile(tx, ty).Draw(spriteBatch, new TilePosition(tx, ty), _tilesData[tx + ty * Width], this, gameTime);
            
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
                    spriteBatch.DrawRectangle(e.Bound, Color.Aqua * 0.5f, 0.4f);
                    spriteBatch.PutPixel(e.Position + e.Origin.ToVector2(), Color.Magenta);
                }
            }
        }

        public void DrawLightMap(LevelRenderState state, SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var e in state.OnScreenEntities)
            {
                var light = e.Get<Light>();

                if (light != null)
                    spriteBatch.Draw(Ressources.ImgLight,
                        new Rectangle((int) e.X - light.Power + e.Width / 2, (int) e.Y - light.Power + e.Height / 2,
                            light.Power * 2, light.Power * 2), light.Color);
            }
        }
    }
}