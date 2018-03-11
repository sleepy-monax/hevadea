using Hevadea.Framework;
using Hevadea.Framework.Scening;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.Game.Loading
{
    public class LoadingScene : Scene
    {

        public static LoadingScene ConstructNewWorld(int seed)
        {
            return new LoadingScene()
            {
                Tasks =
                {

                }
            };
        }

        public Queue<LoadingTask> Tasks { get; set; } = new Queue<LoadingTask>();
        public List<LoadingTask> DoneTasks { get; set; } = new List<LoadingTask>();

        private LoadingTask _currentTask;
        private GameManager _game;
        private SpriteBatch _spriteBatch;

        public override void Load()
        {
            _game = new GameManager();
            _spriteBatch = Rise.Graphic.CreateSpriteBatch();
        }

        public override void OnDraw(GameTime gameTime)
        {
            int index = 0;

            _spriteBatch.Begin();

            foreach (var t in DoneTasks)
            {
                _spriteBatch.DrawString(Ressources.FontHack, t.TaskName, new Vector2(16, 64 * index), Color.Green);
                index++;
            }

            foreach (var t in Tasks)
            {
                _spriteBatch.DrawString(Ressources.FontHack, t.TaskName, new Vector2(16, 64 * index), Color.Red);
                index++;
            }

            _spriteBatch.End();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (_currentTask == null || _currentTask.HasFinish)
            {
                if (_currentTask != null)
                {
                    DoneTasks.Add(_currentTask);
                }
                if (Tasks.Count > 0)
                {
                    _currentTask = Tasks.Dequeue();
                    _currentTask.RunTask(_game);
                }
            }
        }

        public override void Unload()
        {

        }
    }
}
