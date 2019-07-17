using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.MainMenu.Tabs
{
    public class TabOption : Tab
    {
        public TabOption()
        {
            Icon = new Sprite(Ressources.TileIcons, new Point(2, 4));

            Content = new LayoutDock
            {
                Padding = new Margins(16),
                Childrens =
                {
                    new WidgetButton{ Text = "Exit", Dock = Dock.Bottom }
                    .RegisterMouseClickEvent( (sender) => Rise.Platform.Stop())
                },
            };
        }
    }
}