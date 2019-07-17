using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Menus.Tabs
{
    public class MinimapTab : InventoryTab
    {
        public MinimapTab(GameState gameState) : base(gameState)
        {
            Icon = new Sprite(Ressources.TileIcons, new Point(5, 4));

            var title = new WidgetLabel
            {
                Text = "Maps",
                Font = Ressources.FontAlagard,
                Dock = Dock.Top
            };

            var minimap = new WidgetMinimap(gameState)
            {
                Dock = Dock.Fill
            };

            Content = new LayoutDock
            {
                Padding = new Margins(16),
                Childrens = { title, minimap }
            };
        }
    }
}