using Hevadea.Entities;
using Hevadea.Entities.Components;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems.SwimmingSystem
{
    public class Swimming : EntityUpdateSystem
    {
        public Swimming()
        {
            Filter.AllOf(typeof(ComponentSwim));
        }

        public override void Update(Entity entity, GameTime gameTime)
        {
        }
    }
}