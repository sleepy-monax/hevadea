using Hevadea.Framework;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Game;
using Hevadea.Game.Entities.Components;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Registry;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hevadea.Scenes.Menus
{
    public class MenuInGame : Menu
    {
        private readonly WidgetPlayerStats _playerStats;

        private readonly Button btnAttack, btnAction;

        public MenuInGame(GameManager game) : base(game)
        {
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

            btnAction.MouseClick += (sender) => { Game.PlayerInput.HandleInput(PlayerInput.Action); };
            btnAttack.MouseHold += (sender) => { Game.PlayerInput.HandleInput(PlayerInput.Attack); };

            _playerStats = new WidgetPlayerStats(game.Player)
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
                    btnAttack, btnAction,
                    new Button{ Text = "Pause", Origine = Anchor.TopLeft, Anchor = Anchor.TopLeft, UnitOffset = new Point(16,16)}
                    .RegisterMouseClickEvent((sender)=>{ Game.CurrentMenu = new MenuGamePaused(Game); }),
                    
                    new Button{ Text = "Inventory", Origine = Anchor.TopLeft, Anchor = Anchor.TopLeft, UnitOffset = new Point(16 + 96,16)}
                    .RegisterMouseClickEvent((sender)=>{ Game.CurrentMenu = new MenuPlayerInventory(Game.Player, RECIPIES.HandCrafted,Game); })
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