using Hevadea.Framework;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.Utils;
using Hevadea.Scenes.MainMenu;
using Hevadea.Scenes.Menus;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Loading
{
    public class LoadingMenu : Menu
    {
        private TaskCompound _task;

        private Label _progressLabel;
        private ProgressBar _progressBar;

        public LoadingMenu(TaskCompound task) : base(task.GetGame())
        {
            _task = task;

            PauseGame = true;

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

            var _cancelButton = new Button
            {
                Text = "Cancel",
                UnitBound = new Rectangle(0, 0, 640, 64),
                Anchor = Anchor.Center,
                Origine = Anchor.Top,
                UnitOffset = new Point(0, 128)
            }.RegisterMouseClickEvent((sender) =>
            {
                _task.Abort();
                Rise.Scene.Switch(new SceneMainMenu());
            });

            Content = new AnchoredContainer
            {
                Childrens =
                {
                    new WidgetFancyPanel
                    {
                        UnitBound = new Rectangle(0, 0, 840, 256),
                        Padding = new Padding(16),
                        Anchor = Anchor.Center,
                        Origine = Anchor.Center,
                        Content = new AnchoredContainer
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

            _task.Start();
        }

        public override void Update(GameTime gameTime)
        {
            var currentTask = _task.GetRunningTask();

            if (currentTask != null)
            {
                _progressLabel.Text = currentTask.GetStatus();
                _progressBar.Value = currentTask.GetProgress();
            }

            _task.Update();
        }
    }
}
