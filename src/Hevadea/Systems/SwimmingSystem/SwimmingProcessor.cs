using System;
using Hevadea.Entities;
using Hevadea.Entities.Components.States;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems
{
    public class Swimming : EntityUpdateSystem
    {
        public Swimming()
        {
            Filter.AllOf(typeof(Swim));
        }

        public override void Update(Entity entity, GameTime gameTime)
        {

        }
    }
}
