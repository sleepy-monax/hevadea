using System;
using Hevadea.Entities;
using Hevadea.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hevadea.Systems.CircleMenuSystem
{
    public class CircleMenuProcessor : EntityUpdateSystem
    {
        public CircleMenuProcessor()
        {
            Filter.AnyOf(typeof(CircleMenu));
        }

        public override void Update(Entity entity, GameTime gameTime)
        {
            var menu = entity.GetComponent<CircleMenu>();

            if (Rise.Input.KeyTyped(Keys.U))
            {
                menu.SelectedItem--;
            }
            else if (Rise.Input.KeyTyped(Keys.I))
            {
                menu.SelectedItem++;
            }

            menu.Animation += (menu.SelectedItem - menu.Animation) * (float)gameTime.ElapsedGameTime.TotalSeconds * 10f;
        }
    }
}
