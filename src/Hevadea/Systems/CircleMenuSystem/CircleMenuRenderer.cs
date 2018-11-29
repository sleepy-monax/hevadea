using System;
using Hevadea.Entities;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems.CircleMenuSystem
{
    public class CircleMenuRenderer : EntityDrawSystem
    {
        public const float DISTANCE_FROM_ENTITY = 24f;
        public const float DISTANCE_FROM_ENTITY_SELECTED = 32f;
        public const float SELECTED_SIZE = 1.5f;

        public CircleMenuRenderer()
        {
            Filter.AnyOf(typeof(CircleMenu));
        }

        public override void Draw(Entity entity, LevelSpriteBatchPool pool, GameTime gameTime)
        {
            var center = entity.Position;
            var menu = entity.GetComponent<CircleMenu>();

            for (int i = 0; i < 16; i++)
            {
                var angle = (i / 16f - menu.Animation / 16f) * Mathf.TwoPI;
                var isSelected = i == Math.Abs(menu.SelectedItem % 16);

                var offx = Mathf.Cos(angle) * (isSelected ? DISTANCE_FROM_ENTITY_SELECTED : DISTANCE_FROM_ENTITY);
                var offy = Mathf.Sin(angle) * (isSelected ? DISTANCE_FROM_ENTITY_SELECTED : DISTANCE_FROM_ENTITY);

                var off = new Vector2(offx, offy);

                pool.Overlay.DrawRectangle(center + off - new Vector2(8 * (isSelected ? SELECTED_SIZE : 1f)), new Vector2(16 * (isSelected ? SELECTED_SIZE : 1f)), Color.Black);
            }
        }
    }
}
