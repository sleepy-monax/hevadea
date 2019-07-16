using Hevadea.Framework;
using Hevadea.Framework.UI;
using Hevadea.Framework.Extension;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            var wayPointNameTextBox = new TextBox() { Dock = Dock.Top };
            var wayPointAddButton = new Button("Add waypoint") { Dock = Dock.Bottom };
            wayPointAddButton.RegisterMouseClickEvent(_ => 
            {
                AddWaypoint(wayPointAddButton.Text);
                gameState.CurrentMenu = new MenuInGame(gameState);
            });

            Content = new Container
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

                        Content   = new Container
                        {
                            Childrens =
                            {
                                new Label()
                                {
                                    Text = "Add waypoint",
                                    Font = Ressources.FontAlagard,
                                    TextSize = 1f,
                                    Dock = Dock.Top
                                },
                                new SpriteButton()
                                {
                                    Anchor = Anchor.TopRight,
                                    Origine = Anchor.Center,
                                    Sprite = new Sprite(Ressources.TileGui, new Point(7, 7)),
                                    UnitBound = new Rectangle(0, 0, 48, 48),
                                    UnitOffset = new Point(-24, 24)
                                }.RegisterMouseClickEvent(sender => gameState.CurrentMenu = new MenuInGame(gameState)),
                                new Label()
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
