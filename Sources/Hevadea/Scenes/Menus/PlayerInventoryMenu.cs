using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Game;
using Hevadea.Game.Entities.Components;
using Hevadea.Scenes.Widgets;

namespace Hevadea.Scenes.Menus
{
    public class PlayerInventoryMenu : Menu
    {

        public PlayerInventoryMenu(GameManager game) : base(game)
        {
            var inventory = new WidgetItemContainer(game.MainPlayer.GetComponent<Inventory>().Content)
            {
                Dock = Dock.Fill,
            };

            var inventoryPanel = new WidgetFancyPanel
            {
                Content = new DockContainer
                {
                    Childrens =
                    {
                        new Label {Text = "Inventory", Font = Ressources.FontAlagard, Dock = Dock.Top},
                        inventory,
                    }
                }
            };

            var tabContainer = new WidgetTabContainer
            {

            };

            Content = new TileContainer
            {
                Flow = FlowDirection.LeftToRight,
                Childrens = { tabContainer, inventoryPanel }
            };
        }
    }
}
