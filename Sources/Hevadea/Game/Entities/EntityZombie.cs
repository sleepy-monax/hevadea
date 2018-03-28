using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components.Ai;
using Hevadea.Game.Entities.Components.Ai.Actions;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Entities.Components.Interaction;
using Hevadea.Game.Entities.Components.Render;
using Hevadea.Game.Registry;
using Hevadea.Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Hevadea.Game.Entities.Components.Ai.Behaviors;

namespace Hevadea.Game.Entities
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
            AddComponent(new Agent{Behavior = new BehaviorEnemy()});
            AddComponent(new Pushable { CanBePushBy = { ENTITIES.PLAYER } });
            AddComponent(new Colider(new Rectangle(-2, -2, 4, 4)));
            AddComponent(new Burnable(1f));
        }
    }
}