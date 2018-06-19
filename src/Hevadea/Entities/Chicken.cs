using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.Ai;
using Hevadea.Entities.Components.Ai.Behaviors;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.Render;
using Hevadea.Entities.Components.States;
using Microsoft.Xna.Framework;

namespace Hevadea.Entities
{
    public class Chicken : Entity
    {
        public Chicken()
        {
            AddComponent(new Agent(new BehaviorAnimal() { NaturalEnvironment = { TILES.GRASS }, MoveSpeedWandering = 0.5f }));
            AddComponent(new Burnable(1f));
            AddComponent(new Colider(new Rectangle(-4, -4, 8, 8)));
            AddComponent(new Health(3));
            AddComponent(new Move());
            AddComponent(new NpcRender(new Sprite(Ressources.TileCreatures, new Point(4, 0), new Point(42, 128), new Point(16, 32))));
            AddComponent(new Pickupable(new Sprite(Ressources.TileEntities, new Point(12, 0))));
            AddComponent(new Pushable());
            AddComponent(new Shadow());
        }
    }
}