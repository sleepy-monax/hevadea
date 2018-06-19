using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.Scening;
using Hevadea.Framework.Threading;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.Utils;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Loading
{
    public class LoadingScene : Scene
    {
        private Job _job;
        private Label _progressLabel;
        private ProgressBar _progressBar;

        public LoadingScene(Job job)
        {
            _job = job;
        }

        public override void Load()
        {
            _progressLabel = new Label
            {
                Anchor = Anchor.Center,
                Font = Ressources.FontRomulus,
                Origine = Anchor.Center,
                Text = "Generating world...",
                UnitOffset = new Point(0, -24)
            };

            _progressBar = new ProgressBar
            {
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitBound = new Rectangle(0, 0, 320, 8),
                UnitOffset = new Point(0, 24)
            };

            var _cancelButton = new SpriteButton()
            {
                Anchor = Anchor.TopRight,
                Origine = Anchor.Center,
                Sprite = new Sprite(Ressources.TileGui, new Point(7, 7)),
                UnitBound = new Rectangle(0, 0, 48, 48),
            }.RegisterMouseClickEvent((sender) =>
            {
                _job.Cancel();
                Game.GoToMainMenu();
            });

            Container = new Container
            {
                Padding = new Margins(16),
                Childrens =
                {
                    new WidgetFancyPanel
                    {
                        Anchor = Anchor.Center,
                        Content = new Container { Childrens = { _progressBar, _progressLabel, _cancelButton } },
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