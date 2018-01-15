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

            HoldingItem = ITEMS.WOOD_LOG;

            Components.Add(new HealthComponent(20));
            Components.Add(new AttackComponent(1));
            Components.Add(new EnergyComponent());
            Components.Add(new NpcRenderComponent(new Sprite(Ressources.tile_creatures, 1, new Point(16, 32))));
            Components.Add(new InventoryComponent(512));
            Components.Add(new InteractComponent());
            Components.Add(new LightComponent { On = true, Color = Color.Orange * 0.50f, Power = 72 });
            Components.Add(new MoveComponent());
        }
        
        public override bool IsBlocking(Entity entity)
        {
            return true;
        }
    }
}