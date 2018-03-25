using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Menus.Tabs
{
    public class CraftingTab : Tab
    {
        public CraftingTab()
        {
            Icon = new Sprite(Ressources.TileIcons, new Point(4, 2));
            Content = new DockContainer()
            {
                Childrens =
                {
                    new Label {Text = "Crafting", Font = Ressources.FontAlagard, Dock = Dock.Top},
                }
            };
        }
    }
}
