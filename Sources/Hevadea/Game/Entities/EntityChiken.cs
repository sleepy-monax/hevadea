using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components.Ai;
using Hevadea.Game.Entities.Components.Ai.Behaviors;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Entities.Components.Render;
using Hevadea.Game.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities
{
    public class EntityChiken : Entity
    {
        public EntityChiken()
        {
            AddComponent(new Move());
            AddComponent(new Health(3));
            AddComponent(new NpcRender(new Sprite(Ressources.TileCreatures, new Point(4,0), new Point(42, 128), new Point(16, 32))));
            AddComponent(new Colider(new Rectangle(-4, -4, 8, 8)));
            AddComponent(new Pushable() { CanBePushBy = { ENTITIES.PLAYER } });
            AddComponent(new Agent() { Behavior = new BehaviorAnimal() { NaturalEnvironment = { TILES.GRASS }, MoveSpeed = 0.5f } });
            AddComponent(new Pickupable(new Sprite(Ressources.TileEntities, new Point(12, 0))));
            AddComponent(new Burnable(1f));
        }
    }
}
