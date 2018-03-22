using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Registry;
using Hevadea.GameObjects.Entities.Components.Ai;
using Hevadea.GameObjects.Entities.Components.Ai.Behaviors;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Entities.Components.Render;
using Microsoft.Xna.Framework;

namespace Hevadea.GameObjects.Entities
{
    public class EntityChiken : Entity
    {
        public EntityChiken()
        {
            Attach(new Move());
            Attach(new Health(3));
            Attach(new Colider(new Rectangle(-4, -4, 8, 8)));
            Attach(new Pushable() { CanBePushBy = { ENTITIES.PLAYER } });
            Attach(new Agent() { Behavior = new BehaviorAnimal() { NaturalEnvironment = { TILES.GRASS }, MoveSpeed = 0.5f } });
            Attach(new Pickupable(new Sprite(Ressources.TileEntities, new Point(12, 0))));
            Attach(new Burnable(1f));
            Attach(new NpcRender(new Sprite(Ressources.TileCreatures, new Point(4,0), new Point(42, 128), new Point(16, 32))));
        }
    }
}
