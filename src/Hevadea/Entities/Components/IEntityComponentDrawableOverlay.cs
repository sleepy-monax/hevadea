using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Components
{
    public interface IEntityComponentOverlay
    {
        void Overlay(SpriteBatch spriteBatch, GameTime gameTime);
    }
}