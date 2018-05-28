using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;

using Hevadea.Loading;

using Hevadea.Scenes.MainMenu;
using Hevadea.Scenes.Widgets;

using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Menus.Tabs
{
    public class SaveTab : Tab
    {
        public SaveTab(Game game)
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
            .RegisterMouseClickEvent((sender) =>
            {
                LoadingTask saveTask = TaskFactorie.SaveWorld(game);
                saveTask.LoadingFinished += (_, e) => { game.CurrentMenu = new MenuInGame(game); };
                game.CurrentMenu = new LoadingMenu(saveTask, game);
            });

            container.AddChild(new Button { Text = "Save and Exit", Padding = new Margins(4) })
            .RegisterMouseClickEvent((sender) =>
            {
                var saveTask = TaskFactorie.SaveWorld(game);
                saveTask.LoadingFinished += (_, e) => { Rise.Scene.Switch(new SceneMainMenu()); };
                game.CurrentMenu = new LoadingMenu(saveTask, game);
            });

            container.AddChild(new Button
            {
                Text = "Exit",
                Padding = new Margins(4)
            })
                .RegisterMouseClickEvent((sender) => { Rise.Scene.Switch(new SceneMainMenu()); });
        }
    }
}