using Maker.Hevadea.Game;
using Maker.Hevadea.Scenes.Widgets;
using Maker.Rise.UI.Widgets;
using Maker.Rise.UI.Widgets.Containers;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Scenes.Menus
{
    public class HUDMenu : Menu
    {
        private readonly ItemWidget _holdingItem;
        private readonly StatesWidget _playerStats;

        public HUDMenu(GameManager game) : base(game)
        {
            _playerStats = new StatesWidget(game.Player)
            {
                Bound = new Rectangle(0, 0, 320, 64),
                Origine = Anchor.TopRight,
                Anchor = Anchor.TopRight,
                Offset = new Point(-16, 16)
            };
            _holdingItem = new ItemWidget {Bound = new Rectangle(0, 0, 64, 64), Offset = new Point(16, 16)};

            Content = new AnchoredContainer
            {
                Childrens =
                {
                    _holdingItem,
                    _playerStats
                }
            };
        }

        public override void Update(GameTime gameTime)
        {
            _holdingItem.Item = Game.Player.HoldingItem;
            base.Update(gameTime);
        }
    }
}