using Hevadea.Framework;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.GameObjects.Items;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hevadea.Scenes.Menus
{
    public class MenuChest : Menu
    {
        private readonly WidgetItemContainer _inventoryA;
        private readonly WidgetItemContainer _inventoryB;
        
        public MenuChest(Entity entity, Entity chest, GameManager.GameManager game) : base(game)
        {
            PauseGame = true;
            
            _inventoryA = new WidgetItemContainer(entity.GetComponent<Inventory>().Content) {Padding = new Padding(4, 4), Dock = Dock.Fill};
            _inventoryB = new WidgetItemContainer(chest.GetComponent<Inventory>().Content) {Padding = new Padding(4, 4), Dock = Dock.Fill};
            
            _inventoryA.MouseClick += Tranfer;
            _inventoryB.MouseClick += Tranfer;

            Content = GuiFactory.CreateSplitContainer(new Rectangle(0, 0, 64, 64), "Inventory", _inventoryA, "Chest", new DockContainer()
            {
                Childrens = {                             new Button
                                { Font  = Ressources.FontRomulus, Text = "Back", Padding = new Padding(4) }
                                .RegisterMouseClickEvent((sender) => {Game.CurrentMenu = new MenuInGame(Game);}),_inventoryB },
                Dock = Dock.Fill
            });
        }

        private void Tranfer(Widget sender)
        {
            
            WidgetItemContainer invA = (WidgetItemContainer)sender;
            WidgetItemContainer invB = (WidgetItemContainer)sender == _inventoryA ? _inventoryB : _inventoryA;
            
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