using Hevadea.Framework;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.Utils;
using Hevadea.Game;
using Hevadea.Game.Loading;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Menus
{
    public class MenuGamePaused : Menu
    {
        public MenuGamePaused(GameManager game) : base(game)
        {
            PauseGame = !game.IsRemote;

            var container = new TileContainer
            {
                Flow = FlowDirection.TopToBottom,
                Childrens =
                {
                    new Label 
                        { Text = "Game Paused", Font = Ressources.FontAlagard},
                    new Button
                            { Text = "Continue", Padding = new Padding(4) }
                        .RegisterMouseClickEvent((sender) => {Game.CurrentMenu = new MenuInGame(Game);}),
                }
            };

            if (!game.IsClient)
            {
                if (!game.IsServer)
                {
                    container.AddChild(new Button
                        {
                            Text = "Start Sever",
                            Padding = new Padding(4)
                        })
                        .RegisterMouseClickEvent((sender) =>
                        {
                            Game.StartServer();
                            Game.CurrentMenu = new MenuInGame(Game);
                        });
                }

                container.AddChild(new Button
                    {
                        Text = "Quick save", 
                        Padding = new Padding(4)
                    })
                    .RegisterMouseClickEvent((sender) =>
                    {
                        Game.CurrentMenu = new LoadingMenu(TaskFactorie.ConstructSaveWorld(game));
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
                .RegisterMouseClickEvent((sender) => { Rise.Scene.Switch(new MainMenu()); });

            Content = new AnchoredContainer
            {
                Childrens =
                {
                    new WidgetFancyPanel
                    {
                        Anchor  = Anchor.Center,
                        Origine = Anchor.Center,
                        UnitBound = new Rectangle(0, 0, 320, container.Childrens.Count * 64 + 32),
                        Padding = new Padding(16),
                        Content = container
                    }                    
                }
            };
        }
    }
}
