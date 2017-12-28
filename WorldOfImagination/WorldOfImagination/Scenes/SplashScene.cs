using System;
using Maker.Rise.Graphic.Path;
using Maker.Rise.Graphic.Path.Brushes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maker.Rise;
using Maker.Rise.Components;

namespace WorldOfImagination.Scenes
{
    public class SplashScene : Scene
    {
        private SpriteBatch sb;
        private DrawBatch db;
        private Texture2D logo;

        public SplashScene()
        {

        }


        public override void Load()
        {
            sb = new SpriteBatch(Engine.Graphic.GraphicsDevice);
            db = new DrawBatch(Engine.Graphic.GraphicsDevice);
            logo = EngineRessources.img_maker_logo;

            Ressources.Load();
            Engine.SetMouseVisibility(true);
            Engine.SetFullScreen(true);
        }

        public override void Unload()
        {
            
        }
        bool once = true;
        public override void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalSeconds > 2 && once)
            {
                Engine.Scene.Switch(new MainMenu());
                once = false;
            }            
        }

        public override void Draw(GameTime gameTime)
        {
            db.Begin();
            db.FillRectangle(Brush.LightGray, Vector2.Zero, Engine.Graphic.GetWidth(), Engine.Graphic.GetHeight());  
            db.End();
            
            
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, Engine.CommonRasterizerState);

            sb.Draw(logo, new Vector2(Engine.Graphic.GetWidth() / 2 - logo.Width / 2, Engine.Graphic.GetHeight() / 2 - logo.Height / 2), Color.White);
            sb.End();
        }
    }
}
