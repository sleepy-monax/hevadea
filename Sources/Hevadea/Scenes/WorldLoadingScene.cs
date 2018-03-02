using Hevadea.Framework;
using Hevadea.Framework.Scening;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Game.Storage;
using Microsoft.Xna.Framework;
using System.Threading;

namespace Hevadea.Scenes
{
    public class WorldLoadingScene : Scene
    {
        private Thread _generatorThread;
        private GameLoader _worldloader;
        private Label _progressLabel;
        private ProgressBar _progressBar;

        public WorldLoadingScene(string path)
        {
            _worldloader = new GameLoader();

            _generatorThread = new Thread(() =>
            {
                Thread.Sleep(1000);
                var game = _worldloader.Load(path);
                Rise.Scene.Switch(new GameScene(game));
            });

            _progressLabel = new Label { Text = "Loading world...", Anchor = Anchor.Center, Origine = Anchor.Center, Font = Ressources.FontRomulus, UnitOffset = new Point(0, -24) };
            _progressBar = new ProgressBar { UnitBound = new Rectangle(0, 0, 320, 8), Anchor = Anchor.Center, Origine = Anchor.Center };

            Container = new AnchoredContainer
            {
                Childrens = { _progressLabel, _progressBar }
            };
        }

        public override void OnDraw(GameTime gameTime)
        {

        }

        public override void Load()
        {
            _generatorThread.Start();
        }

        public override void Unload()
        {
        }

        public override void OnUpdate(GameTime gameTime)
        {

            _progressLabel.Text = $"Loading world: {_worldloader.GetStatus()}";
            _progressBar.Value += (_worldloader.GetProgress() - _progressBar.Value) * 0.1f;

        }
    }
}
