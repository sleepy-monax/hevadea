using Maker.Rise.GameComponent;
using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Maker.Rise
{
    public sealed class RiseGame : Game
    {
        public Microsoft.Xna.Framework.GraphicsDeviceManager Graphics;
        public readonly RasterizerState RasterizerState;
        
        public readonly AudioManager     Audio;
        public readonly DebugManager     Debug;
        public readonly InputManager     Input;
        public readonly NetworkManager   Network;
        public readonly RessourceManager Ressource;
        public readonly SceneManager     Scene;
        public readonly UiManager        UI;
        public readonly Ressources       Ress;

        public int DrawTime { get; private set; }
        private Stopwatch drawStopwatch;

        public delegate void OnLoadHandler(RiseGame sender, EventArgs e);
        public event OnLoadHandler OnLoad;

        public delegate void OnUpdateHandler(RiseGame sender, GameTime gameTime, EventArgs e);
        public event OnUpdateHandler OnUpdate;

        public delegate void OnDrawHandler(RiseGame sender, GameTime gameTime,  EventArgs e);
        public event OnDrawHandler OnDraw;

        public RiseGame()
        {
            RasterizerState = new RasterizerState { ScissorTestEnable = true};
            drawStopwatch = new Stopwatch();
            Graphics = new Microsoft.Xna.Framework.GraphicsDeviceManager(this);

            Audio = new AudioManager(this);
            Input = new InputManager(this);
            Network = new NetworkManager(this);
            Scene = new SceneManager(this);
            UI = new UiManager(this);
            Ressource = new RessourceManager(this);
            Debug = new DebugManager(this);
            Ress = new Ressources(this);
            
            Content.RootDirectory = "Content";
        }

        // Game components Managments -----------------------------------------
        private void IntializeGameComponents()
        {
            Ressource.Initialize(); Ress.Load();
            Audio.Initialize();
            Input.Initialize();
            Network.Initialize();
            Scene.Initialize();
            UI.Initialize();
            Debug.Initialize();

            OnLoad?.Invoke(this, new EventArgs());
        }

        private void DrawGameComponent(GameTime gameTime)
        {
            Scene.Draw(gameTime);
            Debug.Draw(gameTime);
        }

        private void UpdateGameComponent(GameTime gameTime)
        {
            Audio.Update(gameTime);
            Input.Update(gameTime);
            Network.Update(gameTime);
            Scene.Update(gameTime);
            UI.Update(gameTime);
            Ressource.Update(gameTime);
            Debug.Update(gameTime);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            UpdateGameComponent(gameTime);
            OnUpdate?.Invoke(this, gameTime, new EventArgs());
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            drawStopwatch.Start();
            //Thread.Sleep(50);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            DrawGameComponent(gameTime);
            OnDraw?.Invoke(this, gameTime, new EventArgs());
            base.Draw(gameTime);
            drawStopwatch.Stop();
            DrawTime = (int)drawStopwatch.Elapsed.Milliseconds;
            drawStopwatch.Reset();
        }

    }
}
