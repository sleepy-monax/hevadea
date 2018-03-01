using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Component;
using Hevadea.Game.Entities.Component.Ai;
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

            Components.Add(new Move());
            Components.Add(new Health(10));
            Components.Add(new Attack());
            Components.Add(new Swim());
            Components.Add(new Energy());
            Components.Add(new NpcRender(new Sprite(Ressources.TileCreatures, 2, new Point(16, 32))));

            var aiAgent = new Agent<GenericAgentStates>()
            {
                States =
                {
                    { GenericAgentStates.Idle, null }
                }
            };
            
            Components.Add(aiAgent);
        }


        public override bool IsBlocking(Entity entity)
        {
            return true;
        }
    }
}