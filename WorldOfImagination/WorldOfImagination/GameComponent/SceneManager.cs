using Microsoft.Xna.Framework;
using System;

namespace WorldOfImagination.GameComponent
{
    public class SceneManager : GameComponent
    {

        public Scene.Scene CurrentScene;
        private Scene.Scene NextScene;

        public SceneManager(WorldOfImaginationGame game) : base(game)
        {

            CurrentScene = null;
            NextScene = null;
        }

        public override void Initialize()
        {

        }

        public override void Update(GameTime gameTime)
        {
            SwitchInternal();
            if (CurrentScene != null)
            {
                CurrentScene.UiRoot.Bound =
                    new Rectangle(0, 0, Game.Graphics.GetWidth(), Game.Graphics.GetHeight());
                Game.UI.RefreshLayout();
                CurrentScene.Update(gameTime);
                
            }
        }

        public override void Draw(GameTime gameTime)
        {
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
            if (NextScene == null) return;
            
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