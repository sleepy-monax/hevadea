using Hevadea.Framework;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Game;
using Hevadea.Game.Entities.Component;
using Hevadea.Game.Registry;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hevadea.Scenes.Menus
{
    public class HudMenu : Menu
    {
        private readonly StatesWidget _playerStats;
        private readonly HotBarWidget _hotBar;

        private readonly Button btnUp, btnDown, btnLeft, btnRight, btnAttack, btnAction;

        public HudMenu(GameManager game) : base(game)
        {
            btnUp = new Button
            {
                Text = "UP",
                Anchor = Anchor.BottomLeft,
                Origine = Anchor.BottomLeft,
                Bound = new Rectangle(0,0,128,128),
                Offset = new Point(128, -128)
            };

            btnDown = new Button { Text = "DOWN",
                Anchor = Anchor.BottomLeft,
                Origine = Anchor.BottomLeft,
                Bound = new Rectangle(0, 0, 128, 128),
                Offset = new Point(128, 0)
            };

            btnLeft = new Button { Text = "LEFT",
                Anchor = Anchor.BottomLeft,
                Origine = Anchor.BottomLeft,
                Bound = new Rectangle(0, 0, 128, 128)
            };

            btnRight = new Button { Text = "RIGHT",
                Anchor = Anchor.BottomLeft,
                Origine = Anchor.BottomLeft,
                Bound = new Rectangle(0, 0, 128, 128),
                Offset = new Point(256, 0)
            };

            btnAttack = new Button { Text = "ATTACK",
                Anchor = Anchor.BottomRight,
                Origine = Anchor.BottomRight,
                Bound = new Rectangle(0, 0, 128, 128),
                Offset = new Point(-128, 0)
            };

            btnAction = new Button { Text = "ACTION",
                Anchor = Anchor.BottomRight,
                Origine = Anchor.BottomRight,
                Bound = new Rectangle(0, 0, 128, 128)
            };

            btnUp.MouseHold += Command;
            btnDown.MouseHold += Command;
            btnLeft.MouseHold += Command;
            btnRight.MouseHold += Command;

            btnAction.MouseHold += Command;
            btnAttack.MouseHold += Command;

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
                    _hotBar,
                    btnUp, btnDown, btnLeft, btnRight, btnAttack, btnAction,
                    new Button{ Text = "Pause", Origine = Anchor.TopLeft, Anchor = Anchor.TopLeft, Offset = new Point(16,16)}
                    .RegisterMouseClickEvent((sender)=>{ Game.CurrentMenu = new PauseMenu(Game); }),
                    
                    new Button{ Text = "Inventory", Origine = Anchor.TopLeft, Anchor = Anchor.TopLeft, Offset = new Point(16 + 96,16)}
                    .RegisterMouseClickEvent((sender)=>{ Game.CurrentMenu = new InventoryMenu(Game.Player, RECIPIES.HandCrafted,Game); })
                }
            };
        }

        private void Command(Widget sender)
        {

            var playerMovement = Game.Player.Components.Get<Move>();

            if (sender == btnLeft)
            {
                playerMovement.Do(-1, 0, Direction.Left);
            }
            if (sender == btnRight)
            {
                playerMovement.Do(+1, 0, Direction.Right);
            }
            if (sender == btnUp)
            {
                playerMovement.Do(0, -1, Direction.Up);
            }
            if (sender == btnDown)
            {
                playerMovement.Do(0, +1, Direction.Down);
            }

            if (sender == btnAttack) Game.Player.Components.Get<Attack>().Do(Game.Player.HoldingItem);

            if (sender == btnAction)
            {
                if (Game.Player.Components.Get<Inventory>().Content.Count(Game.Player.HoldingItem) == 0)
                    Game.Player.HoldingItem = null;

                Game.Player.Components.Get<Interact>().Do(Game.Player.HoldingItem);
            }
        }

        public override void Update(GameTime gameTime)
        {            
            if (Rise.Input.KeyPress(Keys.U)) _hotBar.Previouse();
            if (Rise.Input.KeyPress(Keys.I)) _hotBar.Next();
            Game.Player.Components.Get<Inventory>().AlowPickUp = true; //Rise.Input.KeyDown(Keys.F);
            if (Rise.Input.KeyPress(Keys.A))
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