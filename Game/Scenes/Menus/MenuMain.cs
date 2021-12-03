using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Platform;
using Hevadea.Framework.UI;
using Hevadea.Scenes.Tabs;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Menus
{
    public class DesktopMainMenu : Scene
    {
        public override void Load()
        {
            Rise.Sound.Play(Resources.Theme0);

            var title = new WidgetLabel
            {
                Text = Game.Title,
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitOffset = new Point(0, -72),
                Font = Resources.FontAlagard,
                TextSize = 3f,
            };

            var subTitle = new WidgetLabel
            {
                Text = Game.SubTitle,
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitOffset = new Point(0, -16),
                Font = Resources.FontRomulus,
                TextColor = ColorPalette.Accent,
                TextSize = 1f,
            };

            var copyright = new WidgetLabel
            {
                Text = "© 2017-2021 SMNX",
                Anchor = Anchor.Bottom,
                Origine = Anchor.Bottom,
                Font = Resources.FontRomulus,
                TextSize = 1f
            };

            var continueButton = new WidgetButton
            {
                Enabled = Game.GetLastGame() != null,
                Text = "Continue",
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitOffset = new Point(0, 72),
                UnitBound = new Rectangle(0, 0, 256, 64),
            }
            .RegisterMouseClickEvent((sender) => Game.Play(Game.GetLastGame()));

            var version = new WidgetLabel
            {
                Text = $"{Game.Title} {Game.Version}",
                Anchor = Anchor.BottomRight,
                Origine = Anchor.BottomRight,
                UnitOffset = new Point(-16, 0),
                Font = Resources.FontHack,
                TextAlignement = TextAlignement.Right,
                TextColor = Color.White * 0.5f,
                TextSize = 1f,
            };

            var homeTab = new Tab
            {
                Icon = new Sprite(Resources.TileIcons, new Point(0, 4)),
                Content = new LayoutDock()
                {
                    Children =
                    {
                        title, subTitle, copyright, continueButton
                    }
                }
            };

            var menu = new WidgetTabContainer
            {
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitBound = new Rectangle(0, 0, 600, 720),
                TabAnchore = Rise.Platform.Family == PlatformFamily.Mobile ? TabAnchore.Bottom : TabAnchore.Left,
                Tabs =
                {
                    homeTab,
                    new TabNewWorld(),
                    new TabLoadWorld(),
                    new TabOption(),
                }
            };
            Container = new LayoutDock().AddChildren(menu, version);
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