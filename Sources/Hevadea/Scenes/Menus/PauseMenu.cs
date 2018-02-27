﻿using Hevadea.Game;
using Hevadea.Scenes.Widgets;
using Maker.Rise;
using Maker.Rise.UI.Widgets;
using Maker.Rise.UI.Widgets.Containers;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Menus
{
    public class PauseMenu : Menu
    {
        public PauseMenu(GameManager game) : base(game)
        {
            PauseGame = true;

            Content = new AnchoredContainer().AddChild(
                new FancyPanel
                {
                    Anchor  = Anchor.Center,
                    Origine = Anchor.Center,
                    Bound   = new Rectangle(0, 0, 320, 416),
                    Padding = new Padding(16),
                    Content = new TileContainer
                    {
                        Flow = FlowDirection.TopToBottom,
                        Childrens =
                        {
                            new Label 
                                { Text = "Game Paused", Font = Ressources.FontAlagard},
                            new Button
                                { Font  = Ressources.FontRomulus, Text = "Continue", Padding = new Padding(4) }
                                .RegisterMouseClickEvent((sender) => {Game.CurrentMenu = new HUDMenu(Game);}),
                            new Button
                                { Font  = Ressources.FontRomulus, Text = "Restart", Padding = new Padding(4) }
                                .RegisterMouseClickEvent(sender => Engine.Scene.Switch(new WorldGenScene())),
                            new Button
                                { Font  = Ressources.FontRomulus, Text = "Quick save", Padding = new Padding(4) }
                                .RegisterMouseClickEvent((sender) => {GameManager.Save("Saves/test", game); Game.CurrentMenu = new HUDMenu(Game);}),
                            new Button
                                { Font  = Ressources.FontRomulus, Text = "Save and exit", Padding = new Padding(4) }
                                .RegisterMouseClickEvent((sender) => {GameManager.Save("Saves/test", game); Engine.Scene.Switch(new MainMenu());}),
                            new Button
                                { Font  = Ressources.FontRomulus, Text = "Exit", Padding = new Padding(4) }
                                .RegisterMouseClickEvent((sender) => {Engine.Scene.Switch(new MainMenu());}),
                        }
                        
                    }
                });
        }
    }
}
