using Hevadea.Framework;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Game;
using Hevadea.Game.Entities.Component;
using Hevadea.Game.Entities.Component.Attributes;
using Hevadea.Game.Registry;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hevadea.Scenes.Menus
{
    public class HudMenu : Menu
    {
        private readonly StatesWidget _playerStats;

        private readonly Button btnUp, btnDown, btnLeft, btnRight, btnAttack, btnAction;

        public HudMenu(GameManager game) : base(game)
        {
            btnUp = new Button
            {
                Text = "UP",
                Anchor = Anchor.BottomLeft,
                Origine = Anchor.BottomLeft,
                UnitBound = new Rectangle(0,0,128,128),
                UnitOffset = new Point(128, -128)
            };

            btnDown = new Button { Text = "DOWN",
                Anchor = Anchor.BottomLeft,
                Origine = Anchor.BottomLeft,
                UnitBound = new Rectangle(0, 0, 128, 128),
                UnitOffset = new Point(128, 0)
            };

            btnLeft = new Button { Text = "LEFT",
                Anchor = Anchor.BottomLeft,
                Origine = Anchor.BottomLeft,
                UnitBound = new Rectangle(0, 0, 128, 128)
            };

            btnRight = new Button { Text = "RIGHT",
                Anchor = Anchor.BottomLeft,
                Origine = Anchor.BottomLeft,
                UnitBound = new Rectangle(0, 0, 128, 128),
                UnitOffset = new Point(256, 0)
            };

            btnAttack = new Button { Text = "ATTACK",
                Anchor = Anchor.BottomRight,
                Origine = Anchor.BottomRight,
                UnitBound = new Rectangle(0, 0, 128, 128),
                UnitOffset = new Point(-128, 0)
            };

            btnAction = new Button { Text = "ACTION",
                Anchor = Anchor.BottomRight,
                Origine = Anchor.BottomRight,
                UnitBound = new Rectangle(0, 0, 128, 128)
            };

            btnUp.MouseHold += (sender) => { Game.PlayerInput.HandleInput(PlayerInput.MoveUp); };
            btnDown.MouseHold += (sender) => { Game.PlayerInput.HandleInput(PlayerInput.MoveDown); };
            btnLeft.MouseHold += (sender) => { Game.PlayerInput.HandleInput(PlayerInput.MoveLeft); };
            btnRight.MouseHold += (sender) => { Game.PlayerInput.HandleInput(PlayerInput.MoveRight); };

            btnAction.MouseClick += (sender) => { Game.PlayerInput.HandleInput(PlayerInput.Action); };
            btnAttack.MouseHold += (sender) => { Game.PlayerInput.HandleInput(PlayerInput.Attack); };

            _playerStats = new StatesWidget(game.Player)
            {
                UnitBound = new Rectangle(0, 0, 320, 64),
                Origine = Anchor.TopRight,
                Anchor = Anchor.TopRight,
                UnitOffset = new Point(-16, 16)
            };
            

            Content = new AnchoredContainer
            {
                Childrens =
                {
                    _playerStats,
                    btnUp, btnDown, btnLeft, btnRight, btnAttack, btnAction,
                    new Button{ Text = "Pause", Origine = Anchor.TopLeft, Anchor = Anchor.TopLeft, UnitOffset = new Point(16,16)}
                    .RegisterMouseClickEvent((sender)=>{ Game.CurrentMenu = new PauseMenu(Game); }),
                    
                    new Button{ Text = "Inventory", Origine = Anchor.TopLeft, Anchor = Anchor.TopLeft, UnitOffset = new Point(16 + 96,16)}
                    .RegisterMouseClickEvent((sender)=>{ Game.CurrentMenu = new InventoryMenu(Game.Player, RECIPIES.HandCrafted,Game); })
                }
            };
        }

        public override void Update(GameTime gameTime)
        {            
            Game.Player.Get<Inventory>().AlowPickUp = true; //Rise.Input.KeyDown(Keys.F);
            if (Rise.Input.KeyPress(Keys.A))
            {
                var level = Game.Player.Level;
                var item = Game.Player.HoldingItem;
                var facingTile = Game.Player.GetFacingTile();
                Game.Player.Get<Inventory>().Content.DropOnGround(level, item, facingTile, 1);
            }
            
            base.Update(gameTime);
        }
    }
}