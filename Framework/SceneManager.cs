using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Hevadea.Framework
{
    public class SceneManager
    {
        private Scene _currentScene;
        private Scene _nextScene;
        private ParalaxeBackground _background;
        private SpriteBatch _spritebatch;

        private object _switchlock = new object();

        public SceneManager()
        {
            _currentScene = null;
            _nextScene = null;
        }

        public void Initialize()
        {
            _spritebatch = Rise.Graphic.CreateSpriteBatch();
        }

        public void SetBackground(ParalaxeBackground background)
        {
            _background = background;
        }

        public Scene GetCurrentScene()
        {
            return _currentScene;
        }

        public void Switch(Scene nextScene)
        {
            lock (_switchlock)
            {
                var s = new Stopwatch();
                _nextScene = nextScene;

                s.Start();
                _nextScene.Load();
                _nextScene.RefreshLayout();
                s.Stop();
                Logger.Log<SceneManager>(LoggerLevel.Fine,
                    $"Scene: '{nextScene.GetType().FullName}' took {s.Elapsed.TotalSeconds}sec to load.");
            }
        }

        public void Update(GameTime gameTime)
        {
            if (_nextScene != null) SwitchInternal();

            _currentScene?.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            if (_background != null)
            {
                _spritebatch.Begin();
                _background.Draw(_spritebatch, gameTime);
                _spritebatch.End();

                _spritebatch.Begin();
                _spritebatch.FillRectangle(Rise.Graphic.GetBound(), Color.Black * 0.3f);
                _spritebatch.End();
            }

            _currentScene?.Draw(_spritebatch, gameTime);
        }

        private void SwitchInternal()
        {
            lock (_switchlock)
            {
                if (_nextScene == null) return;

                if (_currentScene == null)
                {
                    Logger.Log<SceneManager>(LoggerLevel.Fine,
                        $"Switching scene to '{_nextScene.GetType().FullName}'.");
                }
                else
                {
                    var s = new Stopwatch();

                    s.Start();
                    _currentScene.Unload();
                    s.Stop();
                    Logger.Log<SceneManager>(LoggerLevel.Fine,
                        $"Scene: '{_currentScene.GetType().FullName}' took {s.Elapsed.TotalSeconds}sec to unload.");

                    Logger.Log<SceneManager>(LoggerLevel.Info,
                        $"Scene switch from '{_currentScene.GetType().FullName}' to '{_nextScene.GetType().FullName}' is done.");
                }

                _currentScene = _nextScene;
                _nextScene = null;
            }
        }
    }
}