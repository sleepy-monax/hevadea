using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Game;
using Hevadea.Game.Loading;
using Hevadea.Scenes.MainMenu;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Menus.Tabs
{
    public class SaveTab : Tab
    {
        public SaveTab(GameManager game)
        {
            Icon = new Sprite(Ressources.TileIcons, new Point(3, 2));

            var container = new FlowContainer
            {
                Dock = Dock.Fill,
                Flow = FlowDirection.TopToBottom,
            };

            Content = new DockContainer()
            {
                Childrens =
                {
                    new Label {Text = "Save", Font = Ressources.FontAlagard, Dock = Dock.Top},
                    container
                }
            };

            if (!game.IsClient)
            {
                container.AddChild(new Button
                {
                    Text = "Quick save",
                    Padding = new Padding(4)
                })
                    .RegisterMouseClickEvent((sender) =>
                    {
                        game.CurrentMenu = new LoadingMenu(TaskFactorie.ConstructSaveWorld(game));
                    });
                container.AddChild(new Button
                {
                    Text = "Save and Exit",
                    Padding = new Padding(4)
                })
                    .RegisterMouseClickEvent((sender) =>
                    {
                        Rise.Scene.Switch(new LoadingScene(TaskFactorie.ConstructSaveWorldAndExit(game)));
                    });
            }

            container.AddChild(new Button
            {
                Text = game.IsClient ? "Disconnect" : "Exit",
                Padding = new Padding(4)
            })
                .RegisterMouseClickEvent((sender) => { Rise.Scene.Switch(new SceneMainMenu()); });
        }
    }
}
