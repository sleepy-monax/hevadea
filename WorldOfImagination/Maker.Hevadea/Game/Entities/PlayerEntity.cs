using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.Entities.Component.Render;
using Maker.Hevadea.Game.Items;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Entities
{
    public class PlayerEntity : Entity
    {
        public Item HoldingItem = new Item();

        public PlayerEntity()
        {
            Width = 8;
            Height = 8;
            Light.On = true;
            Light.Color = Color.Orange * 0.50f;
            Light.Power = 72;

            AddComponent(new MoveComponent());
            AddComponent(new HealthComponent(20));
            AddComponent(new AttackComponent(1));
            AddComponent(new NpcRenderComponent(new Sprite(Ressources.tile_creatures, 0, new Point(16, 32))));
            AddComponent(new InventoryComponent(16));
        }
    }
}