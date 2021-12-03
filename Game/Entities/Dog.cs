using Hevadea.Entities.Blueprints;
using Hevadea.Entities.Components;
using Hevadea.Entities.Components.AI;
using Hevadea.Entities.Components.AI.Behaviors;
using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Registry;
using Microsoft.Xna.Framework;

namespace Hevadea.Entities
{
    public class Dog : Entity
    {
        public Dog()
        {
            AddComponent(new Agent(new BehaviorEnemy
            {
                MoveSpeedAgro = 0.75f,
                MoveSpeedWandering = 0.5f,
                NaturalEnvironment =
                {
                    TILES.DIRT,
                    TILES.GRASS,
                    TILES.SAND,
                    TILES.WOOD_FLOOR
                },
                Targets = new Groupe<EntityBlueprint>("targets")
                {
                    Members =
                    {
                        ENTITIES.CHIKEN,
                    }
                }
            }));

            AddComponent(new ComponentPickupable());
            AddComponent(new ComponentCastShadow());
            AddComponent(new RendererCreature(Rise.Rnd.Pick(Resources.Sprites.GetSprites("entity/dog"))));
            AddComponent(new ComponentFlammable());
            AddComponent(new ComponentDropExperience(4));

            AddComponent(new ComponentMove());
            AddComponent(new ComponentCollider(new Rectangle(-4, -4, 8, 8)));
            AddComponent(new ComponentHealth(3));
            AddComponent(new ComponentPushable());
            AddComponent(new ComponentSwim());
        }
    }
}