using Hevadea.Entities;
using Hevadea.Framework;
using Hevadea.Framework.Platform;
using Hevadea.Framework.UI;
using Hevadea.Framework.Extension;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Menus
{
    public class MenuRespawn : Menu
    {
        public MenuRespawn(Player player, GameState gameState) : base(gameState)
        {
            PauseGame = true;

            Content = new LayoutDock
            {
                Childrens =
                {
                    new WidgetFancyPanel
                    {
                        UnitBound = new Rectangle(0, 0, 840, 420),
                        Padding   = new Margins(16),
                        Anchor    = Anchor.Center,
                        Origine   = Anchor.Center,
                        Dock      = Rise.Platform.Family == PlatformFamily.Mobile ? Dock.Fill : Dock.None,

                        Content   = new LayoutDock
                        {
                            Childrens =
                            {
                                new WidgetLabel() { Text = "You died!", Font = Ressources.FontAlagard, TextSize = 2f, Anchor = Anchor.Center, Origine = Anchor.Center },
                                new WidgetButton("Respawn") {UnitOffset = new Point(-16, 0), Anchor = Anchor.Bottom , Origine = Anchor.BottomRight}
                                .RegisterMouseClickEvent((sender)=> { gameState.CurrentMenu = new MenuInGame(gameState); gameState.GetSession(player).Respawn(); }),
                                new WidgetButton("Exit") {UnitOffset = new Point(16, 0), Anchor = Anchor.Bottom, Origine = Anchor.BottomLeft }
                                .RegisterMouseClickEvent(gameState.SaveAndExit),
                            }
                        }
                    }
                }
            };
        }
    }
}