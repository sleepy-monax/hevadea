using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems.RenderingSystem
{
    public class SpriteRenderSystem : EntityDrawSystem
    {
        public SpriteRenderSystem()
        {
            Filter.AnyOf(typeof(RendererSprite), typeof(RendererCreature));
        }

        public override void Draw(Entity entity, LevelSpriteBatchPool pool, GameTime gameTime)
        {
            entity.GetComponent<Renderer>().Render(pool.Entities, entity, entity.Position, gameTime);
        }
    }
}