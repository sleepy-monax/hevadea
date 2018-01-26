using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.UI;
using Maker.Rise.Enums;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Menus
{
    public class InventoryMenu : Menu
    {
        public InventoryUi inv;
        public CraftingControl craft;
       

        public InventoryMenu(PlayerEntity entity, GameManager game) : base(game)
        {
            Layout = LayoutMode.Horizontal;
            var LeftPanel = new Panel() { Padding = new Padding(16), Color = Color.Black * 0.1f};
            var RightPanel = new Panel() { Padding = new Padding(16), Color = Color.Black * 0.1f };

            inv = new InventoryUi(entity.Components.Get<Inventory>().Content)
            {
                Dock = Dock.Fill
            };

            inv.OnMouseClick +=
            (sender, args) =>
            {
                if (inv.SelectedItem != null )
                {
                    entity.HoldingItem = inv.SelectedItem;
                }
            };

            craft = new CraftingControl(entity.Components.Get<Inventory>().Content)
            {
                Dock = Dock.Fill
            };

            PauseGame = true;
            LeftPanel.AddChild(new Label(){Dock = Dock.Top, Text = "Inventory", Bound = new Rectangle(0,0,32,48)});
            LeftPanel.AddChild(inv);
            RightPanel.AddChild(new Label() { Dock = Dock.Top, Text = "Hand Crafting", Bound = new Rectangle(0, 0, 32, 48) });
            RightPanel.AddChild(new PlayerInfoPanel(Game.Player) { Dock = Dock.Bottom, Bound = new Rectangle(64, 64, 64, 64) });
            RightPanel.AddChild(craft);
            AddChild(LeftPanel);
            AddChild(RightPanel);
        }

    }
}