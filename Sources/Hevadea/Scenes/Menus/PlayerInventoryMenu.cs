using Hevadea.Craftings;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Scenes.Menus.Tabs;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.GameObjects.Items;

namespace Hevadea.Scenes.Menus
{
    
    public abstract class InventoryTab : Tab
    {
        public GameManager.GameManager Game { get; }

        public InventoryTab(GameManager.GameManager game)
        {
            Game = game;
        }
    }

    public class PlayerInventoryMenu : Menu
    {
        private WidgetItemContainer _inventory;
        private CraftingTab         _crafting;
        private WidgetTabContainer  _sideMenu;

        public PlayerInventoryMenu(GameManager.GameManager game, List<Recipe> recipes) : base(game)
        {
            InitializeComponents();
        }

        public PlayerInventoryMenu(GameManager.GameManager game, ItemStorage container, string containerName) : base(game)
        {

            InitializeComponents();

        }

        public PlayerInventoryMenu(GameManager.GameManager game) : base(game)
        {
            InitializeComponents();
        }

        public void InitializeComponents(InventoryTab mainTab = null)
        {
            PauseGame = true;

            _inventory = new WidgetItemContainer(Game.MainPlayer.GetComponent<Inventory>().Content);
            _crafting = new CraftingTab(Game);

            _inventory.Dock = Dock.Fill;

            _inventory.MouseClick += (sender) =>
            {
                _inventory.HighlightedItem = _inventory.SelectedItem;
                Game.MainPlayer.HoldingItem = _inventory.SelectedItem;
            };

            var inventoryPanel = new WidgetFancyPanel
            {
                Content = new DockContainer
                {
                    Childrens =
                    {
                        new Label {Text = "Inventory", Font = Ressources.FontAlagard, Dock = Dock.Top},
                        _inventory,
                    }
                }
            };

            _sideMenu = new WidgetTabContainer
            {
                Padding = new Padding(16, 16),
                Tabs =
                {
                    _crafting,
                    new ContainerTab()
                    {
                        Icon = new Sprite(Ressources.TileIcons, new Point(0, 2)),
                        Childrens =
                        {
                            new PlayerStatesTab(),
                            new EquipmentTab(),
                        }
                    },
                    new SaveTab(Game),
                }
            };

            if (mainTab != null)
            {
                _sideMenu.Tabs.Insert(0, mainTab);
            }

            Content = new TileContainer
            {
                Flow = FlowDirection.RightToLeft,
                Childrens = { _sideMenu, inventoryPanel }
            };
        }
    }
}
