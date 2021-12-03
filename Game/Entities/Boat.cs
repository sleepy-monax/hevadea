using Hevadea.Entities.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Entities
{
    public class Boat : Entity
    {
        public Boat()
        {
            AddComponent(new RendererCreature(Resources.Sprites["entity/boat"]));

            AddComponent(new ComponentCollider(new Rectangle(-8, -8, 16, 16)));
            AddComponent(new ComponentMove());
            AddComponent(new ComponentPushable());
            AddComponent(new ComponentRideable());
            AddComponent(new ComponentSwim());
            AddComponent(new ComponentPickupable());
        }
    }
}
