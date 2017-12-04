using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WorldOfImagination.Utils;

namespace WorldOfImagination.Game
{
    public abstract class Entity
    {
        public GameState State;
        public WorldOfImaginationGame Game;

        public float X = 0f;
        public float Y = 0f;

        public float SpeedX = 0f;
        public float SpeedY = 0f;

        public float Width = 0f;
        public float Height = 0f;

        public float MaxSpeed = 5f;
        
        public Entity(WorldOfImaginationGame game, GameState state)
        {
            State = state;
            Game = game;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Game.Debug.Visible)
            {
                spriteBatch.DrawRectangle(new Rectangle((int)X, (int)Y, (int)Width, (int)Height), Color.Red);
            }
            
            OnDraw(spriteBatch, gameTime);
        }

        public abstract void OnDraw(SpriteBatch spriteBatch, GameTime gameTime);

        public void Update(GameTime gameTime)
        {
            OnUpdate(gameTime);
        }

        public abstract void OnUpdate(GameTime gameTime);

        public RectangleF ToRectangleF()
        {
            return  new RectangleF(X, Y, Width, Height);
        }

        public abstract void OnColide(Entity entity);
    }
}