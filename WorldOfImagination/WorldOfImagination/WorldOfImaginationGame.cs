using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using WorldOfImagination.GameComponent;
using WorldOfImagination.GameComponent.Scene;
using WorldOfImagination.Utils;

namespace WorldOfImagination
{
    public class WorldOfImaginationGame : Game
    {
        public Microsoft.Xna.Framework.GraphicsDeviceManager Graphics;

        public AudioManager Audio;
        public DebugManager Debug;
        public InputManager Input;
        public NetworkManager Network;
        public RessourceManager Ressource;
        public SceneManager Scene;
        public UIManager UI;

        public int DrawTime { get; private set; }
        public int UpdateTime { get; private set; }

        private Stopwatch updateStopwatch;
        private Stopwatch drawStopwatch;

        public WorldOfImaginationGame()
        {
            updateStopwatch = new Stopwatch();
            drawStopwatch = new Stopwatch();

            Graphics = new Microsoft.Xna.Framework.GraphicsDeviceManager(this);

            Audio = new AudioManager(this)
                           { UpdateOrder = 0 };

            Input = new InputManager(this)
                           { UpdateOrder = 1 };

            Network = new NetworkManager(this)
                           { UpdateOrder = 2 };


            Scene = new SceneManager(this)
            { DrawOrder = 0, UpdateOrder = 3 };

            UI = new UIManager(this)
            { DrawOrder = 1, UpdateOrder = 4};

            Ressource = new RessourceManager(this)
                           { UpdateOrder = 5 };

            Debug = new DebugManager(this)
            { DrawOrder = 2, UpdateOrder = 6 };


            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.SetTitle("World Of Imagination");
            this.SetFullScreen();
            
            Scene.Switch(new MainMenu(this));
            Console.WriteLine($"{nameof(WorldOfImaginationGame)} initialized !");
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
