using Hevadea.Game;
using Hevadea.Game.Entities.Creatures;
using Hevadea.Game.Registry;
using Hevadea.WorldGenerator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading;
using Hevadea.Framework;
using Hevadea.Framework.Scening;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;

namespace Hevadea.Scenes
{
    public class WorldGenScene : Scene
    {
        private Thread _generatorThread;
        private Generator _worldgen;
        private Label _progressLabel;
        private ProgressBar _progressBar;
        
        public WorldGenScene()
        {
            _generatorThread = new Thread(() =>
            {
                Thread.Sleep(1000);
                GC.AddMemoryPressure(600 * 1024 * 1024);
                _worldgen = GENERATOR.DEFAULT;
                _worldgen.Seed = new Random().Next();
                var world = _worldgen.Generate();
                var player = (PlayerEntity)ENTITIES.PLAYER.Build();
                Rise.Scene.Switch(new GameScene(new GameManager(world, player)));
            });

            _progressLabel = new Label { Text = "Generating world...", Anchor = Anchor.Center, Origine = Anchor.Center, Font = Ressources.FontRomulus, UnitOffset = new Point(0, -24) };
            _progressBar = new ProgressBar { UnitBound = new Rectangle(0, 0, 320, 8), Anchor = Anchor.Center, Origine = Anchor.Center};
            
            Container = new AnchoredContainer
            {
                Childrens = {_progressLabel, _progressBar}
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
            if (_worldgen?.CurrentLevel?.CurrentFeature != null)
            {
                _progressLabel.Text = $"{_worldgen.CurrentLevel.LevelName}: {_worldgen.CurrentLevel.CurrentFeature.GetName()}";
                _progressBar.Value = _worldgen.CurrentLevel.GetProgress();
            }
        }
    }
}