using Hevadea.Entities;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems
{
    public interface IEntityRenderSystem
    {
        void Render(Entity entity, LevelSpriteBatchPool pool, GameTime gameTime);
    }
}
