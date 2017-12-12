using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using WorldOfImagination.GameComponent.UI;
using WorldOfImagination.Utils;

namespace WorldOfImagination.GameComponent
{
    public class SceneManager : GameComponent
    {
        public Scene.Scene CurrentScene;
        private Animation animation;
        private Scene.Scene NextScene;
        private SpriteBatch sb;
        private readonly RasterizerState _rasterizerState;

        public SceneManager(WorldOfImaginationGame game) : base(game)
        {
            animation = new Animation();
            CurrentScene = null;
            NextScene = null;
            
            _rasterizerState = new RasterizerState() { ScissorTestEnable = true};
            
        }

        public override void Initialize()
        {
            sb = new SpriteBatch(Game.GraphicsDevice);
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
            
            sb.Begin(SpriteSortMode.Immediate, null, null, null, _rasterizerState);
            var height = (int) (Game.Graphics.GetHeight() * MathUtils.Interpolate(animation.TwoPhases) );
            var width = (int) (Game.Graphics.GetWidth() * MathUtils.Interpolate(animation.TwoPhases) );
            var rect = new Rectangle(Game.Graphics.GetWidth() / 2 - width / 2,
            Game.Graphics.GetHeight() / 2 - height / 2, width, height);
            Game.GraphicsDevice.ScissorRectangle = rect;
            sb.Draw(Game.Ress.img_menu_background, new Rectangle(0, 0, Game.Graphics.GetWidth(), Game.Graphics.GetHeight()), Color.White * animation.TwoPhases);
            Game.GraphicsDevice.ScissorRectangle =
                new Rectangle(0, 0, Game.Graphics.GetWidth(), Game.Graphics.GetHeight());
            //sb.FillRectangle(rect, Color.Black * animation.SinTwoPhases);
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