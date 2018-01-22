using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.UI;
using Maker.Rise.Enum;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Menus
{
    public class InventoryMenu : Menu
    {
        public InventoryUi inv;
        public CraftingUi craft;
       

        public InventoryMenu(Entity entity, GameManager game) : base(game)
        {
            Layout = LayoutMode.Horizontal;
            var LeftPanel = new Panel();
            var RightPanel = new Panel();

            inv = new InventoryUi(entity.Components.Get<Inventory>().Content)
            {
                Dock = Dock.Fill
            };

            craft = new CraftingUi(entity.Components.Get<Inventory>().Content)
            {
                Dock = Dock.Fill
            };

            PauseGame = true;
            LeftPanel.AddChild(new Label(){Dock = Dock.Top, Text = "Inventory", Bound = new Rectangle(0,0,32,32)});
            LeftPanel.AddChild(inv);
            RightPanel.AddChild(new Label() { Dock = Dock.Top, Text = "Hand Crafting", Bound = new Rectangle(0, 0, 32, 32) });
            RightPanel.AddChild(craft);
            AddChild(LeftPanel);
            AddChild(RightPanel);
        }

    }
}