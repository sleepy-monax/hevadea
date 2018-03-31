using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Render;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Microsoft.Xna.Framework;

namespace Hevadea.Entities.Blueprints
{
    public class EntityNpc : Entity
    {
        public EntityNpc()
        {

            AddComponent(new NpcRender(new Sprite(Ressources.TileCreatures, 2, new Point(16, 32))));
            AddComponent(new Interact());
            AddComponent(new Attack());
            AddComponent(new Target());
            AddComponent(new Burnable(1f));
        }
    }
}