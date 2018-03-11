using Hevadea.Framework;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Game;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Menus
{
    public class MenuGamePaused : Menu
    {
        public MenuGamePaused(GameManager game) : base(game)
        {
            PauseGame = true;

            Content = new AnchoredContainer().AddChild(
                new WidgetFancyPanel
                {
                    Anchor  = Anchor.Center,
                    Origine = Anchor.Center,
                    UnitBound = new Rectangle(0, 0, 320, 416),
                    Padding = new Padding(16),
                    Content = new TileContainer
                    {
                        Flow = FlowDirection.TopToBottom,
                        Childrens =
                        {
                            new Label 
                                { Text = "Game Paused", Font = Ressources.FontAlagard},
                            new Button
                                { Text = "Continue", Padding = new Padding(4) }
                                .RegisterMouseClickEvent((sender) => {Game.CurrentMenu = new MenuInGame(Game);}),
                            new Button
                                { Text = !Game.IsMasterGame ? "Start Sever" : "ServerStarted", Padding = new Padding(4) }
                                .RegisterMouseClickEvent((sender) => { Game.StartServer(); Game.CurrentMenu = new MenuInGame(Game);}),
                            new Button
                                { Text = "Restart", Padding = new Padding(4) }
                                .RegisterMouseClickEvent(sender => Rise.Scene.Switch(new SceneWorldGeneration())),
                            new Button
                                { Text = "Quick save", Padding = new Padding(4) }
                                .RegisterMouseClickEvent((sender) => {Rise.Scene.Switch(new SceneWorldSaving(Rise.Platform.GetStorageFolder() + "/Saves/test", game));}),
                            new Button
                                { Text = "Save and exit", Padding = new Padding(4) }
                                .RegisterMouseClickEvent((sender) => {Rise.Scene.Switch(new SceneWorldSaving(Rise.Platform.GetStorageFolder() + "/Saves/test", game, true));}),
                            new Button
                                { Text = "Exit", Padding = new Padding(4) }
                                .RegisterMouseClickEvent((sender) => {Rise.Scene.Switch(new MainMenu());}),
                        }
                        
                    }
                });
        }
    }
}
