using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.Platform;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.Utils;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.Player;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Menus
{
    public class MenuInGame : Menu
    {
        private readonly WidgetPlayerStats _playerStats;
        private readonly Widget _minimap;
        private readonly WidgetHotBar _hotBar;

        private readonly Button btnAttack, btnAction, btnPickup, btnDrop;

        private readonly SpriteButton btnMinimap;

        public MenuInGame(GameManager.GameManager game) : base(game)
        {
            // On screen button -----------------------------------------------

            btnAttack = new Button
            {
                Anchor = Anchor.BottomRight,
                Origine = Anchor.BottomRight,
                Text = "ATTACK",
                UnitBound = new Rectangle(0, 0, 128, 128),
                UnitOffset = new Point(-128, 0)
            };

            btnAction = new Button
            {
                Anchor = Anchor.BottomRight,
                Origine = Anchor.BottomRight,
                Text = "ACTION",
                UnitBound = new Rectangle(0, 0, 128, 128)
            };

            btnPickup = new Button
            {
                Anchor = Anchor.BottomRight,
                Origine = Anchor.BottomRight,
                Text = "PICKUP",
                UnitBound = new Rectangle(0, 0, 128, 128),
                UnitOffset = new Point(0, -128),
            };

            btnDrop = new Button
            {
                Anchor = Anchor.BottomRight,
                Origine = Anchor.BottomRight,
                Text = "DROP",
                UnitBound = new Rectangle(0, 0, 128, 128),
                UnitOffset = new Point(-128, -128)
            };

            btnMinimap = new SpriteButton
            {
                Anchor = Anchor.TopRight,
                IsEnable = false,
                Origine = Anchor.TopRight,
                Sprite = new Sprite(Ressources.TileIcons, new Point(5, 4)),
                UnitOffset = new Point(-16, 16),
            };

            // Hud ------------------------------------------------------------

            _playerStats = new WidgetPlayerStats(game.MainPlayer)
            {
                Anchor = Anchor.TopLeft,
                Origine = Anchor.TopLeft,
                UnitBound = new Rectangle(0, 0, 320, 64),
                UnitOffset = new Point(16, 16),
            };


            _minimap = new WidgetFancyPanel()
            {
                Anchor = Anchor.TopRight,
                Origine = Anchor.TopRight,
                UnitBound = new Rectangle(0, 0, 320, 320),
                UnitOffset = new Point(-16, 16),
                Content = new WidgetMinimap(game)
                
            }; 

            _hotBar = new WidgetHotBar(Game.MainPlayer.GetComponent<Inventory>().Content)
            {
                Anchor = Anchor.Bottom,
                Origine = Anchor.Bottom,
                UnitOffset = new Point(0, -16)
            };
            
            Content = new AnchoredContainer
            {
                Childrens =
                {
                    _playerStats, _minimap, btnMinimap, _hotBar,

                    new Button{ Text = "Inventory", Origine = Anchor.BottomLeft, Anchor = Anchor.BottomLeft, UnitOffset = new Point(16,-16)}
                    .RegisterMouseClickEvent((sender)=>{ Game.CurrentMenu = new PlayerInventoryMenu(Game); })
                }
            };

            btnAction.MouseClick  += (sender) => { Game.PlayerInput.HandleInput(PlayerInput.Action); };
            btnAttack.MouseHold   += (sender) => { Game.PlayerInput.HandleInput(PlayerInput.Attack); };
            btnDrop.MouseClick    += (sender) => { Game.PlayerInput.HandleInput(PlayerInput.DropItem); };
            btnPickup.MouseClick  += (sender) => { Game.PlayerInput.HandleInput(PlayerInput.Pickup); };
            btnMinimap.MouseClick += (sender) => { _minimap.Enable(); sender.Disable(); };

            _minimap.MouseClick += (sender) => { sender.Disable(); btnMinimap.Enable(); };

            if (Rise.Platform.Family == PlatformFamily.Mobile)
            {
                var c = (Container)Content;
                c.AddChilds(btnAttack, btnAction, btnDrop, btnPickup);
            }
        }
    }
}