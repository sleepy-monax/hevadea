using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Menus.Tabs
{
    public class MinimapTab : InventoryTab
    {
        public MinimapTab(Game game) : base(game)
        {
            Icon = new Sprite(Ressources.TileIcons, new Point(5, 4));
            Content = new WidgetMinimap(game);
        }
    }
}