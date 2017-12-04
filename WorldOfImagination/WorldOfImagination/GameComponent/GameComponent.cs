using System;
using Microsoft.Xna.Framework;

namespace WorldOfImagination.GameComponent
{
    public abstract class GameComponent
    {
        protected WorldOfImaginationGame Game;
        protected GameComponent(WorldOfImaginationGame game)
        {
            Game = game;
        }
        
        public abstract void Initialize();
        public abstract void Draw(GameTime gameTime);
        public abstract void Update(GameTime gameTime);
    }
}