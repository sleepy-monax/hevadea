using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Ai;
using Hevadea.Entities.Components.Ai.Behaviors;
using Hevadea.Entities.Components.Render;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Registry;
using Microsoft.Xna.Framework;

namespace Hevadea.Entities.Blueprints
{
    public class BlueprintCreature : EntityBlueprint
    {
        public bool IsAgressive { get; set; } = false;

        public bool CanSwim { get; set; } = false;
        public bool CanWalkOnLand { get; set; } = true;
        public float Heal { get; set; } = 5f;

        public BlueprintCreature(string name) : base(name)
        {
        }

        public override void AttachComponents(Entity e)
        {
            e.AddComponent(new Move());
            e.AddComponent(new Health(3));
            e.AddComponent(new NpcRender(new Sprite(Ressources.TileCreatures, new Point(4, 0), new Point(42, 128), new Point(16, 32))));
            e.AddComponent(new Colider(new Rectangle(-4, -4, 8, 8)));
            e.AddComponent(new Pushable());
            e.AddComponent(new Agent()
            {
                Behavior = new BehaviorAnimal()
                {
                    NaturalEnvironment =
                    {
                        TILES.GRASS
                    },
                    MoveSpeedWandering = 0.5f
                }
            });
            e.AddComponent(new Pickupable(new Sprite(Ressources.TileEntities, new Point(12, 0))));
            e.AddComponent(new Burnable(1f));
        }
    }
}
