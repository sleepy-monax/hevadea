using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.Entities.Component.Render;
using Maker.Hevadea.Game.Items;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Entities
{
    public class Player : Mob
    {
        public Item HoldingItem = new Item();

        public Player()
        {
            Width = 8;
            Height = 8;
            Light.On = true;
            Light.Color = Color.Orange * 0.50f;
            Light.Power = 72;

            Health = MaxHealth = 20;
            IsInvincible = false;

            AddComponent(new MoveComponent());
            AddComponent(new InventoryComponent(16));
            AddComponent(new NpcRenderComponent(new Sprite(Ressources.tile_creatures, 0, new Point(16, 16))) {Priority = 1});
        }
    }
}