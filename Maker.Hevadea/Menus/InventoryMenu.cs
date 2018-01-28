using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.UI;
using Maker.Hevadea.Menus;
using Maker.Hevadea.UI;

using Maker.Rise;
using Maker.Rise.Enums;
using Maker.Rise.UI;

using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Menus
{
    public class InventoryMenu : Menu
    {
        private InventoryUi inv;
        private CraftingControl craft;
        private ItemFrameControl HoldingItemFrame;
        private PlayerEntity Player;


        public InventoryMenu(PlayerEntity entity, GameManager game) : base(game)
        {
            Player = entity;
            var HostPanel = new Panel { Dock = Dock.Fill, Layout = LayoutMode.Horizontal };

            var upperPanel = new Panel() { Padding = new Padding(0, 0, 16, 16), Dock = Dock.Top, Bound = new Rectangle(64, 64, 64, 64) };
            var RightPanel = new PrettyPanel() { Padding = new Padding(16) };
            var LeftPanel = new PrettyPanel() { Padding = new Padding(16)};

            HoldingItemFrame = new ItemFrameControl() { Padding = new Padding(8), Dock = Dock.Left, Item = Game.Player.HoldingItem, Bound = new Rectangle(64, 64, 64, 64) };


            var playerInfo = new PlayerInfoPanel(Game.Player) { Dock = Dock.Top, Bound = new Rectangle(64, 64, 64, 64) };




            inv = new InventoryUi(entity.Components.Get<Inventory>().Content)
            {
                Dock = Dock.Fill,
                Padding = new Padding(8)
            };

            inv.OnMouseClick +=
            (sender, args) =>
            {
                if (inv.SelectedItem != null )
                {
                    entity.HoldingItem = inv.SelectedItem;
                }
            };

            craft = new CraftingControl(entity.Components.Get<Inventory>().Content)
            {
                Dock = Dock.Fill
            };

            PauseGame = true;

            var closeButton = new Button { Text = "", Icon = EngineRessources.IconClose, Bound = new Rectangle(64, 64, 64, 64), Dock = Dock.Right };
            closeButton.OnMouseClick += CloseButtonMouseClick;
            upperPanel.AddChild(closeButton);
            upperPanel.AddChild(HoldingItemFrame);
            upperPanel.AddChild(playerInfo);
            AddChild(upperPanel);

            LeftPanel.AddChild(new Label(){Dock = Dock.Top, Text = "Inventory", Bound = new Rectangle(0,0,32,48)});
            LeftPanel.AddChild(inv);
            HostPanel.AddChild(LeftPanel);

            RightPanel.AddChild(new Label() { Dock = Dock.Top, Text = "Hand Crafting", Bound = new Rectangle(0, 0, 32, 48) });
            RightPanel.AddChild(craft);
            HostPanel.AddChild(RightPanel);

            AddChild(HostPanel);
            
        }

        private void CloseButtonMouseClick(object sender, System.EventArgs e)
        {
            Game.CurrentMenu = new HUDMenu(Game);
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            HoldingItemFrame.Item = Player.HoldingItem;
            base.OnUpdate(gameTime);
        }

    }
}