using Hevadea.Entities.Components;
using Hevadea.Entities.Components.AI;
using Hevadea.Entities.Components.AI.Behaviors;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Items;
using Hevadea.Registry;
using Microsoft.Xna.Framework;

namespace Hevadea.Entities
{
    public class Fish : Entity
    {
        public Fish()
        {
            AddComponent(new RendererSprite {Sprite = new Sprite(Resources.TileEntities, new Point(11, 0))});
            AddComponent(
                new Agent(new BehaviorAnimal() {NaturalEnvironment = {TILES.WATER}, MoveSpeedWandering = 0.5f}));
            AddComponent(new ComponentBreakable());
            AddComponent(new ComponentCollider(new Rectangle(-4, -4, 8, 8)));
            AddComponent(new ComponentDropable {Items = {new Drop(ITEMS.RAW_FISH, 1f, 1, 1)}});
            AddComponent(new ComponentMove());
            AddComponent(new Pushable());
            AddComponent(new ComponentSwim {IsSwimingPainfull = false, ShowAnimation = false});
        }
    }
}