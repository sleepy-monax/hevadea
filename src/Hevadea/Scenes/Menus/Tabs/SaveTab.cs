using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;

using Hevadea.Loading;
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
            .RegisterMouseClickEvent((sender) =>
            {
                var job = Jobs.SaveWorld.Then((_, e) => { gameState.CurrentMenu = new MenuInGame(gameState); });
                job.SetArguments(new Jobs.WorldSaveInfo(gameState.GetSavePath(), gameState));

                gameState.CurrentMenu = new LoadingMenu(job, gameState);
            });

            container.AddChild(new Button { Text = "Save and Exit", Padding = new Margins(4) })
            .RegisterMouseClickEvent((sender) =>
            {
                var job = Jobs.SaveWorld;
                job.SetArguments(new Jobs.WorldSaveInfo(gameState.GetSavePath(), gameState));
                job.Then((_, e) => { Game.GoToMainMenu(); });

                gameState.CurrentMenu = new LoadingMenu(job, gameState);
            });

            container.AddChild(new Button
            {
                Text = "Exit",
                Padding = new Margins(4)
            })
            .RegisterMouseClickEvent(sender => Game.GoToMainMenu());
        }
    }
}