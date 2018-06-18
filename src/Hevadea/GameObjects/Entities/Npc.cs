using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.GameObjects.Entities.Components.Actions;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Entities.Components.Render;
using Hevadea.GameObjects.Entities.Components.States;
using Microsoft.Xna.Framework;

namespace Hevadea.GameObjects.Entities
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