using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.MainMenu.Tabs
{
    public class TabOption : Tab
    {
        public TabOption()
        {
            Icon = new Sprite(Ressources.TileIcons, new Point(2, 4));

            Content = new Container
            {
                Padding = new Margins(16),
                Childrens =
                {
                    new Button{ Text = "Exit", Dock = Dock.Bottom }
                    .RegisterMouseClickEvent( (sender) => Rise.Platform.Stop())
                },
            };
        }
    }
}