using Hevadea.Framework;
using Hevadea.Framework.Scening;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Game;
using Hevadea.Game.Storage;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hevadea.Scenes
{
    public class WorldSaveScene : Scene
    {

        private Thread _generatorThread;
        private GameSaver _worldSave;
        private Label _progressLabel;
        private ProgressBar _progressBar;

        public WorldSaveScene(string path, GameManager game, bool GoToMainMenu = false)
        {
            _worldSave = new GameSaver();

            _generatorThread = new Thread(() =>
            {
                Thread.Sleep(1000);
                _worldSave.Save(path, game);

                if (GoToMainMenu)
                {
                    Rise.Scene.Switch(new MainMenu());
                }
                else
                {
                    Rise.Scene.Switch(new GameScene(game));
                }
            });

            _progressLabel = new Label { Text = "Saving world...", Anchor = Anchor.Center, Origine = Anchor.Center, Font = Ressources.FontRomulus, UnitOffset = new Point(0, -24) };
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

            _progressLabel.Text = $"Saving world: {_worldSave.GetStatus()}";
            _progressBar.Value = _worldSave.GetProgress();

        }

    }
}
