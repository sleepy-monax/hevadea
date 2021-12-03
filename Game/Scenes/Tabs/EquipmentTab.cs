using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.UI;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Tabs
{
    public class EquipmentTab : Tab
    {
        public EquipmentTab()
        {
            Icon = new Sprite(Resources.TileIcons, new Point(4, 4));
            Content = new LayoutDock()
            {
                Padding = new Spacing(16),
                Children =
                {
                    new WidgetLabel
                    {
                        Text = "Equipment",
                        Font = Resources.FontAlagard,
                        Dock = Dock.Top
                    },
                }
            };
        }
    }
}