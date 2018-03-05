using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Component;
using Hevadea.Game.Entities.Component.Ai;
using Hevadea.Game.Entities.Component.Attributes;
using Hevadea.Game.Entities.Component.Render;
using Microsoft.Xna.Framework;

namespace Hevadea.Game.Entities.Creatures
{
    public class ZombieEntity : Entity
    {
        public ZombieEntity()
        {
            Width = 8;
            Height = 8;

            Origin = new Point(4, 4);

            Add(new Move());
            Add(new Health(10));
            Add(new Attack());
            Add(new Swim());
            Add(new Energy());
            Add(new NpcRender(new Sprite(Ressources.TileCreatures, 2, new Point(16, 32))));            
            Add(new Agent());
        }


        public override bool IsBlocking(Entity entity)
        {
            return true;
        }
    }
}