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
    public class Chicken : Entity
    {
        public Chicken()
        {
            AddComponent(new Pickupable());
            AddComponent(new CastShadow());
            AddComponent(new MobRenderer(Ressources.ImgChicken));
            AddComponent(new Flammable());

            AddComponent(new Move());
            AddComponent(new Agent(new BehaviorAnimal() { NaturalEnvironment = { TILES.GRASS }, MoveSpeedWandering = 0.5f }));
            AddComponent(new Collider(new Rectangle(-4, -4, 8, 8)));
            AddComponent(new Health(3));
            AddComponent(new Pushable());
        }
    }
}