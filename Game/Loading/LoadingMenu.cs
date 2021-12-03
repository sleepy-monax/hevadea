using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Threading;
using Hevadea.Framework.UI;
using Hevadea.Framework.Extension;
using Hevadea.Scenes.Menus;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Loading
{
    public class LoadingMenu : Menu
    {
        private Job _job;
        private WidgetLabel _progressLabel;
        private WidgetProgress _progressBar;

        public LoadingMenu(string title, Job job, GameState gameState) : base(gameState)
        {
            _job = job;

            PauseGame = true;

            _progressLabel = new WidgetLabel
            {
                Anchor = Anchor.Center,
                Font = Resources.FontRomulus,
                Origine = Anchor.Center,
                Text = "",
                UnitOffset = new Point(0, -16)
            };

            _progressBar = new WidgetProgress
            {
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitBound = new Rectangle(0, 0, 320 + 64, 8),
                UnitOffset = new Point(0, 16)
            };

            Content = new LayoutDock
            {
                Children =
                {
                    new WidgetLabel
                    {
                        Text = title,
                        Anchor = Anchor.Center,
                        Origine = Anchor.Center,
                        UnitOffset = new Point(0),
                        Font = Resources.FontAlagard,
                        TextSize = 1f,
                    },
                    new WidgetFancyPanel
                    {
                        Anchor = Anchor.Bottom,
                        Origine = Anchor.Bottom,
                        Content = new LayoutDock {Children = {_progressBar, _progressLabel }},
                        Dock = Rise.Platform.Family == Framework.Platform.PlatformFamily.Mobile ? Dock.Fill : Dock.None,
                        UnitBound = new Rectangle(0, 0, 512, 96),
                        UnitOffset = new Point(0, -32)
                    }
                }
            };

            _job.Start(true);

            _job.Finish += (sender, e) =>
            {
                if (!_job.Canceled) gameState.CurrentMenu = new MenuInGame(gameState);
            };
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(Rise.Graphic.GetBound(), Color.Black * 0.5f);

            base.Draw(spriteBatch, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            _progressLabel.Text = _job.Status;
            _progressBar.Value = _job.Progress;

            base.Update(gameTime);
        }
    }
}