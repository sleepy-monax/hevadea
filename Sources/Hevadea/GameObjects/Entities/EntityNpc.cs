using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Entities.Components.Interaction;
using Hevadea.GameObjects.Entities.Components.Render;
using Microsoft.Xna.Framework;

namespace Hevadea.GameObjects.Entities
{
    public class EntityNpc : Entity
    {
        public EntityNpc()
        {
            Attach(new NpcRender(new Sprite(Ressources.TileCreatures, 2, new Point(16, 32))));
            Attach(new Interact());
            Attach(new Attack());
            Attach(new Target());
            Attach(new Burnable(1f));
        }
    }
}