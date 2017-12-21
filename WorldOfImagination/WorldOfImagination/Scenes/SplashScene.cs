using System;
using Maker.Rise.Graphic.Path;
using Maker.Rise.Graphic.Path.Brushes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maker.Rise;
using Maker.Rise.GameComponent;

namespace WorldOfImagination.Scenes
{
    public class SplashScene : Scene
    {
        private readonly SpriteBatch sb;
        private readonly DrawBatch db;
        private Texture2D logo;
        public SplashScene(WorldOfImaginationGame game) : base(game)
        {
            sb = new SpriteBatch(Game.GraphicsDevice);
            db = new DrawBatch(Game.GraphicsDevice);
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
            db.Begin();
            db.FillRectangle(Brush.LightGray, Vector2.Zero, Game.Graphics.GetWidth(), Game.Graphics.GetHeight());  
            db.End();
            
            
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, Game.RasterizerState);

            sb.Draw(logo, new Vector2(Game.Graphics.GetWidth() / 2 - logo.Width / 2, Game.Graphics.GetHeight() / 2 - logo.Height / 2), Color.White);
            sb.End();
        }
    }
}
