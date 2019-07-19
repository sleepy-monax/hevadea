using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.UI;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Tabs
{
    public class TabOption : Tab
    {
        public TabOption()
        {
            Icon = new Sprite(Resources.TileIcons, new Point(2, 4));

            Content = new LayoutDock
            {
                Padding = new Spacing(16),
                Children =
                {
                    new WidgetButton {Text = "Exit", Dock = Dock.Bottom}
                        .RegisterMouseClickEvent((sender) => Rise.Platform.Stop())
                },
            };
        }
    }
}