using Hevadea.Framework;
using Hevadea.Framework.Scening;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Game;
using Hevadea.Game.Registry;
using Hevadea.Scenes.Widgets;
using Hevadea.WorldGenerator;
using Microsoft.Xna.Framework;
using System;
using System.Threading;
using Hevadea.Game.Entities;

namespace Hevadea.Scenes
{
    public class SceneWorldGeneration : Scene
    {
        private Thread _generatorThread;
        private Generator _worldgen;
        private Label _progressLabel;
        private ProgressBar _progressBar;
        
        public SceneWorldGeneration()
        {
            _generatorThread = new Thread(() =>
            {
                Thread.Sleep(1000);
                _worldgen = GENERATOR.DEFAULT;
                _worldgen.Seed = new Random().Next();
                var world = _worldgen.Generate();
                var player = (EntityPlayer)ENTITIES.PLAYER.Build();
                Rise.Scene.Switch(new SceneGameplay(new GameManager(world, player)));
            });

            _progressLabel = new Label { Text = "Generating world...", Anchor = Anchor.Center, Origine = Anchor.Center, Font = Ressources.FontRomulus, UnitOffset = new Point(0, -24) };
            _progressBar = new ProgressBar { UnitBound = new Rectangle(0, 0, 320, 8), Anchor = Anchor.Center, Origine = Anchor.Center, UnitOffset = new Point(0, 24)};
            
            Container = new AnchoredContainer
            {
                Childrens = { new WidgetFancyPanel{UnitBound = new Rectangle(0, 0, 420, 128),Padding = new Padding(16), Anchor = Anchor.Center, Origine = Anchor.Center, Content = new AnchoredContainer().AddChild(_progressBar).AddChild(_progressLabel)}}
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