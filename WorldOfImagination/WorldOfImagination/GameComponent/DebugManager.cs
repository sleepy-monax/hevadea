using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using WorldOfImagination.Utils;

namespace WorldOfImagination.GameComponent
{
    public class DebugManager : GameComponent
    {
        
        SpriteBatch sb;
        public bool Visible = false;
        
        public DebugManager(WorldOfImaginationGame game) : base(game)
        {
        }

        public override void Initialize()
        {
            sb = new SpriteBatch(Game.GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {            
                sb.Begin();
                sb.DrawString(Game.Ress.arial, $"Draw time : {Game.DrawTime}ms", new Rectangle(16,16,16,16), Alignement.Left, Style.DropShadow, Color.White);
                sb.End();
            }
        }
    }
}