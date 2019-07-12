using Hevadea.Entities.Blueprints;
using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.Ai;
using Hevadea.Entities.Components.Ai.Behaviors;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.Renderer;
using Hevadea.Entities.Components.States;
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
                Targets = new BlueprintGroupe<EntityBlueprint>("targets")
                {
                    Members =
                    {
                        ENTITIES.CHIKEN,   
                    }
                }
            }));
            
            AddComponent(new Pickupable());
            AddComponent(new CastShadow());
            AddComponent(new MobRenderer(Ressources.ImgDog));
            AddComponent(new Flammable());

            AddComponent(new Move());
            AddComponent(new Collider(new Rectangle(-4, -4, 8, 8)));
            AddComponent(new Health(3));
            AddComponent(new Pushable());
        }
    }
}