using System;
using Hevadea.Framework;
using Hevadea.Framework.Scening;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Scenes;
using Hevadea.Scenes.Widgets;

namespace Hevadea.Game.Loading
{
    public class LoadingScene : Scene
    {

        public static LoadingScene ConstructNewWorld(string path, int seed)
        {
            return new LoadingScene(path)
            {
                Tasks =
                {
                    new LoadingTaskGenerateWorld(seed),
                    new LoadingTaskNewPlayer(),
                    new LoadingTaskInitializeGame(),
                    new LoadingTaskSaveWorld(),
                    new LoadingTaskEnterGame()
                }
            };
        }

        public static LoadingScene ConstructSaveWorld(GameManager game)
        {
            return new LoadingScene(game)
            {
                Tasks =
                {
                    new LoadingTaskSaveWorld(),
                    new LoadingTaskEnterGame()
                }
            };
        }
        
        public static LoadingScene ConstructLoadWorld(string path)
        {
            return new LoadingScene(path)
            {
                Tasks =
                {
                    new LoadingTaskLoadWorld(),
                    new LoadingTaskInitializeGame(),
                    new LoadingTaskEnterGame()
                }
            };
        }

        public List<LoadingTask> Tasks { get; set; } = new List<LoadingTask>();
        public List<LoadingTask> DoneTasks { get; set; } = new List<LoadingTask>();

        private Label _progressLabel;
        private ProgressBar _progressBar;
        
        private LoadingTask _currentTask;
        private GameManager _game;
        private SpriteBatch _spriteBatch;
        private Queue<LoadingTask> _tasksQueue;
        
        public Action LoadingFinished;

        public LoadingScene(GameManager game)
        {
            _game = game;
        }
        
        public LoadingScene(string gamePath)
        {
            _game = new GameManager(){SavePath = gamePath};
        }
        
        public override void Load()
        {
            _spriteBatch = Rise.Graphic.CreateSpriteBatch();
            _tasksQueue = new Queue<LoadingTask>(Tasks);

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
                _currentTask?.Thread.Abort();
                _tasksQueue.Clear();
                Rise.Scene.Switch(new MainMenu());
            });
            
            Container = new AnchoredContainer
            {
                Childrens = { new WidgetFancyPanel{UnitBound = new Rectangle(0, 0, 420, 128),Padding = new Padding(16), Anchor = Anchor.Center, Origine = Anchor.Center, Content = new AnchoredContainer().AddChild(_progressBar).AddChild(_progressLabel).AddChild(_cancelButton)}}
            };
        }

        public override void OnDraw(GameTime gameTime)
        {
            var index = 1;

            _spriteBatch.Begin();

            foreach (var t in DoneTasks)
            {
                _spriteBatch.DrawString(Ressources.FontHack, t.TaskName, new Vector2(16, 24 * index + 2), Color.Black);
                _spriteBatch.DrawString(Ressources.FontHack, t.TaskName, new Vector2(16, 24 * index), Color.Green);
                index++;
            }

            if (_currentTask != null)
            {   
                _spriteBatch.DrawString(Ressources.FontHack, _currentTask.TaskName, new Vector2(16, 24 * index + 2), Color.Black);
                _spriteBatch.DrawString(Ressources.FontHack, _currentTask.TaskName, new Vector2(16, 24 * index), Color.Gold);
                index++;
            }
            
            foreach (var t in _tasksQueue)
            {
                _spriteBatch.DrawString(Ressources.FontHack, t.TaskName, new Vector2(16, 24 * index + 2), Color.Black);
                _spriteBatch.DrawString(Ressources.FontHack, t.TaskName, new Vector2(16, 24 * index), Color.Red);
                index++;
            }

            _spriteBatch.End();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (_currentTask != null)
            {
                _progressLabel.Text = _currentTask.TaskName;
                _progressBar.Value = _currentTask.GetProgress();
            }
            else
            {
                _progressLabel.Text = "Loading...";
            }
            
            if (_currentTask == null || _currentTask.HasFinish)
            {
                if (_currentTask != null)
                {
                    DoneTasks.Add(_currentTask);
                }
                
                if (_tasksQueue.Count > 0)
                {
                    _currentTask = _tasksQueue.Dequeue();
                    _currentTask.RunTask(_game);
                }
                else
                {
                    _currentTask = null;
                    LoadingFinished?.Invoke();   
                }
            }
        }

        public override void Unload()
        {

        }
    }
}
