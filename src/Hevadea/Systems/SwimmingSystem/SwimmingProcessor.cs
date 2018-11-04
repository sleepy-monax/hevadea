using System;
using Hevadea.Entities;
using Hevadea.Entities.Components.States;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems
{
    public class Swimming : GameSystem, IEntityProcessSystem
    {
        public Swimming()
        {
            Filter.AllOf(typeof(Swim));
        }

        public void Process(Entity entity, GameTime gameTime)
        {

        }
    }
}
