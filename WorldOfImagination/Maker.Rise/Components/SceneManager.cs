using Maker.Rise.Extension;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Maker.Rise.Components
{
    public class SceneManager : GameComponent
    {
        public Scene CurrentScene;
        private Scene NextScene;

        private Animation animation;

        public SceneManager(RiseGame game) : base(game)
        {
            animation = new Animation();
            CurrentScene = null;
            NextScene = null;
        }

        public override void Initialize()
        {
        }

        public override void Update(GameTime gameTime)
        {
            animation.Update(gameTime);

            if (animation.TwoPhases == 1f)
            {
                SwitchInternal();
                animation.Show = false;
            }

            if (CurrentScene != null)
            {
                CurrentScene.UiRoot.Bound = new Rectangle(
                    0, 0,
                    Engine.Graphic.GetWidth(),
                    Engine.Graphic.GetHeight());

                CurrentScene.UiRoot.RefreshLayout();

                CurrentScene.UiRoot.Update(gameTime);
                CurrentScene.Update(gameTime);
            }

            if (NextScene != null)
            {
                NextScene.UiRoot.Bound = new Rectangle(
                    0, 0,
                    Engine.Graphic.GetWidth(),
                    Engine.Graphic.GetHeight());

                NextScene.UiRoot.RefreshLayout();

                NextScene.UiRoot.Update(gameTime);
                NextScene.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.ScissorRectangle =
                new Rectangle(0, 0, Engine.Graphic.GetWidth(), Engine.Graphic.GetHeight());

            if (CurrentScene != null)
            {
                CurrentScene.Draw(gameTime);
                Engine.Ui.DrawUiTree(gameTime, CurrentScene.UiRoot);
            }

            var height = (int) (Engine.Graphic.GetHeight() * animation.SinTwoPhases);
            var width = (int) (Engine.Graphic.GetWidth() * animation.SinTwoPhases);
            var rect = new Rectangle(Engine.Graphic.GetWidth() / 2 - width / 2,
                Engine.Graphic.GetHeight() / 2 - height / 2, width, height);

            Game.GraphicsDevice.ScissorRectangle = rect;

            if (NextScene != null)
            {
                NextScene.Draw(gameTime);
                Engine.Ui.DrawUiTree(gameTime, NextScene.UiRoot);
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
            Console.WriteLine("\n--- SCENE SWITCH BEGIN ---");

            var s = new Stopwatch();
            NextScene = nextScene;

            s.Start();
            NextScene.Load();
            NextScene.UiRoot.RefreshLayout();
            s.Stop();

            Console.WriteLine($"'{nextScene.GetType().FullName}' took {s.Elapsed.TotalSeconds}s to load.");

            animation.Show = true;
            animation.Speed = 0.75f;
            animation.Reset();
        }

        private void SwitchInternal()
        {
            if (NextScene == null) return;

            if (CurrentScene == null)
            {
                Console.WriteLine($"Switching scene to '{NextScene.GetType().FullName}'.");
            }
            else
            {
                var s = new Stopwatch();

                s.Start();
                CurrentScene.Unload();
                s.Stop();
                Console.WriteLine($"'{CurrentScene.GetType().FullName}' took {s.Elapsed.TotalSeconds}s to unload.");

                Console.WriteLine($"Switching scene from '{CurrentScene.GetType().FullName}'");
                Console.WriteLine($"                  to '{NextScene.GetType().FullName}'.");
            }

            CurrentScene = NextScene;
            NextScene = null;
            Console.WriteLine("--- SCENE SWITCH END ---");
            //CurrentScene.Load();
        }
    }
}