using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Menus.Tabs
{
    public class PlayerStatesTab : Tab
    {
        public PlayerStatesTab()
        {
            Icon = new Sprite(Ressources.TileIcons, new Point(3, 4));

            Content = new Container()
            {
                Childrens =
                {
                    new Label {Text = "Stats", Font = Ressources.FontAlagard, Dock = Dock.Top},
                }
            };
        }
    }
}