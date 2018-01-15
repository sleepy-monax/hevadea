using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.UI;

using Maker.Hevadea.Scenes;
using Maker.Rise.Enum;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Menus
{
    public class ChestMenu : Menu
    {
        public InventoryUi inv;
        public InventoryUi chestInv;
        
        public ChestMenu(Entity entity, ChestEntity chest, World world, GameScene game) : base(world, game)
        {
            Layout = LayoutMode.Horizontal;
            var LeftPanel = new Panel();
            var RightPanel = new Panel();

            inv = new InventoryUi(entity.Components.Get<InventoryComponent>().Inventory)
            {
                Dock = Dock.Fill
            };
            
            chestInv = new InventoryUi(chest.Components.Get<InventoryComponent>().Inventory)
            {
                Dock = Dock.Fill
            };
            
            inv.OnMouseClick += 
            (sender, args) =>
            {
                if (inv.SelectedItem != null && chestInv.Inventory.HasFreeSlot())
                {
                    inv.Inventory.Remove(inv.SelectedItem, 1);
                    chestInv.Inventory.Add(inv.SelectedItem);
                }
            };
            
            chestInv.OnMouseClick += 
            (sender, args) =>
            {
                if (chestInv.SelectedItem != null && inv.Inventory.HasFreeSlot())
                {
                    chestInv.Inventory.Remove(chestInv.SelectedItem, 1);
                    inv.Inventory.Add(chestInv.SelectedItem);
                }
            };
            
            LeftPanel.AddChild(new Label { Dock = Dock.Top, Text = "Inventory", Bound = new Rectangle(0,0,32,32)});
            LeftPanel.AddChild(inv);
            
            RightPanel.AddChild(new Label { Dock = Dock.Top, Text = "Chest", Bound = new Rectangle(0, 0, 32, 32) });
            RightPanel.AddChild(chestInv);
            
            AddChild(LeftPanel);
            AddChild(RightPanel);
            
            PauseGame = true;
        }
    }
}