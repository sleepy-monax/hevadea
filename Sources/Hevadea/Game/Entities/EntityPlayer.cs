using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Entities.Components.Interaction;
using Hevadea.Game.Entities.Components.Render;
using Hevadea.Game.Items;
using Microsoft.Xna.Framework;

namespace Hevadea.Game.Entities
{
    public class EntityPlayer : Entity
    {
        public EntityPlayer()
        {
            HoldingItem = null;

            Attach(new Health(20){ ShowHealthBar = false });
            Attach(new Attack());
            Attach(new Energy());
            Attach(new NpcRender(new Sprite(Ressources.TileCreatures, 0, new Point(16, 32))));
            Attach(new Inventory(64) {AlowPickUp = true});
            Attach(new Interact());
            Attach(new Light {On = true, Color = Color.White * 0.50f, Power = 64});
            Attach(new Move());
            Attach(new Swim());
            Attach(new Pushable());
            Attach(new Colider(new Rectangle(-2, -2, 4, 4)));
        }

        public Item HoldingItem { get; set; }

        public override bool IsBlocking(Entity entity)
        {
            return !(entity is ItemEntity);
        }
    }
}