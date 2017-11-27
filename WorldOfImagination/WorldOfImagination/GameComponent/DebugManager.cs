using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using WorldOfImagination.Utils;

namespace WorldOfImagination.GameComponent
{
    public class DebugManager : DrawableGameComponent
    {

        SpriteFont alagard;
        SpriteFont romulus;
        SpriteFont arial;
        SpriteBatch sb;
        WorldOfImaginationGame g;

        public DebugManager(Game game) : base(game)
        {
            g = (WorldOfImaginationGame)game;
            game.Components.Add(this);
        }

        public override void Initialize()
        {
            sb = new SpriteBatch(g.GraphicsDevice);
            
            alagard = g.Ressource.GetSpriteFont("alagard");
            romulus = g.Ressource.GetSpriteFont("romulus");
            arial = g.Ressource.GetSpriteFont("arial");

            Console.WriteLine($"{nameof(DebugManager)} initialized !");
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.DrawString(arial, $"Draw time : {g.DrawTime}ms", new Rectangle(16,16,16,16), Alignement.Left, Style.DropShadow, Color.White);
            sb.End();
            base.Draw(gameTime);
        }
    }
}