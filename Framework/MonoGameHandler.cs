using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Hevadea.Framework
{
    public class MonoGameHandler : Microsoft.Xna.Framework.Game
    {
        public int UpdateTime { get; private set; } = 0;
        public int DrawTime { get; private set; } = 0;

        public GraphicsDeviceManager Graphics { get; private set; }

        private Stopwatch _updateStopwatch = new Stopwatch();
        private Stopwatch _drawStopwatch = new Stopwatch();

        public delegate void GameloopEventHandler(Microsoft.Xna.Framework.Game sender, GameTime gameTime);

        public event EventHandler OnInitialize;
        public event EventHandler OnLoadContent;
        public event EventHandler OnUnloadContent;

        public event GameloopEventHandler OnUpdate;
        public event GameloopEventHandler OnDraw;

        public int Ticks { get; private set; } = 0;

        public MonoGameHandler()
        {
            Content.RootDirectory = "Content";

            Graphics = new GraphicsDeviceManager(this);
            Graphics.SynchronizeWithVerticalRetrace = true;

            IsFixedTimeStep = true;
        }

        protected override void Initialize()
        {
            Logger.Log<MonoGameHandler>("Initializing...");
            IsMouseVisible = true;
            OnInitialize?.Invoke(this, EventArgs.Empty);
        }

        protected override void LoadContent()
        {
            Logger.Log<MonoGameHandler>("LoadContent...");
            OnLoadContent?.Invoke(this, EventArgs.Empty);
        }

        protected override void UnloadContent()
        {
            Logger.Log<MonoGameHandler>("UnloadContent...");
            OnUnloadContent?.Invoke(this, EventArgs.Empty);
        }

        protected override void Update(GameTime gameTime)
        {
            _updateStopwatch.Start();
            OnUpdate?.Invoke(this, gameTime);
            _updateStopwatch.Stop();

            UpdateTime = (int) _updateStopwatch.ElapsedMilliseconds;
            _updateStopwatch.Reset();

            Ticks++;
        }

        protected override void Draw(GameTime gameTime)
        {
            _drawStopwatch.Start();
            OnDraw?.Invoke(this, gameTime);
            _drawStopwatch.Stop();

            DrawTime = (int) _drawStopwatch.ElapsedMilliseconds;
            _drawStopwatch.Reset();
        }
    }
}