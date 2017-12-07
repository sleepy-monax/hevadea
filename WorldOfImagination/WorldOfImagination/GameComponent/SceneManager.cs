using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using WorldOfImagination.GameComponent.UI;
using WorldOfImagination.Utils;

namespace WorldOfImagination.GameComponent
{
    public class SceneManager : GameComponent
    {
        private string loadingMessage = "Loading";
        private Vector2 loadingMessageSize = Vector2.Zero;
        public Scene.Scene CurrentScene;
        private Animation animation;
        private Scene.Scene NextScene;
        private SpriteBatch sb;

        public SceneManager(WorldOfImaginationGame game) : base(game)
        {
            animation = new Animation();
            CurrentScene = null;
            NextScene = null;
            
        }

        public override void Initialize()
        {
            sb = new SpriteBatch(Game.GraphicsDevice);
            loadingMessageSize = Game.Ress.font_bebas.MeasureString(loadingMessage);
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
                CurrentScene.UiRoot.Bound =
                    new Rectangle(0, 0, Game.Graphics.GetWidth(), Game.Graphics.GetHeight());
                Game.UI.RefreshLayout();
                CurrentScene.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            CurrentScene?.Draw(gameTime);
            Game.UI.Draw(gameTime);
            
            sb.Begin();
            var height = (int) (Game.Graphics.GetHeight() * MathUtils.Interpolate(animation.TwoPhases));
            sb.FillRectangle(new Rectangle(0, Game.Graphics.GetHeight() / 2 - height/2, Game.Graphics.GetWidth(), height), Color.Black * animation.Linear);
            sb.DrawString(Game.Ress.font_bebas, loadingMessage, new Vector2(Game.Graphics.GetWidth() / 2 - loadingMessageSize.X / 2, Game.Graphics.GetHeight() / 2 - loadingMessageSize.Y / 2), Color.White * animation.Linear);
            //sb.DrawCircle(new Vector2(Game.Graphics.GetWidth() / 2f, Game.Graphics.GetHeight() / 2f),  10 + (int)(loadingMessageSize.X *Math.Sin(animation.ValueLinear * 3)), 10, Color.White * animation.ValueLinear);
            sb.End();
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
                Console.WriteLine($"Switching scene from '{CurrentScene.GetType().FullName}' to '{NextScene.GetType().FullName}'.");
                CurrentScene.Unload();
            }

            CurrentScene = NextScene;
            NextScene = null;

            CurrentScene.Load();
        }
    }
}