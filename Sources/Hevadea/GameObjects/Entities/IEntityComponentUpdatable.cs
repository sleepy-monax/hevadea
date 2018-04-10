using Microsoft.Xna.Framework;

namespace Hevadea.Entities
{
    public interface IEntityComponentUpdatable
    {
        void Update(GameTime gameTime);
    }
}