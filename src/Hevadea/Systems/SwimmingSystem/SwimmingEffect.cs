using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Renderer;
using Hevadea.Entities.Components.States;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems
{
    public class SwimmingEffect : EntityDrawSystem
    {
        public static readonly Point Size = new Point(16, 8);

        public SwimmingEffect()
        {
            Filter.AllOf(typeof(Swim));
        }

        public override void Draw(Entity entity, LevelSpriteBatchPool pool, GameTime gameTime)
        {
            if (entity.IsSwiming())
            {
                var frame = MobRenderer.FRAMES[(int)(gameTime.TotalGameTime.TotalSeconds * 8 % 4)];

                Rectangle source = new Rectangle(new Point(16 * frame, 0), Size);
                Vector2 position = entity.Position2D - Size.ToVector2() / 2;

                pool.Tiles.Draw(Ressources.ImgWater, position, source, Color.White);
            }
        }
    }
}
