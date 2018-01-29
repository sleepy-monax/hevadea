using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Menus;
using Maker.Hevadea.Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maker.Rise.Enums;
using Maker.Hevadea.UI;
using Maker.Rise.UI;

namespace Maker.Hevadea.Menus
{
    public class HUDMenu : Menu
    {
        ItemFrameControl itemFrame;
        PlayerInfoPanel playerInfo;
        public HUDMenu(GameManager game) : base(game)
        {
            var panelHost = new Panel() { Dock = Dock.Bottom, Bound = new Rectangle(64, 64, 64, 64), Padding = new Padding(0,0,8,8) };

            itemFrame = new ItemFrameControl() { Padding = new Padding(8), Dock = Dock.Left, Item = Game.Player.HoldingItem, Bound = new Rectangle(64, 64, 64, 64) };
            playerInfo = new PlayerInfoPanel(Game.Player) { Dock = Dock.Top, Bound = new Rectangle(64, 64, 64, 64) };

            panelHost.AddChild(itemFrame);
            panelHost.AddChild(playerInfo);

            AddChild(panelHost);
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        protected override void OnUpdate(GameTime gameTime)
        {

        }
    }
}
