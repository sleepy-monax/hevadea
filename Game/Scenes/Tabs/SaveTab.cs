using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.UI;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Tabs
{
    public class SaveTab : Tab
    {
        public SaveTab(GameState gameState)
        {
            Icon = new Sprite(Resources.TileIcons, new Point(3, 2));

            var container = new LayoutFlow
            {
                Dock = Dock.Fill,
                Flow = LayoutFlowDirection.TopToBottom,
                Marging = 8,
            };

            Content = new LayoutDock()
            {
                Padding = new Spacing(16),
                Children =
                {
                    new WidgetLabel {Text = "Save", Font = Resources.FontAlagard, Dock = Dock.Top},
                    container
                }
            };

            container.AddChild(
                new WidgetButton
                    {
                        Text = "Quick save",
                        Padding = new Spacing(4)
                    }
                    .RegisterMouseClickEvent(gameState.QuickSave)
            );

            container.AddChild(
                new WidgetButton
                    {
                        Text = "Save and Exit",
                        Padding = new Spacing(4)
                    }
                    .RegisterMouseClickEvent(gameState.SaveAndExit)
            );

            container.AddChild(
                new WidgetButton
                    {
                        Text = "Exit",
                        Padding = new Spacing(4)
                    }
                    .RegisterMouseClickEvent(Game.GoToMainMenu)
            );
        }
    }
}