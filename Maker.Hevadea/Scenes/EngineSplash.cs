using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Scenes
{
    class EngineSplash : Scene
    {
        private SpriteBatch sb;
        private Texture2D logo;

        public override void Load()
        {
            sb = Engine.Graphic.CreateSpriteBatch();
            logo = Ressources.img_engine_logo;;
        }

        public override void Unload()
        {
        }

        bool once = true;

        public override void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalSeconds > 4 && once)
            {
                Engine.Scene.Switch(new MainMenu());
                once = false;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, Engine.CommonRasterizerState);
            sb.FillRectangle(new Rectangle(0, 0, Engine.Graphic.GetWidth(), Engine.Graphic.GetHeight()), Color.Black);
            sb.Draw(logo, new Vector2(Engine.Graphic.GetWidth() / 2 - logo.Width / 2, Engine.Graphic.GetHeight() / 2 - logo.Height / 2), Color.White);
            sb.End();
        }
    }
}
