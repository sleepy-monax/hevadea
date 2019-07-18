using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Components
{
    public interface IEntityComponentDrawable
    {
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}