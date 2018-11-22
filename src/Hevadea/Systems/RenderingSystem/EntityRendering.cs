using Hevadea.Entities;
using Hevadea.Entities.Components.Renderer;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems
{
    public class SpriteRenderSystem : EntityDrawSystem
    {
        public SpriteRenderSystem()
        {
            Filter.AnyOf(typeof(SpriteRenderer), typeof(MobRenderer));
        }

        public override void Draw(Entity entity, LevelSpriteBatchPool pool, GameTime gameTime)
        {
            entity.GetComponent<RendererComponent>().Render(pool.Entities, entity, entity.Position, gameTime);
        }
    }
}
