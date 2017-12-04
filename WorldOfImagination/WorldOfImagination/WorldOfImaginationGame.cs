using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using WorldOfImagination.GameComponent;
using WorldOfImagination.GameComponent.Scene;
using WorldOfImagination.Utils;

namespace WorldOfImagination
{
    public class WorldOfImaginationGame : Microsoft.Xna.Framework.Game
    {
        public Microsoft.Xna.Framework.GraphicsDeviceManager Graphics;

        public readonly AudioManager Audio;
        public readonly DebugManager Debug;
        public readonly InputManager Input;
        public readonly NetworkManager Network;
        public readonly RessourceManager Ressource;
        public readonly SceneManager Scene;
        public readonly UiManager UI;
        public readonly Ressources Ress;

        public int DrawTime { get; private set; }
        private Stopwatch drawStopwatch;

        public WorldOfImaginationGame()
        {
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
            Audio.Initialize();
            Input.Initialize();
            Network.Initialize();
            Scene.Initialize();
            UI.Initialize();
            Ressource.Initialize();
            Debug.Initialize();
            Ress.Load();
        }

        private void DrawGameComponent(GameTime gameTime)
        {
            Scene.Draw(gameTime);
            UI.Draw(gameTime);
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
            this.SetTitle("World Of Imagination");
            this.SetFullScreen();

            Scene.Switch(new MainMenu(this));
            Console.WriteLine($"{nameof(WorldOfImaginationGame)} initialized !");

            IntializeGameComponents();
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
            base.Update(gameTime);
        }

        protected override bool BeginDraw()
        {
            drawStopwatch.Start();

            return base.BeginDraw();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            DrawGameComponent(gameTime);
            base.Draw(gameTime);
        }

        protected override void EndDraw()
        {
            drawStopwatch.Stop();
            DrawTime = (int)drawStopwatch.Elapsed.Milliseconds;
            drawStopwatch.Reset();
            base.EndDraw();
        }
    }
}
