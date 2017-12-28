using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Maker.Rise
{
    public sealed class RiseGame : Game
    {

        public int DrawTime { get; private set; } = 0;
        public int UpdateTime { get; private set; } = 0;

        private Stopwatch drawStopwatch;
        private Stopwatch UpdateStopwatch;

        public delegate void OnLoadHandler(RiseGame sender, EventArgs e);
        public event OnLoadHandler OnLoad;

        public delegate void OnUpdateHandler(RiseGame sender, GameTime gameTime, EventArgs e);
        public event OnUpdateHandler OnUpdate;

        public delegate void OnDrawHandler(RiseGame sender, GameTime gameTime,  EventArgs e);
        public event OnDrawHandler OnDraw;

        public RiseGame()
        {
            drawStopwatch = new Stopwatch();
            UpdateStopwatch = new Stopwatch();
            
            Content.RootDirectory = "Content";
        }

        // Game components Managments -----------------------------------------
        private void IntializeGameComponents()
        {
            Engine.Ressource.Initialize(); EngineRessources.Load();
            Engine.Audio.Initialize();
            Engine.Input.Initialize();
            Engine.Network.Initialize();
            Engine.Scene.Initialize();
            Engine.UI.Initialize();
            Engine.Debug.Initialize();

            OnLoad?.Invoke(this, new EventArgs());
        }

        private void DrawGameComponent(GameTime gameTime)
        {
            Engine.Scene.Draw(gameTime);
            Engine.Debug.Draw(gameTime);
        }

        private void UpdateGameComponent(GameTime gameTime)
        {
            Engine.Audio.Update(gameTime);
            Engine.Input.Update(gameTime);
            Engine.Network.Update(gameTime);
            Engine.Scene.Update(gameTime);
            Engine.UI.Update(gameTime);
            Engine.Ressource.Update(gameTime);
            Engine.Debug.Update(gameTime);
        }

        protected override void Initialize()
        {
            //this.SetFullScreen();
            
            IntializeGameComponents();
            Console.WriteLine($"{nameof(RiseGame)} initialized !");
           
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            UpdateStopwatch.Start();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            UpdateGameComponent(gameTime);
            OnUpdate?.Invoke(this, gameTime, new EventArgs());
            base.Update(gameTime);
            UpdateStopwatch.Stop();
            UpdateTime = UpdateStopwatch.Elapsed.Milliseconds;
            UpdateStopwatch.Reset();
        }

        protected override void Draw(GameTime gameTime)
        {
            drawStopwatch.Start();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            DrawGameComponent(gameTime);
            OnDraw?.Invoke(this, gameTime, new EventArgs());
            base.Draw(gameTime);
            drawStopwatch.Stop();

            DrawTime = drawStopwatch.Elapsed.Milliseconds;
            drawStopwatch.Reset();
        }

    }
}
