using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.Utils;
using Hevadea.Game;
using Hevadea.Game.Registry;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Menus
{
    public class MenuInGame : Menu
    {
        private readonly WidgetPlayerStats _playerStats;
        private readonly Button btnAttack, btnAction, btnPickup, btnDrop;

        public MenuInGame(GameManager game) : base(game)
        {
            btnAttack = new Button
            {
                Text = "ATTACK",
                Anchor = Anchor.BottomRight,
                Origine = Anchor.BottomRight,
                UnitBound = new Rectangle(0, 0, 128, 128),
                UnitOffset = new Point(-128, 0)
            };

            btnAction = new Button
            {
                Text = "ACTION",
                Anchor = Anchor.BottomRight,
                Origine = Anchor.BottomRight,
                UnitBound = new Rectangle(0, 0, 128, 128)
            };

            btnPickup = new Button
            {
                Text = "PICKUP",
                Anchor = Anchor.BottomRight,
                Origine = Anchor.BottomRight,
                UnitBound = new Rectangle(0, 0, 128, 128),
                UnitOffset = new Point(0, -128),
            };

            btnDrop = new Button
            {
                Text = "DROP",
                Anchor = Anchor.BottomRight,
                Origine = Anchor.BottomRight,
                UnitBound = new Rectangle(0, 0, 128, 128),
                UnitOffset = new Point(-128, -128)
            };

            btnAction.MouseClick += (sender) => { Game.PlayerInput.HandleInput(PlayerInput.Action); };
            btnAttack.MouseHold += (sender) => { Game.PlayerInput.HandleInput(PlayerInput.Attack); };
            btnPickup.MouseClick += (sender) => { Game.PlayerInput.HandleInput(PlayerInput.Pickup); };
            btnDrop.MouseClick += (sender) => { Game.PlayerInput.HandleInput(PlayerInput.DropItem); };

            _playerStats = new WidgetPlayerStats(game.MainPlayer)
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
                    btnAttack, btnAction, btnDrop, btnPickup,

                    new Button{ Text = "Inventory", Origine = Anchor.TopLeft, Anchor = Anchor.TopLeft, UnitOffset = new Point(16,16)}
                    .RegisterMouseClickEvent((sender)=>{ Game.CurrentMenu = new PlayerInventoryMenu(Game); })
                }
            };
        }
    }
}