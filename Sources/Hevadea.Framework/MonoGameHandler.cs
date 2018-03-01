using System;
using Microsoft.Xna.Framework;

namespace Hevadea.Framework
{
    public class MonoGameHandler : Game
    {
        public delegate void GameEventHandler(Game sender, GameTime gameTime);

        public event EventHandler OnInitialize;
        public event EventHandler OnLoadContent;
        public event EventHandler OnUnloadContent;
        public event GameEventHandler OnDraw;
        public event GameEventHandler OnUpdate;

        public MonoGameHandler()
        {
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
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