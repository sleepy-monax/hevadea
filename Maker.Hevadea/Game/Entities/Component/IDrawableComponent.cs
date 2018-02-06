using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Component
{
    public interface IDrawableComponent
    {
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
