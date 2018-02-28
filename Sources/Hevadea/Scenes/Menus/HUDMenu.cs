using Hevadea.Game;
using Hevadea.Game.Entities.Component;
using Hevadea.Scenes.Widgets;
using Maker.Rise;
using Maker.Rise.UI.Widgets;
using Maker.Rise.UI.Widgets.Containers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hevadea.Scenes.Menus
{
    public class HudMenu : Menu
    {
        private readonly StatesWidget _playerStats;
        private readonly HotBarWidget _hotBar;

        public HudMenu(GameManager game) : base(game)
        {   
            _playerStats = new StatesWidget(game.Player)
            {
                Bound = new Rectangle(0, 0, 320, 64),
                Origine = Anchor.TopRight,
                Anchor = Anchor.TopRight,
                Offset = new Point(-16, 16)
            };
            
            _hotBar = new HotBarWidget(game.Player.Components.Get<Inventory>().Content)
            {
                Bound = new Rectangle(0, 0, 320, 64),
                Anchor = Anchor.Bottom,
                Origine = Anchor.Center,
                Offset = new Point(0, -48),
                ItemSize = 64,
            };
            _hotBar.ItemSelected += (sender, args) => { game.Player.HoldingItem = _hotBar.GetSelectedItem(); };

            Content = new AnchoredContainer
            {
                Childrens =
                {
                    _playerStats,
                    _hotBar
                }
            };
        }

        public override void Update(GameTime gameTime)
        {            
            if (Engine.Input.KeyPress(Keys.U)) _hotBar.Previouse();
            if (Engine.Input.KeyPress(Keys.I)) _hotBar.Next();
            Game.Player.Components.Get<Inventory>().AlowPickUp = Engine.Input.KeyDown(Keys.F);
            if (Engine.Input.KeyPress(Keys.A))
            {
                var level = Game.Player.Level;
                var item = Game.Player.HoldingItem;
                var facingTile = Game.Player.GetFacingTile();
                Game.Player.Components.Get<Inventory>().Content.DropOnGround(level, item, facingTile, 1);
            }
            
            base.Update(gameTime);
        }
    }
}