using Microsoft.Xna.Framework;
using System;

namespace Hevadea.Framework
{
    public class MonoGameHandler : Game
    {
        public delegate void GameEventHandler(Game sender, GameTime gameTime);
        public GraphicsDeviceManager Graphics;
        public event EventHandler OnInitialize;
        public event EventHandler OnLoadContent;
        public event EventHandler OnUnloadContent;
        public event GameEventHandler OnDraw;
        public event GameEventHandler OnUpdate;

        public MonoGameHandler()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        private bool WindowSizeIsBeingChanged = false;
        protected override void Initialize()
        {
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
            Window.ClientSizeChanged += (sender, args) =>
            {
                WindowSizeIsBeingChanged = !WindowSizeIsBeingChanged;
                if (WindowSizeIsBeingChanged)
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
            OnUpdate?.Invoke(this, gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            OnDraw?.Invoke(this, gameTime);
        }
    }
}