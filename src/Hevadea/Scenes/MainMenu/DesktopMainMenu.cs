using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.Platform;
using Hevadea.Framework.Scening;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.Utils;
using Hevadea.Scenes.MainMenu.Tabs;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using System;

namespace Hevadea.Scenes.MainMenu
{
    public class DesktopMainMenu : Scene
    {
        public override void Load()
        {
            Rise.Scene.SetBackground(new Random().NextValue(Ressources.ParalaxeForest, Ressources.ParalaxeMontain));

            var title = new Label
            {
                Text = Game.Name,
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitOffset = new Point(0, -72),
                Font = Ressources.FontAlagard,
                TextSize = 3f,
            };

            var subTitle = new Label
            {
                Text = "\"Tales of the unknow\"",
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitOffset = new Point(0, -16),
                Font = Ressources.FontRomulus,
                TextSize = 1f,
            };

            var copyright = new Label
            {
                Text = "© 2017-2018 MAKER",
                Anchor = Anchor.Bottom,
                Origine = Anchor.Bottom,
                Font = Ressources.FontRomulus,
                TextSize = 1f
            };

            var continueButton = new Button
            {
                Text = "Continue",
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitOffset = new Point(0, 72),
                UnitBound = new Rectangle(0, 0, 256, 64),
            }
            .RegisterMouseClickEvent((sender) => Game.Play(Game.GetLastGame()));

            var menu = new WidgetTabContainer
            {
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitBound = new Rectangle(0, 0, 600, 720),
                TabAnchore = Rise.Platform.Family == PlatformFamily.Mobile ? TabAnchore.Bottom : TabAnchore.Left,
                Tabs =
                {
                    new Tab
                    {
                        Icon = new Sprite(Ressources.TileIcons, new Point(0,4)),
                        Content = new Container(title, subTitle, copyright, continueButton)
                    },

                    new TabNewWorld(),
                    new TabLoadWorld(),
                    new TabMultiplayerConnect(),
                    new TabOption(),
                }
            };
            Container = new Container(menu);
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