using Hevadea.Framework.UI;
using Hevadea.Framework.Extension;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Scenes.Menus;
using Hevadea.Worlds;

namespace Hevadea.Scenes.PauseMenus
{
    public class MenuMinimapWaypoint : Menus.Menu
    {
        public MenuMinimapWaypoint(GameState gameState) : base(gameState)
        {
            PauseGame = true;

            var wayPointNameTextBox = new WidgetTextBox() { Dock = Dock.Top };
            var wayPointAddButton = new WidgetButton("Add waypoint") { Dock = Dock.Bottom };
            wayPointAddButton.RegisterMouseClickEvent(_ => 
            {
                AddWaypoint(wayPointAddButton.Text);
                gameState.CurrentMenu = new MenuInGame(gameState);
            });

            Content = new LayoutDock
            {

                Childrens =
                {
                    new WidgetFancyPanel
                    {
                        UnitBound = new Rectangle(0, 0, 600, 720),
                        Padding   = new Margins(16),
                        Anchor    = Anchor.Center,
                        Origine   = Anchor.Center,
                        Dock      = Dock.None,

                        Content   = new LayoutDock
                        {
                            Childrens =
                            {
                                new WidgetLabel()
                                {
                                    Text = "Add waypoint",
                                    Font = Ressources.FontAlagard,
                                    TextSize = 1f,
                                    Dock = Dock.Top
                                },
                                new WidgetSprite()
                                {
                                    Anchor = Anchor.TopRight,
                                    Origine = Anchor.Center,
                                    Sprite = new Sprite(Ressources.TileGui, new Point(7, 7)),
                                    UnitBound = new Rectangle(0, 0, 48, 48),
                                    UnitOffset = new Point(-24, 24)
                                }.RegisterMouseClickEvent(sender => gameState.CurrentMenu = new MenuInGame(gameState)),
                                new WidgetLabel()
                                {
                                    Text = "Name:",
                                    Font = Ressources.FontRomulus,
                                    TextAlignement = TextAlignement.Left,
                                    Dock = Dock.Top
                                },
                                wayPointNameTextBox,
                                wayPointAddButton,
                            }
                        }
                    }
                }
            };
        }

        void AddWaypoint(string name)
        {
            var player = GameState.LocalPlayer.Entity;
            var minimap = GameState.LocalPlayer.Entity.Level.Minimap;
            minimap.Waypoints.Add(new MinimapWaypoint(player.Coordinates, 0, name));
        }
    }
}
