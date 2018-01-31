using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.UI;
using Maker.Hevadea.UI;
using Maker.Rise.Enums;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Menus
{
    public class ChestMenu : Menu
    {
        public InventoryUi inv;
        public InventoryUi chestInv;
        
        public ChestMenu(Entity entity, ChestEntity chest, GameManager game) : base(game)
        {
            Layout = LayoutMode.Horizontal;
            var LeftPanel = new PrettyPanel { Padding = new Padding(8) };
            var RightPanel = new PrettyPanel { Padding = new Padding(8) };

            inv = new InventoryUi(entity.Components.Get<Inventory>().Content)
            {
                Dock = Dock.Fill,
                Padding = new Padding(8)
            };
            
            chestInv = new InventoryUi(chest.Components.Get<Inventory>().Content)
            {
                Dock = Dock.Fill,
                Padding = new Padding(8)
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
            
            LeftPanel.AddChild(new Label { Font = Ressources.fontAlagard, Dock = Dock.Top, Text = "Inventory", Bound = new Rectangle(0,0,48,48)});
            LeftPanel.AddChild(inv);
            
            RightPanel.AddChild(new Label { Font = Ressources.fontAlagard, Dock = Dock.Top, Text = "Chest", Bound = new Rectangle(0, 0, 48, 48) });
            RightPanel.AddChild(chestInv);
            
            AddChild(LeftPanel);
            AddChild(RightPanel);
            
            PauseGame = true;
        }
    }
}