using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Scenes.Widgets;

using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Menus.Tabs
{
    public class SaveTab : Tab
    {
        public SaveTab(GameState gameState)
        {
            Icon = new Sprite(Ressources.TileIcons, new Point(3, 2));

            var container = new LayoutFlow
            {
                Dock = Dock.Fill,
                Flow = LayoutFlowDirection.TopToBottom,
                Marging = 8,
            };

            Content = new LayoutDock()
            {
                Padding = new Margins(16),
                Childrens =
                {
                    new WidgetLabel {Text = "Save", Font = Ressources.FontAlagard, Dock = Dock.Top},
                    container
                }
            };

            container.AddChild(
                new WidgetButton
                {
                    Text = "Quick save",
                    Padding = new Margins(4)
                }
                .RegisterMouseClickEvent(gameState.QuickSave)
            );

            container.AddChild(
                new WidgetButton
                {
                    Text = "Save and Exit",
                    Padding = new Margins(4)
                }
                .RegisterMouseClickEvent(gameState.SaveAndExit)
            );

            container.AddChild(
                new WidgetButton
                {
                    Text = "Exit",
                    Padding = new Margins(4)
                }
                .RegisterMouseClickEvent(Game.GoToMainMenu)
            );
        }
    }
}