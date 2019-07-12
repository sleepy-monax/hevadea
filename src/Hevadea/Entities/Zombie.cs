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
    public class Zombie : Entity
    {
        public Zombie()
        {
            AddComponent(new Flammable());
            AddComponent(new ShadowCaster());

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
                }
            }));

            AddComponent(new MobRenderer(Ressources.ImgZombie));

            AddComponent(new Attack());
            AddComponent(new Collider(new Rectangle(-2, -2, 4, 4)));
            AddComponent(new Energy());
            AddComponent(new Health(10));
            AddComponent(new Move());
            AddComponent(new Pushable { CanBePushBy = { ENTITIES.PLAYER } });
            AddComponent(new Swim());
        }
    }
}