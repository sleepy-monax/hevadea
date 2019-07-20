using Hevadea.Entities.Components;
using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.UI;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hevadea.Scenes.Menus
{
    public class MenuInGame : Menu
    {
        public MenuInGame(GameState gameState) : base(gameState)
        {
            var btnMinimap = new WidgetSprite
            {
                Anchor = Anchor.TopRight,
                Origine = Anchor.TopRight,
                Sprite = new Sprite(Resources.TileIcons, new Point(5, 4)),
                UnitOffset = new Point(-16, 16),
            };

            var playerStats = new WidgetPlayerStats(GameState.LocalPlayer.Entity)
            {
                Dock = Dock.Top,
                UnitBound = new Rectangle(0, 0, 320, 64),
            };

            var minimapPanel = new WidgetFancyPanel()
            {
                Enabled = false,
                Anchor = Anchor.TopRight,
                Origine = Anchor.TopRight,
                UnitBound = new Rectangle(0, 0, 320, 320),
                UnitOffset = new Point(-16, 16),
                Padding = new Spacing(0),
                Content = new WidgetMinimap(gameState),
            };

            var hotBar = new WidgetHotBar(GameState.LocalPlayer.Entity.GetComponent<ComponentInventory>().Content)
            {
                Dock = Dock.Bottom,
                UnitOffset = new Point(0, -16)
            };

            Content = new LayoutDock
            {
                Children =
                {
                    playerStats, minimapPanel, btnMinimap, hotBar,

                    new WidgetButton
                        {
                            Text = "Inventory",
                            Origine = Anchor.BottomLeft,
                            Anchor = Anchor.BottomLeft,
                            UnitOffset = new Point(16, -16)
                        }
                        .RegisterMouseClickEvent(
                            (sender) => { GameState.CurrentMenu = new MenuGamePaused(GameState); })
                },
                Padding = new Spacing(16)
            };

            btnMinimap.MouseClick += (sender) =>
            {
                minimapPanel.Toggle();
                minimapPanel.RefreshLayout();
                sender.Disable();
            };
            minimapPanel.MouseClick += (sender) =>
            {
                sender.Toggle();
                btnMinimap.Enable();
            };
        }

        public override void Update(GameTime gameTime)
        {
            if (Rise.Input.KeyTyped(Keys.Escape))
            {
                GameState.CurrentMenu = new MenuGamePaused(GameState);
            }

            base.Update(gameTime);
        }
    }
}