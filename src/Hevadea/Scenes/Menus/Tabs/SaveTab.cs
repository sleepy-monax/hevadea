using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Scenes.Widgets;

using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Menus.Tabs
{
    public class SaveTab : Tab
    {
        public SaveTab(GameState gameState)
        {
            Icon = new Sprite(Ressources.TileIcons, new Point(3, 2));

            var container = new FlowLayout
            {
                Dock = Dock.Fill,
                Flow = FlowDirection.TopToBottom,
            };

            Content = new Container()
            {
                Childrens =
                {
                    new Label {Text = "Save", Font = Ressources.FontAlagard, Dock = Dock.Top},
                    container
                }
            };

            container.AddChild(new Button { Text = "Quick save", Padding = new Margins(4) })
            .RegisterMouseClickEvent(gameState.QuickSave);

            container.AddChild(new Button { Text = "Save and Exit", Padding = new Margins(4) })
            .RegisterMouseClickEvent(gameState.SaveAndExit);

            container.AddChild(new Button
            {
                Text = "Exit",
                Padding = new Margins(4)
            })
            .RegisterMouseClickEvent(Game.GoToMainMenu);
        }
    }
}