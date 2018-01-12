using Maker.Hevadea.Game.Entities.Component.Interaction;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.Entities.Component.Render;
using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Game.Registry;
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
            Origin = new Point(4, 8);
            Light.On = true;
            Light.Color = Color.Orange * 0.50f;
            Light.Power = 72;
            HoldingItem = ITEMS.WOOD_LOG;

            AddComponent(new MoveComponent());
            AddComponent(new HealthComponent(20));
            AddComponent(new AttackComponent(1));
            AddComponent(new NpcRenderComponent(new Sprite(Ressources.tile_creatures, 1, new Point(16, 32))));
            AddComponent(new InventoryComponent(512));
            AddComponent(new InteractComponent());
        }
        
        public override bool IsBlocking(Entity entity)
        {
            return true;
        }
    }
}