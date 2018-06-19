using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.Ai;
using Hevadea.Entities.Components.Ai.Behaviors;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.Render;
using Hevadea.Entities.Components.States;
using Hevadea.Registry;
using Microsoft.Xna.Framework;

namespace Hevadea.Entities
{
    public class Zombie : Entity
    {
        public Zombie()
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
                }
            }));

            AddComponent(new Attack());
            AddComponent(new Burnable(1f));
            AddComponent(new Colider(new Rectangle(-2, -2, 4, 4)));
            AddComponent(new Energy());
            AddComponent(new Health(10));
            AddComponent(new Move());
            AddComponent(new NpcRender(new Sprite(Ressources.TileCreatures, 2, new Point(16, 32))));
            AddComponent(new Pushable { CanBePushBy = { ENTITIES.PLAYER } });
            AddComponent(new Shadow());
            AddComponent(new Swim());
        }
    }
}