using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.GameObjects.Entities.Components.Actions;
using Hevadea.GameObjects.Entities.Components.Ai;
using Hevadea.GameObjects.Entities.Components.Ai.Behaviors;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Entities.Components.Render;
using Hevadea.GameObjects.Entities.Components.States;
using Hevadea.Registry;
using Microsoft.Xna.Framework;

namespace Hevadea.GameObjects.Entities.Blueprints.Legacy
{
    public class EntityZombie : Entity
    {
        public EntityZombie()
        {
            AddComponent(new Move());
            AddComponent(new Health(10));
            AddComponent(new Attack());
            AddComponent(new Swim());
            AddComponent(new Energy());
            AddComponent(new NpcRender(new Sprite(Ressources.TileCreatures, 2, new Point(16, 32))));            
            AddComponent(new Agent{ Behavior = new BehaviorEnemy
            {
                MoveSpeedAgro = 0.75f,
                MoveSpeedWandering = 0.5f,
                NaturalEnvironment = { TILES.DIRT, TILES.GRASS, TILES.SAND, TILES.WOOD_FLOOR }
            }});
            AddComponent(new Pushable { CanBePushBy = { EntityFactory.PLAYER } });
            AddComponent(new Colider(new Rectangle(-2, -2, 4, 4)));
            AddComponent(new Burnable(1f));
        }
    }
}