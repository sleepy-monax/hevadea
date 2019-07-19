using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.UI;
using Hevadea.Scenes.Widgets;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Menus
{
    public class MenuAddMinimapWaypoint : Menu
    {
        public MenuAddMinimapWaypoint(GameState gameState) : base(gameState)
        {
            PauseGame = true;
            EscapeToClose = true;

            var wayPointNameTextBox = new WidgetTextBox() {Dock = Dock.Top};
            var wayPointAddButton = new WidgetButton("Add waypoint") {Dock = Dock.Bottom};
            wayPointAddButton.RegisterMouseClickEvent(_ =>
            {
                AddWaypoint(wayPointAddButton.Text);
                gameState.CurrentMenu = new MenuInGame(gameState);
            });

            Content = new LayoutDock
            {
                Children =
                {
                    new WidgetFancyPanel
                    {
                        UnitBound = new Rectangle(0, 0, 600, 720),
                        Padding = new Spacing(16),
                        Anchor = Anchor.Center,
                        Origine = Anchor.Center,
                        Dock = Dock.None,

                        Content = new LayoutDock
                        {
                            Children =
                            {
                                new WidgetLabel()
                                {
                                    Text = "Add waypoint",
                                    Font = Resources.FontAlagard,
                                    TextSize = 1f,
                                    Dock = Dock.Top
                                },
                                new WidgetSprite()
                                {
                                    Anchor = Anchor.TopRight,
                                    Origine = Anchor.Center,
                                    Sprite = new Sprite(Resources.TileGui, new Point(7, 7)),
                                    UnitBound = new Rectangle(0, 0, 48, 48),
                                    UnitOffset = new Point(-24, 24)
                                }.RegisterMouseClickEvent(sender => gameState.CurrentMenu = new MenuInGame(gameState)),
                                new WidgetLabel()
                                {
                                    Text = "Name:",
                                    Font = Resources.FontRomulus,
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

        private void AddWaypoint(string name)
        {
            var player = GameState.LocalPlayer.Entity;
            var minimap = GameState.LocalPlayer.Entity.Level.Minimap;
            minimap.Waypoints.Add(new MinimapWaypoint(player.Coordinates, 0, name));
        }
    }
}