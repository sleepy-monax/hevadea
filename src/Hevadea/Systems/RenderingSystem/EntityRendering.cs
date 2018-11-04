using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hevadea.Entities;
using Hevadea.Entities.Components.Renderer;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems
{
    public class SpriteRenderSystem : GameSystem, IEntityRenderSystem
    {
        public SpriteRenderSystem()
        {
            Filter.AnyOf(typeof(SpriteRenderer), typeof(MobRenderer));
        }

        public void Render(Entity entity, LevelSpriteBatchPool pool, GameTime gameTime)
        {
            entity.GetComponent<RendererComponent>().Draw(pool.Entities, entity, entity.Position2D, gameTime);
        }
    }
}
