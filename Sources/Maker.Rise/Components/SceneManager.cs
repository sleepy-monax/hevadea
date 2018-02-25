using Hevadea.Framework.Utils;
using Maker.Rise.Extension;
using Maker.Rise.Graphic;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Maker.Rise.Components
{
    public class SceneManager : GameComponent
    {
        public int Timer = 0;
        public bool IsSceneSwitching = false;
        public Scene CurrentScene;
        private Scene NextScene;

        public ParalaxeBackground Background = null;
        public SpriteBatch sb;
        public SceneManager(InternalGame game) : base(game)
        {
            CurrentScene = null;
            NextScene = null;
        }

        public override void Initialize()
        {
            sb = Engine.Graphic.CreateSpriteBatch();
        }

        public override void Update(GameTime gameTime)
        {
            if (IsSceneSwitching)
            {
                Timer += 3;
            }

            if (Timer == 120)
            {
                SwitchInternal();
            }

            if (Timer == 240)
            {
                IsSceneSwitching = false;
                Timer = 0;
            }


            if (CurrentScene != null)
            {
                if (CurrentScene.Container != null)
                {   
                    CurrentScene.Container.Bound = Engine.Graphic.GetResolutionRect();
                    CurrentScene.Container?.RefreshLayout();
                    CurrentScene.Container?.UpdateInternal(gameTime);
                }

                if (NextScene == null)
                {
                    CurrentScene.OnUpdate(gameTime);
                }
            }

        }

        public override void Draw(GameTime gameTime)
        {            
            if (Background != null)
                sb.BeginDrawEnd(Background.Draw, gameTime);

            CurrentScene?.OnDraw(gameTime);

                        
            if (CurrentScene?.Container != null)
            {
                sb.BeginDrawEnd(
                    CurrentScene.Container.DrawIternal,
                    gameTime,
                    new SpriteBatchBeginState{ SortMode = SpriteSortMode.Immediate, SamplerState = SamplerState.PointWrap, RasterizerState = Engine.CommonRasterizerState});
            }

            if (IsSceneSwitching)
            {
                DrawTransition();
            }
        }

        private void DrawTransition()
        {
            Engine.Graphic.Begin(sb);

            int sizeX = Engine.Graphic.GetWidth() / 80;
            int sizeY = Engine.Graphic.GetWidth() / 60;

            for (int x = 0; x < 80; x++)
            {
                for (int y = 0; y < 60; y++)
                {
                    int dd = (y + x % 2 * 2 + x / 3) - (Timer);
                    if (dd < 0 && dd > -120)
                    {
                        sb.FillRectangle(new Rectangle(x * sizeX, y * sizeY, sizeX, sizeY), Color.Black);  
                    }
                }
            }
            sb.End();
        }
        
        /// <summary>
        /// Switch the current scene to a another one.
        /// </summary>
        /// <remarks>
        /// The switch will be made on the next update-Draw cicle !
        /// </remarks>
        /// <param name="nextScene">Scene to switch.</param>
        public void Switch(Scene nextScene)
        {
            var s = new Stopwatch();
            NextScene = nextScene;

            s.Start();
            NextScene.Load();
            s.Stop();
            Logger.Log<SceneManager>(LoggerLevel.Fine, $"Scene: '{nextScene.GetType().FullName}' took {s.Elapsed.TotalSeconds}sec to load.");

            Timer = 0;
            IsSceneSwitching = true;
        }

        private void SwitchInternal()
        {
            if (NextScene == null) return;

            if (CurrentScene == null)
            {
                Logger.Log<SceneManager>(LoggerLevel.Fine, $"Switching scene to '{NextScene.GetType().FullName}'.");
            }
            else
            {
                var s = new Stopwatch();

                s.Start();
                CurrentScene.Unload();
                s.Stop();
                Logger.Log<SceneManager>(LoggerLevel.Fine, $"Scene: '{CurrentScene.GetType().FullName}' took {s.Elapsed.TotalSeconds}sec to unload.");


                Logger.Log<SceneManager>(LoggerLevel.Info, $"Scene switch from '{CurrentScene.GetType().FullName}' to '{NextScene.GetType().FullName}' is done.");
            }

            CurrentScene = NextScene;
            NextScene = null;
            //CurrentScene.Load();
        }
    }
}