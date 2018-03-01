using Hevadea.Framework;
using Hevadea.Framework.UI;
using Hevadea.Game;
using Hevadea.Game.Entities;
using Hevadea.Game.Entities.Component;
using Hevadea.Game.Items;
using Hevadea.Scenes.Widgets;
using Maker.Rise;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hevadea.Scenes.Menus
{
    public class ChestMenu : Menu
    {
        private readonly InventoryWidget _inventoryA;
        private readonly InventoryWidget _inventoryB;
        
        public ChestMenu(Entity entity, Entity chest, GameManager game) : base(game)
        {
            PauseGame = true;
            
            _inventoryA = new InventoryWidget(entity.Components.Get<Inventory>().Content) {Padding = new Padding(4, 4), Dock = Dock.Fill};
            _inventoryB = new InventoryWidget(chest.Components.Get<Inventory>().Content) {Padding = new Padding(4, 4), Dock = Dock.Fill};
            
            _inventoryA.MouseClick += Tranfer;
            _inventoryB.MouseClick += Tranfer;

            Content = GuiHelper.CreateSplitContainer(new Rectangle(0, 0, 64, 64), "Inventory", _inventoryA, "Chest", _inventoryB);
        }

        private void Tranfer(Widget sender)
        {
            
            InventoryWidget invA = (InventoryWidget)sender;
            InventoryWidget invB = (InventoryWidget)sender == _inventoryA ? _inventoryB : _inventoryA;
            
            Item item = invA.SelectedItem;
            
            if (Rise.Input.KeyDown(Keys.LeftShift))
            {
                while (invA.Content.Count(item) > 0 && invB.Content.HasFreeSlot())
                {
                    invA.Content.Remove(item, 1);
                    invB.Content.Add(item, 1);
                }
            }
            else
            {
                if (item != null && invB.Content.HasFreeSlot())
                {
                    invA.Content.Remove(item, 1);
                    invB.Content.Add(item, 1);
                }
            }
        }
    }
}