using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WorldOfImagination.Utils;

namespace WorldOfImagination.GameComponent.Scene
{
    public class MainMenu : Scene
    {
        SpriteBatch sb;
        SpriteFont alagard;
        SpriteFont romulus;
        WorldOfImaginationGame g;

        public MainMenu(Game game) : base(game)
        {
            sb = new SpriteBatch(game.GraphicsDevice);
        }

        public override void Load()
        {
            g = (WorldOfImaginationGame)Game;
            alagard = g.Ressource.GetSpriteFont("alagard");
            romulus = g.Ressource.GetSpriteFont("romulus");
        }

        public override void Unload()
        {

        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap);

            var titleRect = new Rectangle(0, (int)(g.Graphics.GetHeight() * (3/8f)), g.Graphics.GetWidth(), g.Graphics.GetHeight() / 4);

            sb.FillRectangle(titleRect, new Color(0, 0, 0, 100));
            sb.DrawString(alagard, "World Of Imagination", titleRect, Alignement.Center, Style.DropShadow, Color.White);
            sb.DrawString(romulus, "\n\n\nTale of the foreigner", titleRect, Alignement.Center, Style.Regular, Color.Gold);
            sb.DrawString(romulus, "\n\n\n\nJe suis une pomme", titleRect, Alignement.Center, Style.Regular, Color.Magenta);
            sb.End();
        }

        public override void Update(GameTime gameTime)
        {
            
        }

    }
}
