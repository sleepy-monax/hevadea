using System.Diagnostics;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.Scening
{
    public class SceneManager
    {
        private int _timer;
        private bool _isSceneSwitching;
        private Scene _currentScene;
        private Scene _nextScene;
        private ParalaxeBackground _background;
        private SpriteBatch _spritebatch;
        
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
            var s = new Stopwatch();
            _nextScene = nextScene;

            s.Start();
            _nextScene.Load();
            s.Stop();
            Logger.Log<SceneManager>(LoggerLevel.Fine, $"Scene: '{nextScene.GetType().FullName}' took {s.Elapsed.TotalSeconds}sec to load.");

            _timer = 0;
            _isSceneSwitching = true;
        }
        
        public void Update(GameTime gameTime)
        {
            if (_isSceneSwitching)
            {
                _timer += 3;
            }

            if (_timer == 120)
            {
                SwitchInternal();
            }
            else if (_timer == 240)
            {
                _isSceneSwitching = false;
                _timer = 0;
            }

            _currentScene?.Update(gameTime);

        }

        public void Draw(GameTime gameTime)
        {            
            if (_background != null)
                _spritebatch.BeginDrawEnd(_background.Draw, gameTime);

            if (_currentScene != null)
            {   
                _spritebatch.Begin(new SpriteBatchBeginState{ SortMode = SpriteSortMode.Immediate, SamplerState = SamplerState.PointWrap, RasterizerState = new RasterizerState(){ScissorTestEnable = true}});
                _currentScene.Draw(_spritebatch, gameTime);
                _spritebatch.End();
            }

            if (_isSceneSwitching)
            {
                DrawTransition();
            }
        }

        private void DrawTransition()
        {
            _spritebatch.Begin();

            var sizeX = Rise.Graphic.GetWidth() / 80 + 1;
            var sizeY = Rise.Graphic.GetWidth() / 60 + 1;

            for (var x = 0; x < 80; x++)
            {
                for (var y = 0; y < 60; y++)
                {
                    var dd = (y + x % 2 * 2 + x / 3) - (_timer);
                    if (dd < 0 && dd > -120)
                    {
                        _spritebatch.FillRectangle(new Rectangle(x * sizeX, y * sizeY, sizeX, sizeY), Color.Black);  
                    }
                }
            }
            _spritebatch.End();
        }
        
        private void SwitchInternal()
        {
            if (_nextScene == null) return;

            if (_currentScene == null)
            {
                Logger.Log<SceneManager>(LoggerLevel.Fine, $"Switching scene to '{_nextScene.GetType().FullName}'.");
            }
            else
            {
                var s = new Stopwatch();

                s.Start();
                _currentScene.Unload();
                s.Stop();
                Logger.Log<SceneManager>(LoggerLevel.Fine, $"Scene: '{_currentScene.GetType().FullName}' took {s.Elapsed.TotalSeconds}sec to unload.");


                Logger.Log<SceneManager>(LoggerLevel.Info, $"Scene switch from '{_currentScene.GetType().FullName}' to '{_nextScene.GetType().FullName}' is done.");
            }

            _currentScene = _nextScene;
            _nextScene = null;
        }
    }
}