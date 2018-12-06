using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems.CircleMenuSystem
{
    public class CircleMenuRenderer : EntityDrawSystem
    {
        public const float DISTANCE_FROM_ENTITY = 24f;
        public const float SELECTED_SIZE = 1.5f;

        public CircleMenuRenderer()
        {
            Filter.AnyOf(typeof(CircleMenu), typeof(Inventory));
        }

        public override void Draw(Entity entity, LevelSpriteBatchPool pool, GameTime gameTime)
        {
            var center = entity.Position;
            var menu = entity.GetComponent<CircleMenu>();
            var inventory = entity.GetComponent<Inventory>();

            var itemCount = inventory.Content.GetStackCount();

            pool.Overlay.DrawCircle(center, DISTANCE_FROM_ENTITY * Easing.CircularEaseInOut(menu.Opacity), 16, Color.White );

            for (int i = 0; i < itemCount; i++)
            {
                var angle = (i / (float)itemCount) * Mathf.TwoPI;
                var isSelected = (i == menu.SelectedItem);

                var offx = Mathf.Cos(angle) * DISTANCE_FROM_ENTITY;
                var offy = Mathf.Sin(angle) * DISTANCE_FROM_ENTITY;

                var off = new Vector2(offx, offy) * Easing.CircularEaseInOut(menu.Opacity);

                var itemSprite = inventory.Content.GetStack(i).GetSprite();

                itemSprite.Draw(pool.Overlay,
              new Vector2(1f) + center + off - new Vector2(8 * (isSelected ? SELECTED_SIZE : 1f) * menu.Opacity), (isSelected ? SELECTED_SIZE : 1f) * menu.Opacity, Color.Black * menu.Opacity * 0.45f);
                itemSprite.Draw(pool.Overlay,
                                center + off - new Vector2(8 * (isSelected ? SELECTED_SIZE : 1f) * menu.Opacity), (isSelected ? SELECTED_SIZE : 1f) * menu.Opacity, Color.White * menu.Opacity);
            }
        }
    }
}
