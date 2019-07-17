using Hevadea.Craftings;
using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.Platform;
using Hevadea.Framework.UI;
using Hevadea.Registry;
using Hevadea.Scenes.Menus.Tabs;
using Hevadea.Scenes.Widgets;
using Hevadea.Systems.InventorySystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.Scenes.Menus
{
    public abstract class InventoryTab : Tab
    {
        public GameState GameState { get; }

        public InventoryTab(GameState gameState)
        {
            GameState = gameState;
        }
    }

    public class PlayerInventoryMenu : Menu
    {
        private WidgetItemContainer _inventory;
        private CraftingTab _crafting;

        public PlayerInventoryMenu(GameState gameState) : base(gameState)
        {
            InitializeComponents();
        }

        public void InitializeComponents()
        {
            PauseGame = true;

            var r = new List<List<Recipe>>();
            foreach (var e in GameState.LocalPlayer.Entity.Level.QueryEntity(GameState.LocalPlayer.Entity.Position, Game.Unit * 3))
            {
                var s = e.GetComponent<CraftingStation>();
                if (s != null)
                {
                    if (!r.Contains(s.Recipes))
                    {
                        r.Add(s.Recipes);
                    }
                }
            }

            var recipies = new List<Recipe>();
            recipies.AddRange(RECIPIES.HandCrafted);
            foreach (var i in r)
            {
                recipies.AddRange(i);
            }

            _inventory = new WidgetItemContainer(GameState.LocalPlayer.Entity.GetComponent<Inventory>().Content);
            _crafting = new CraftingTab(GameState, recipies);

            _inventory.Dock = Dock.Fill;

            _inventory.MouseClick += (sender) =>
            {
                _inventory.HighlightedItem = _inventory.SelectedItem;
                GameState.LocalPlayer.Entity.HoldItem(_inventory.SelectedItem);
            };

            var closeBtn = new WidgetSprite()
            {
                Sprite = new Sprite(Ressources.TileGui, new Point(7, 7)),
                UnitBound = new Rectangle(0, 0, 48, 48),
                Anchor = Anchor.TopRight,
                Origine = Anchor.TopRight,
                UnitOffset = new Point(-24, 24)
            };

            closeBtn.MouseClick += CloseBtnOnMouseClick;

            WidgetTabContainer _sideMenu = new WidgetTabContainer
            {
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitBound = new Rectangle(0, 0, 600, 720),
                Dock = Rise.Platform.Family == PlatformFamily.Mobile ? Dock.Fill : Dock.None,
                TabAnchore = Rise.Platform.Family == PlatformFamily.Mobile ? TabAnchore.Bottom : TabAnchore.Left,

                Tabs =
                {
                    new EquipmentTab(),
                    _crafting,
                    new Tab()
                    {
                        Icon = new Sprite(Ressources.TileIcons, new Point(2, 3)),
                        Content = new LayoutDock()
                        {
                            Padding = new Margins(16),
                            Childrens =
                            {
                                new WidgetLabel {Text = "Inventory", Font = Ressources.FontAlagard, Dock = Dock.Top},
                                _inventory,
                            }
                        }
                    },
                    new MinimapTab(GameState),
                    new SaveTab(GameState),
                }
            };

            Content = new LayoutDock()
            {
                Childrens = { _sideMenu },
            };
        }

        private void CloseBtnOnMouseClick(Widget sender)
        {
            GameState.CurrentMenu = new MenuInGame(GameState);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(Rise.Graphic.GetBound(), Color.Black * 0.5f);
            base.Draw(spriteBatch, gameTime);
        }
    }
}