using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Scenes.Widgets;
using Maker.Rise.UI.Widgets;
using Maker.Rise.UI.Widgets.Containers;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Scenes.Menus
{
    public class ChestMenu : Menu
    {
        public ChestMenu(Entity entity, Entity chest, GameManager game) : base(game)
        {
            PauseGame = true;
            
            var inventoryA = new InventoryWidget(entity.Components.Get<Inventory>().Content)
                {Padding = new Padding(4, 4), Dock = Dock.Fill};
            
            var inventoryB = new InventoryWidget(chest.Components.Get<Inventory>().Content)
                {Padding = new Padding(4, 4), Dock = Dock.Fill};
            
            inventoryA.MouseClick += sender =>
            {
                Item item = inventoryA.SelectedItem;
                if (item != null && inventoryB.Content.GetFreeSpace() >= 1)
                {
                    inventoryA.Content.Remove(item, 1);
                    inventoryB.Content.Add(item, 1);
                }
            };
            
            inventoryB.MouseClick += sender =>
            {
                Item item = inventoryB.SelectedItem;
                if (item != null && inventoryA.Content.GetFreeSpace() >= 1)
                {
                    inventoryB.Content.Remove(item, 1);
                    inventoryA.Content.Add(item, 1);
                }
            };

            Content = GUIHelper.CreateSplitContainer(new Rectangle(0, 0, 64, 64), "Inventory", inventoryA, "Chest", inventoryB);
        }
    }
}