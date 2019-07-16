using Hevadea.Entities.Components;
using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.Platform;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.Extension;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Menus
{
    public class MenuInGame : Menu
    {
        public MenuInGame(GameState gameState) : base(gameState)
        {
            var btnMinimap = new SpriteButton
            {
                Anchor = Anchor.TopRight,
                Origine = Anchor.TopRight,
                Sprite = new Sprite(Ressources.TileIcons, new Point(5, 4)),
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
                Padding = new Margins(0),
                Content = new WidgetMinimap(gameState),
            };

            var hotBar = new WidgetHotBar(GameState.LocalPlayer.Entity.GetComponent<Inventory>().Content)
            {
                Dock = Dock.Bottom,
                UnitOffset = new Point(0, -16)
            };

            Content = new Container
            {
                Childrens =
                {
                    playerStats, minimapPanel, btnMinimap, hotBar,

                    new Button
                        {
                            Text = "Inventory",
                            Origine = Anchor.BottomLeft,
                            Anchor = Anchor.BottomLeft,
                            UnitOffset = new Point(16,-16)
                        }
                    .RegisterMouseClickEvent(
                        (sender) =>
                        {
                            GameState.CurrentMenu = new PlayerInventoryMenu(GameState);
                        })
                },
                Padding = new Margins(16)
            };
            
            btnMinimap.MouseClick += (sender) => { minimapPanel.Toggle(); minimapPanel.RefreshLayout(); sender.Disable(); };
            minimapPanel.MouseClick += (sender) => { sender.Toggle(); btnMinimap.Enable(); };
        }
    }
}