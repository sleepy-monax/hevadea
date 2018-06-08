using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.Threading;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.Utils;
using Hevadea.Scenes.Menus;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Loading
{
    public class LoadingMenu : Menu
    {
        private Job _job;

        private Label _progressLabel;
        private ProgressBar _progressBar;

        public LoadingMenu(Job job, GameState gameState) : base(gameState)
        {
            _job = job;

            PauseGame = true;

            _progressLabel = new Label
            {
                Text = "Loading...",
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

            Content = new Container
            {
                Childrens =
                {
                    new WidgetFancyPanel
                    {
                        UnitBound = new Rectangle(0, 0, 840, 256),
                        Padding = new Margins(16),
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

            _job.Finish += (sender, e) =>
            {
                gameState.CurrentMenu = new MenuInGame(gameState);
            };
        }

        public override void Update(GameTime gameTime)
        {
            _progressLabel.Text = _job.Status;
            _progressBar.Value = _job.Progress;
        }
    }
}