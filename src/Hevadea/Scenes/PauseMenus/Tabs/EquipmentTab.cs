using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Menus.Tabs
{
    public class EquipmentTab : Tab
    {
        public EquipmentTab()
        {
            Icon = new Sprite(Ressources.TileIcons, new Point(4, 4));
            Content = new LayoutDock()
            {
                Padding = new Margins(16),
                Childrens =
                {
                    new WidgetLabel
                    {
                        Text = "Equipment",
                        Font = Ressources.FontAlagard,
                        Dock = Dock.Top
                    },
                }
            };
        }
    }
}