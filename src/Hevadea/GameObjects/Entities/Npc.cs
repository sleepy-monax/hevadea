using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.GameObjects.Entities.Components.Actions;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Entities.Components.Render;
using Microsoft.Xna.Framework;

namespace Hevadea.GameObjects.Entities
{
    public class Npc : Entity
    {
        public Npc()
        {
            AddComponent(new NpcRender(new Sprite(Ressources.TileCreatures, 2, new Point(16, 32))));
            AddComponent(new Interact());
            AddComponent(new Attack());
            AddComponent(new Burnable(1f));
        }
    }
}