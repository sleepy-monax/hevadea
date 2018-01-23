using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Menus;
using Maker.Hevadea.Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maker.Rise.Enums;

namespace Maker.Hevadea.Menus
{
    public class HUDMenu : Menu
    {
        public HUDMenu(GameManager game) : base(game)
        {

            AddChild(new PlayerInfoPanel(Game.Player) { Dock = Dock.Bottom, Bound = new Rectangle(64, 64, 64, 64) });
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        protected override void OnUpdate(GameTime gameTime)
        {

        }
    }
}
