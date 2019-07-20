using Hevadea.Entities.Components;
using Hevadea.Entities.Components.AI;
using Hevadea.Entities.Components.AI.Behaviors;
using Hevadea.Registry;
using Microsoft.Xna.Framework;

namespace Hevadea.Entities
{
    public class Chicken : Entity
    {
        public Chicken()
        {
            AddComponent(new ComponentPickupable());
            AddComponent(new ComponentCastShadow());
            AddComponent(new RendererCreature(Resources.Sprites["entity/chicken"]));
            AddComponent(new ComponentFlammable());

            AddComponent(new ComponentMove());
            AddComponent(new Agent(new BehaviorAnimal() {NaturalEnvironment = {TILES.GRASS}, MoveSpeedWandering = 0.5f}));
            AddComponent(new ComponentCollider(new Rectangle(-4, -4, 8, 8)));
            AddComponent(new ComponentHealth(3));
            AddComponent(new ComponentPushable());
            AddComponent(new ComponentSwim());
            AddComponent(new ComponentDropExperience(2));

        }
    }
}