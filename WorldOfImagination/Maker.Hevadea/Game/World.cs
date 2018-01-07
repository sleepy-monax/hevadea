﻿using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.LevelGen;
using Maker.Rise;
using Maker.Rise.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game
{
    public class World
    {
        public Player Player;
        public Level[] Levels;
        private SpriteBatch spriteBatch;
        private BlendState lightBlend;

        private RenderTarget2D SceneRenderTaget;
        private RenderTarget2D lightRenderTaget;
        public Camera Camera;
        public int Time;

        public Level this[int index]
        {
            get { return Levels[index]; }
            set { Levels[index] = value; }
        }

        public World()
        {
            Levels = new Level[1];
            spriteBatch = new SpriteBatch(Engine.Graphic.GraphicsDevice);
            Camera = new Camera();

            lightBlend = new BlendState()
            {
                ColorBlendFunction = BlendFunction.Add,
                ColorSourceBlend = Blend.DestinationColor,
                ColorDestinationBlend = Blend.Zero
            };

            SceneRenderTaget = new RenderTarget2D(Engine.Graphic.GraphicsDevice, Engine.Graphic.GetWidth(),
                Engine.Graphic.GetHeight());
            lightRenderTaget = new RenderTarget2D(Engine.Graphic.GraphicsDevice, Engine.Graphic.GetWidth(),
                Engine.Graphic.GetHeight());
        }

        public void Draw(GameTime gameTime, bool showDebug = true, bool renderTiles = true, bool renderEntity = true)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap, null, Engine.CommonRasterizerState,
                null, Camera.GetTransform());

            var state = Player.Level.GetRenderState(Camera);

            Player.Level.DrawTerrain(state, spriteBatch, gameTime);
            Player.Level.DrawEntities(state, spriteBatch, gameTime);

            state.Clear();
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            Player.Level.Update(gameTime);
            Time++;
        }

        // Static functions ---------------------------------------------------
        // Load, Save and generate a new World.

        public void Initialize()
        {
            foreach (var l in Levels)
            {
                l.Initialize(this);
            }

            Camera.FocusEntity = Player;
        }

        public static World Generate(int seed)
        {
            World world = new World();

            world[0] = new OverWorldGenerator().Generate();

            world.Player = new Player();
            world[0].AddEntity(world.Player);

            world.Player.SetPosition((world.Levels[0].Width / 2) * ConstVal.TileSize,
                (world.Levels[0].Height / 2) * ConstVal.TileSize);


            return world;
        }
    }
}