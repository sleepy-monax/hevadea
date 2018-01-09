using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Component
{
    interface IDrawableComponent
    {
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
