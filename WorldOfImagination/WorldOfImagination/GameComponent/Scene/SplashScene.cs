using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WorldOfImagination.GameComponent.Scene
{
    public class SplashScene : Scene
    {
        private readonly SpriteBatch sb;
        private Texture2D logo;
        public SplashScene(WorldOfImaginationGame game) : base(game)
        {
            sb = new SpriteBatch(Game.GraphicsDevice);
            
        }


        public override void Load()
        {
            logo = Game.Ress.img_maker_logo;
        }

        public override void Unload()
        {
            
        }
        bool once = true;
        public override void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalSeconds > 2 && once)
            {
                Game.Scene.Switch(new MainMenu(Game));
                once = false;
            }            
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.White);
            sb.Begin();
            sb.Draw(logo, new Vector2(Game.Graphics.GetWidth() / 2 - logo.Width / 2, Game.Graphics.GetHeight() / 2 - logo.Height / 2), Color.White);
            sb.End();
        }
    }
}
