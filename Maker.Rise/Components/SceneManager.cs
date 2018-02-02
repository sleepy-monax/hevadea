using Maker.Rise.Extension;
using Maker.Rise.Graphic;
using Maker.Rise.Ressource;
using Maker.Utils;
using Maker.Utils.Enums;
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

        private RenderTarget2D RT0;
        public RenderTarget2D RenderTarget { get; private set; }
        public RenderTarget2D BlurRT { get; set; }

        private BlurEffect blur;

        public SceneManager(InternalGame game) : base(game)
        {
            CurrentScene = null;
            NextScene = null;
        }

        public override void Initialize()
        {
            sb = Engine.Graphic.CreateSpriteBatch();

            ResetRenderTargets();

            blur = new BlurEffect();
            blur.Setup(Engine.Graphic.GetWidth(), Engine.Graphic.GetHeight());
        }

        public void ResetRenderTargets()
        {
            RenderTarget = Engine.Graphic.CreateFullscreenRenderTarget();
            BlurRT = Engine.Graphic.CreateFullscreenRenderTarget();
            RT0 = Engine.Graphic.CreateFullscreenRenderTarget();
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
                CurrentScene.Container.RefreshLayout(Engine.Graphic.GetResolutionRect());

                if (NextScene == null)
                {
                    CurrentScene.Container.Update(gameTime);
                    CurrentScene.Update(gameTime);
                }
            }

            NextScene?.Container.RefreshLayout(Engine.Graphic.GetResolutionRect());
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.ScissorRectangle =
                new Rectangle(0, 0, Engine.Graphic.GetWidth(), Engine.Graphic.GetHeight());


            Engine.Graphic.SetRenderTarget(RenderTarget);

            // Render the scene:
            Engine.Graphic.Begin(sb);
            Background?.Draw(sb, gameTime);
            sb.End();

            if (CurrentScene != null)
            {
                CurrentScene.Draw(gameTime);
            }

            Engine.Graphic.SetRenderTarget(RT0);

            sb.Begin(SpriteSortMode.Immediate, null, null, null, null, blur.Effect);
            blur.Use(true);
            sb.Draw(RenderTarget, Engine.Graphic.GetResolutionRect(), Color.White);
            sb.End();


            Engine.Graphic.SetRenderTarget(BlurRT);
            sb.Begin(SpriteSortMode.Immediate, null, null, null, null, blur.Effect);
            blur.Use(false);
            sb.Draw(RT0, Engine.Graphic.GetResolutionRect(), Color.White);
            sb.End();

            Engine.Graphic.SetRenderTarget(null);
            sb.Begin(SpriteSortMode.Immediate);
            sb.Draw(RenderTarget, Engine.Graphic.GetResolutionRect(), Color.White);
            sb.End();

            if (CurrentScene != null)
            {
                Engine.Ui.DrawUiTree(gameTime, CurrentScene.Container);
            }

            if (IsSceneSwitching)
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