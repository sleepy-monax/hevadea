using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.Scening;
using Hevadea.Framework.Threading;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.Utils;
using Hevadea.Scenes.MainMenu;
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
                Text = "Generating world...",
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                Font = Ressources.FontRomulus,
                UnitOffset = new Point(0, -24)
            };

            _progressBar = new ProgressBar
            {
                UnitBound = new Rectangle(0, 0, 320, 8),
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitOffset = new Point(0, 24)
            };

            var _cancelButton = new SpriteButton()
            {
                Sprite = new Sprite(Ressources.TileGui, new Point(7, 7)),
                UnitBound = new Rectangle(0, 0, 48, 48),
                Anchor = Anchor.TopRight,
                Origine = Anchor.Center
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
                        UnitBound = new Rectangle(0, 0, 840, 256),
                        Anchor = Anchor.Center,
                        Origine = Anchor.Center,
                        Dock = Rise.Platform.Family == Framework.Platform.PlatformFamily.Mobile ? Dock.Fill : Dock.None,
                        Content = new Container
                        {
                            Childrens =
                            {
                                _progressBar,
                                _progressLabel,
                                _cancelButton
                            }
                        }
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
            _progressLabel.Text = _job.Status;
            _progressBar.Value = _job.Progress;
        }

        public override void Unload()
        {
        }
    }
}