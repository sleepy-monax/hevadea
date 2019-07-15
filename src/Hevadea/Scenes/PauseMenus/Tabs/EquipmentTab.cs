using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Menus.Tabs
{
    public class EquipmentTab : Tab
    {
        public EquipmentTab()
        {
            Icon = new Sprite(Ressources.TileIcons, new Point(4, 4));
            Content = new Container()
            {
                Padding = new Margins(16),
                Childrens =
                {
                    new Label
                    {
                        Text = "Equipment",
                        Font = Ressources.FontAlagard,
                        Dock = Dock.Top },
                }
            };
        }
    }
}