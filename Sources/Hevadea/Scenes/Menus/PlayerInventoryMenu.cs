using Hevadea.Entities.Components;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Scenes.Menus.Tabs;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Menus
{
    
    public abstract class InventoryTab : Tab
    {
        public GameManager Game { get; }

        public InventoryTab(GameManager game)
        {
            Game = game;
        }
    }

    public class PlayerInventoryMenu : Menu
    {
        WidgetItemContainer inventory;
        CraftingTab         crafting;

        public PlayerInventoryMenu(GameManager game) : base(game)
        {

            InitializeComponents();

        }

        public void InitializeComponents()
        {
            PauseGame = true;

            inventory = new WidgetItemContainer(Game.MainPlayer.GetComponent<Inventory>().Content);
            crafting = new CraftingTab(Game);

            inventory.Dock = Dock.Fill;

            inventory.MouseClick += (sender) =>
            {
                inventory.HighlightedItem = inventory.SelectedItem;
                Game.MainPlayer.HoldingItem = inventory.SelectedItem;
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
                Padding = new Padding(16, 16),
                Tabs =
                {
                    new ContainerTab()
                    {
                        Icon = new Sprite(Ressources.TileIcons, new Point(0, 2)),
                        Childrens =
                        {
                            new PlayerStatesTab(),
                            new EquipmentTab(),
                        }
                    },
                    new CraftingTab(Game),
                    new SaveTab(Game),
                }
            };

            Content = new TileContainer
            {
                Flow = FlowDirection.RightToLeft,
                Childrens = { tabContainer, inventoryPanel }
            };
        }
    }
}
