using Hevadea.Craftings;
using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.Platform;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.Utils;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.Registry;
using Hevadea.Scenes.Menus.Tabs;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

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
        WidgetItemContainer _inventory;
        CraftingTab _crafting;
      
        public PlayerInventoryMenu(GameManager game) : base(game)
        {
            InitializeComponents();
        }

        public void InitializeComponents()
        {
            PauseGame = true;

            var r = new List<List<Recipe>>();
            foreach (var e in Game.MainPlayer.Level.GetEntitiesOnArea(Game.MainPlayer.X, Game.MainPlayer.Y, GLOBAL.Unit * 3))
            {
                var s = e.GetComponent<CraftingStation>();
                if (s != null)
                {
                    if (!r.Contains(s.Recipies))
                    {
                        r.Add(s.Recipies);
                    }
                }
            }

            var recipies = new List<Recipe>();
            recipies.AddRange(RECIPIES.HandCrafted);
            foreach (var i in r)
            {
                recipies.AddRange(i);
            }

            _inventory = new WidgetItemContainer(Game.MainPlayer.GetComponent<Inventory>().Content);
            _crafting = new CraftingTab(Game, recipies);

            _inventory.Dock = Dock.Fill;

            _inventory.MouseClick += (sender) =>
            {
                _inventory.HighlightedItem = _inventory.SelectedItem;
                Game.MainPlayer.HoldingItem = _inventory.SelectedItem;
            };

            var closeBtn = new SpriteButton()
            {
                Sprite = new Sprite(Ressources.TileGui, new Point(7, 7)),
                UnitBound = new Rectangle(0, 0, 64, 64),
                Anchor = Anchor.TopRight,
                Origine = Anchor.Center
            };

            closeBtn.MouseClick += CloseBtnOnMouseClick;

            WidgetTabContainer _sideMenu = new WidgetTabContainer
            {
                Padding = new BoxElement(32),
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitBound = new Rectangle(0, 0, 600, 720),
                Dock = Rise.Platform.Family == PlatformFamily.Mobile ? Dock.Fill : Dock.None,
                TabAnchore = Rise.Platform.Family == PlatformFamily.Mobile ? TabAnchore.Bottom : TabAnchore.Left,
                Childrens = { closeBtn },

                Tabs =
                {
                    new EquipmentTab(),
                    _crafting,
                    new Tab()
                    {
                        Icon = new Sprite(Ressources.TileIcons, new Point(2, 3)),
                        Content = new Container()
                        {
                            Childrens =
                            {
                                new Label {Text = "Inventory", Font = Ressources.FontAlagard, Dock = Dock.Top},
                                _inventory,
                            }
                        }
                    },
                    new MinimapTab(Game),
                    new SaveTab(Game),
                }
            };

            Content = new Container()
            {
                Childrens = { _sideMenu },
            };
        }

        void CloseBtnOnMouseClick(Widget sender)
        {
            Game.CurrentMenu = new MenuInGame(Game);
        }
    }
}