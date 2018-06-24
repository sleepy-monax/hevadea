using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hevadea.Framework.Extension;
using Hevadea.Entities;
using Hevadea.Entities.Components.Attributes;
using Microsoft.Xna.Framework;
using Hevadea.Tiles.Components;

namespace Hevadea.Systems
{
    public class PhysicSystem : GameSystem, IEntityProcessSystem
    {
        public PhysicSystem()
        {
            Filter.AllOf(typeof(Physic));
        }

        public void Process(Entity entity, GameTime gameTime)
        {
            var physic = entity.GetComponent<Physic>();
            physic.Speed += physic.Acceleration;
            physic.Acceleration = Vector2.Zero;

            entity.Position += physic.Speed * gameTime.GetDeltaTime();
        }
    }
}
