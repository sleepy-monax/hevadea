using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities
{
    public interface IEntityComponentDrawable
    {
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}