using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Scenes.Widgets;
using Maker.Rise;
using Maker.Rise.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Maker.Hevadea.Scenes.Menus
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

            Content = GUIHelper.CreateSplitContainer(new Rectangle(0, 0, 64, 64), "Inventory", _inventoryA, "Chest", _inventoryB);
        }

        private void Tranfer(Widget sender)
        {
            
            InventoryWidget invA = (InventoryWidget)sender;
            InventoryWidget invB = (InventoryWidget)sender == _inventoryA ? _inventoryB : _inventoryA;
            
            Item item = invA.SelectedItem;
            
            if (Engine.Input.KeyDown(Keys.LeftShift))
            {
                while (invA.Content.Count(item) > 0 && invB.Content.GetFreeSpace() >= 1)
                {
                    invA.Content.Remove(item, 1);
                    invB.Content.Add(item, 1);
                }
            }
            else
            {
                if (item != null && invB.Content.GetFreeSpace() >= 1)
                {
                    invA.Content.Remove(item, 1);
                    invB.Content.Add(item, 1);
                }
            }
        }
    }
}