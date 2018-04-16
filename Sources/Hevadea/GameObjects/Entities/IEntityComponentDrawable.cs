using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities
{
    public interface IEntityComponentDrawable
    {
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}