using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.Threading;

namespace Hevadea.Framework
{
    public class MonoGameHandler : Game
    {
        private Stopwatch _drawStopwatch = new Stopwatch();
        private Stopwatch _updateStopwatch = new Stopwatch();
        private bool _windowSizeIsBeingChanged = false;

        public delegate void GameloopEventHandler(Game sender, GameTime gameTime);

        public GraphicsDeviceManager Graphics;

        public event EventHandler OnInitialize;

        public event EventHandler OnLoadContent;

        public event EventHandler OnUnloadContent;

        public event GameloopEventHandler OnDraw;

        public event GameloopEventHandler OnUpdate;

        public int DrawTime { get; private set; } = 0;
        public int UpdateTime { get; private set; } = 0;

        public Thread UiThread { get; private set; } = null;

        public MonoGameHandler()
        {
            Content.RootDirectory = "Content";
            Graphics = new GraphicsDeviceManager(this);

            //Graphics.SynchronizeWithVerticalRetrace = false;
            //IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            Window.AllowUserResizing = true;
            IsMouseVisible = true;

            Window.ClientSizeChanged += (sender, args) =>
            {
                _windowSizeIsBeingChanged = !_windowSizeIsBeingChanged;
                if (_windowSizeIsBeingChanged)
                {
                    Rise.Graphic.SetSize(new Point(Window.ClientBounds.Width, Window.ClientBounds.Height));
                }
            };
            OnInitialize?.Invoke(this, EventArgs.Empty);
        }

        protected override void LoadContent()
        {
            OnLoadContent?.Invoke(this, EventArgs.Empty);
        }

        protected override void UnloadContent()
        {
            OnUnloadContent?.Invoke(this, EventArgs.Empty);
        }

        protected override void Update(GameTime gameTime)
        {
            _updateStopwatch.Start();
            OnUpdate?.Invoke(this, gameTime);
            _updateStopwatch.Stop();

            UpdateTime = (int)_updateStopwatch.ElapsedMilliseconds;
            _updateStopwatch.Reset();
        }

        protected override void Draw(GameTime gameTime)
        {
            if (UiThread == null) UiThread = Thread.CurrentThread;

            _drawStopwatch.Start();
            OnDraw?.Invoke(this, gameTime);
            _drawStopwatch.Stop();

            DrawTime = (int)_drawStopwatch.ElapsedMilliseconds;
            _drawStopwatch.Reset();
        }
    }
}