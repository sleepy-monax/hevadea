using Microsoft.Xna.Framework;
using System;

namespace WorldOfImagination.GameComponent
{
    public class SceneManager : DrawableGameComponent
    {

        public Scene.Scene CurrentScene { get; private set; }
        private Scene.Scene NextScene;

        public SceneManager(Game game) : base(game)
        {
            game.Components.Add(this);
            CurrentScene = null;
            NextScene = null;
        }

        public override void Initialize()
        {
            base.Initialize();
            Console.WriteLine($"{nameof(SceneManager)} initialized !");
        }

        public override void Update(GameTime gameTime)
        {
            SwitchInternal();
            CurrentScene?.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            CurrentScene?.Draw(gameTime);
        }

        /// <summary>
        /// Switch the current scene to a another one.
        /// </summary>
        /// <remarks>
        /// The switch will be made on the next update-Draw cicle !
        /// </remarks>
        /// <param name="nextScene">Scene to switch.</param>
        public void Switch(Scene.Scene nextScene)
        {
            NextScene = nextScene;
        }

        private void SwitchInternal()
        {
            if (NextScene != null)
            {
                if (CurrentScene == null)
                {
                    Console.WriteLine($"Switching scene to '{NextScene.GetType().FullName}'.");
                }
                else
                {
                    Console.WriteLine($"Switching scene from '{CurrentScene.GetType().FullName}' to '{NextScene.GetType().FullName}'.");
                    CurrentScene.Unload();
                }

                CurrentScene = NextScene;
                NextScene = null;

                CurrentScene.Load();
            }
        }
    }
}