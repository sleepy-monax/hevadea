using System;
using System.IO;
using System.Net;
using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.Platform;
using Hevadea.Framework.Scening;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.Utils;
using Hevadea.Game.Loading;
using Hevadea.Scenes.MainMenu.Tabs;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.MainMenu
{
    public class SceneMainMenu : Scene
    {
        public override void Load()
        {
            Rise.Scene.SetBackground(Ressources.ParalaxeForest);

            var hevadeaLogo = new Label { Text = "Hevadea", Anchor = Anchor.Center, Origine = Anchor.Center, Font = Ressources.FontAlagardBig, Scale = 1.5f};
            var creators = new Label { Text = "(c) 2017-2018 Interesting Games", Anchor = Anchor.Bottom, Origine = Anchor.Bottom, Font = Ressources.FontRomulus, Scale = 0.5f };
            var continueButton = new Button
                {
                    Text = "Continue",
                    Anchor = Anchor.Center,
                    Origine = Anchor.Top,
                    UnitBound = new Rectangle(0, 0, 256, 64),
                    UnitOffset = new Point(0, 64)
                }
                .RegisterMouseClickEvent(ContinueLastGame);
            var menu = new WidgetTabContainer
            {
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitBound = new Rectangle(0, 0, 600, 720),
                UnitOffset = new Point(8,0),
                Padding = new Padding(16),
                Tabs =
                {
                    new Tab
                    {
                        Icon = new Sprite(Ressources.TileIcons, new Point(0,4)),
                        Content = new AnchoredContainer()
                        {
                            Childrens =
                            {
                                hevadeaLogo,
                                creators,
                            }
                        }
                    },

                    new ContainerTab
                    {
                        Icon = new Sprite(Ressources.TileIcons, new Point(0,2)),
                        Childrens =
                        {
                            new TabNewWorld(),
                            new TabLoadWorld(),
                        }
                        
                    },

                    new ContainerTab
                    {
                        Icon = new Sprite(Ressources.TileIcons, new Point(0,3)),
                        Childrens =
                        {
                            new TabMultiplayerConnect(),
                        }
                    },


                    new TabOption(),
                }
            };

            if (File.Exists(Rise.Platform.GetStorageFolder() + "/.lastgame"))
            {   
                var container = (Container) menu.Tabs[0].Content;
                container.Childrens.Add(continueButton);
            }
            
            if (Rise.Platform.Family == PlatformFamily.Mobile)
            {
                Container = menu;
            }
            else
            {
                Container = new AnchoredContainer
                {
                    Childrens = { menu },
                };
            }

        }

        private void ContinueLastGame(Widget sender)
        {
            if (File.Exists(Rise.Platform.GetStorageFolder() + "/.lastgame"))
            {
                Rise.Scene.Switch(new LoadingScene(TaskFactorie.ConstructLoadWorld(File.ReadAllText(Rise.Platform.GetStorageFolder() + "/.lastgame"))));
            }
        }

        public override void Unload()
        {
        }

        public override void OnUpdate(GameTime gameTime)
        {
        }

        public override void OnDraw(GameTime gameTime)
        {

        }
    }
}