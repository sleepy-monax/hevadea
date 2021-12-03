using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Threading;
using Hevadea.Framework.UI;
using Hevadea.Framework.Extension;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Loading
{
    public class LoadingScene : Scene
    {
        private Job _job;
        private WidgetLabel _progressLabel;
        private WidgetProgress _progressBar;

        public LoadingScene(Job job)
        {
            _job = job;
        }

        public override void Load()
        {
            _progressLabel = new WidgetLabel
            {
                Anchor = Anchor.Center,
                Font = Resources.FontRomulus,
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
                Sprite = new Sprite(Resources.TileGui, new Point(7, 7)),
                UnitBound = new Rectangle(0, 0, 48, 48),
                UnitOffset = new Point(-48, 48)
            }.RegisterMouseClickEvent((sender) =>
            {
                _job.Cancel();
                Game.GoToMainMenu();
            });

            Container = new LayoutDock
            {
                Padding = new Spacing(16),
                Children =
                {
                    new WidgetFancyPanel
                    {
                        Anchor = Anchor.Center,
                        Content = new LayoutDock {Children = {_progressBar, _progressLabel, _cancelButton}},
                        Dock = Rise.Platform.Family == Framework.Platform.PlatformFamily.Mobile ? Dock.Fill : Dock.None,
                        Origine = Anchor.Center,
                        UnitBound = new Rectangle(0, 0, 840, 256),
                    }
                }
            };

            _job.Start(true);
        }

        public override void OnDraw(GameTime gameTime)
        {
        }

        public override void OnUpdate(GameTime gameTime)
        {
            _progressBar.Value = _job.Progress;
            _progressLabel.Text = _job.Status;
        }

        public override void Unload()
        {
        }
    }
}