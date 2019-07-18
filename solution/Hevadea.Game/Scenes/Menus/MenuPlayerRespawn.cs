using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Platform;
using Hevadea.Framework.UI;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Menus
{
    public class MenuPlayerRespawn : Menu
    {
        public MenuPlayerRespawn(GameState gameState) : base(gameState)
        {
            PauseGame = true;

            Content = new LayoutDock
            {
                Children =
                {
                    new WidgetFancyPanel
                    {
                        UnitBound = new Rectangle(0, 0, 840, 420),
                        Padding = new Spacing(16),
                        Anchor = Anchor.Center,
                        Origine = Anchor.Center,
                        Dock = Rise.Platform.Family == PlatformFamily.Mobile ? Dock.Fill : Dock.None,

                        Content = new LayoutDock
                        {
                            Children =
                            {
                                new WidgetLabel()
                                    {
                                        Text = "You died!", Font = Resources.FontAlagard, TextSize = 2f,
                                        Anchor = Anchor.Center, Origine = Anchor.Center
                                    },

                                new WidgetButton("Respawn")
                                    {
                                        UnitOffset = new Point(-16, 0), Anchor = Anchor.Bottom,
                                        Origine = Anchor.BottomRight
                                    }
                                    .RegisterMouseClickEvent((sender) =>
                                    {
                                        gameState.CurrentMenu = new MenuInGame(gameState);
                                        gameState.LocalPlayer.Respawn();
                                    }),

                                new WidgetButton("Exit")
                                    {
                                        UnitOffset = new Point(16, 0), Anchor = Anchor.Bottom,
                                        Origine = Anchor.BottomLeft
                                    }
                                    .RegisterMouseClickEvent(gameState.SaveAndExit),
                            }
                        }
                    }
                }
            };
        }
    }
}