using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.Render;
using Hevadea.Entities.Components.States;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Microsoft.Xna.Framework;

namespace Hevadea.Entities
{
    public class Npc : Entity
    {
        public Npc()
        {
            AddComponent(new Attack());
            AddComponent(new Burnable(1f));
            AddComponent(new Interact());
            AddComponent(new NpcRender(new Sprite(Ressources.TileCreatures, 2, new Point(16, 32))));
            AddComponent(new Shadow());
        }
    }
}