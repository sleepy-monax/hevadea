using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Entities.Component.Render;
using Maker.Hevadea.Game.Items;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Entities
{
    public class PlayerEntity : Entity
    {
        public Item HoldingItem { get; set; }

        public PlayerEntity()
        {
            Width = 8;
            Height = 8;
            Origin = new Point(4, 7);

            HoldingItem = null;

            Components.Add(new Health(20));
            Components.Add(new Attack());
            Components.Add(new Energy());
            Components.Add(new NpcRender(new Sprite(Ressources.tile_creatures, 0, new Point(16, 32))));
            Components.Add(new Inventory(512) { AlowPickUp = true });
            Components.Add(new Interact());
            Components.Add(new Light { On = true, Color = Color.White * 0.30f, Power = 48 });
            Components.Add(new Move());
        }
        
        public override bool IsBlocking(Entity entity)
        {
            return true;
        }
    }
}