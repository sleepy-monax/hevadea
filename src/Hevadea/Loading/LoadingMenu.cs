using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.Threading;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.Extension;
using Hevadea.Scenes.Menus;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Loading
{
    public class LoadingMenu : Menu
    {
        Job _job;
        WidgetLabel _progressLabel;
        WidgetProgress _progressBar;

        public LoadingMenu(Job job, GameState gameState) : base(gameState)
        {
            _job = job;

            PauseGame = true;

            _progressLabel = new WidgetLabel
            {
                Anchor = Anchor.Center,
                Font = Ressources.FontRomulus,
                Origine = Anchor.Center,
                Text = "Loading...",
                UnitOffset = new Point(0, -24)
            };

            _progressBar = new WidgetProgress
            {
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitBound = new Rectangle(0, 0, 320, 8),
                UnitOffset = new Point(0, 24)
            };

            var _cancelButton = new WidgetSprite()
            {
                Anchor = Anchor.TopRight,
                Origine = Anchor.Center,
                Sprite = new Sprite(Ressources.TileGui, new Point(7, 7)),
                UnitBound = new Rectangle(0, 0, 48, 48),
                UnitOffset = new Point(-48, 48)
            }.RegisterMouseClickEvent((sender) =>
            {
                _job.Cancel();
                Game.GoToMainMenu();
            });

            Content = new LayoutDock
            {
                Childrens =
                {
                    new WidgetFancyPanel
                    {
                        Anchor = Anchor.Center,
                        Content = new LayoutDock { Childrens = { _progressBar, _progressLabel, _cancelButton } },
                        Dock = Rise.Platform.Family == Framework.Platform.PlatformFamily.Mobile ? Dock.Fill : Dock.None,
                        Origine = Anchor.Center,
                        UnitBound = new Rectangle(0, 0, 840, 256),
                    }
                }
            };

            _job.Start(true);

            _job.Finish += (sender, e) =>
            {
				if (!_job.Canceled)
				{
					gameState.CurrentMenu = new MenuInGame(gameState);
                }
            };
        }

        public override void Update(GameTime gameTime)
        {
            _progressLabel.Text = _job.Status;
            _progressBar.Value = _job.Progress;
        }
    }
}