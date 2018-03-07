using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities.Components
{
    public interface IEntityComponentDrawable
    {
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}