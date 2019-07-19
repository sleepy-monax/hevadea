using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.UI;
using Hevadea.Scenes.Menus;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Tabs
{
    public class MinimapTab : InventoryTab
    {
        public MinimapTab(GameState gameState) : base(gameState)
        {
            Icon = new Sprite(Resources.TileIcons, new Point(5, 4));

            var title = new WidgetLabel
            {
                Text = "Maps",
                Font = Resources.FontAlagard,
                Dock = Dock.Top
            };

            var minimap = new WidgetMinimap(gameState)
            {
                Dock = Dock.Fill
            };

            Content = new LayoutDock
            {
                Padding = new Spacing(16),
                Children = {title, minimap}
            };
        }
    }
}