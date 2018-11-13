using Hevadea.Entities;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems
{
    public interface IEntityProcessSystem
    {
        void Process(Entity entity, GameTime gameTime);
    }
}
