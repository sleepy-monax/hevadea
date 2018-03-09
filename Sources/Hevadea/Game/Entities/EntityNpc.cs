using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components;
using Hevadea.Game.Entities.Components.Interaction;
using Hevadea.Game.Entities.Components.Render;
using Microsoft.Xna.Framework;

namespace Hevadea.Game.Entities
{
    public class EntityNpc : Entity
    {
        public EntityNpc()
        {
            Attachs
            (
                new NpcRender(new Sprite(Ressources.TileCreatures, 2, new Point(16, 32))),
                new Interact(),
                new Attack(),
                new Target()
            );
        }
    }
}