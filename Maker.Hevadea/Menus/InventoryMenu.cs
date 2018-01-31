using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.UI;
using Maker.Hevadea.UI;

using Maker.Rise;
using Maker.Rise.Enums;
using Maker.Rise.Extension;
using Maker.Rise.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Menus
{
    public class InventoryMenu : Menu
    {
        private InventoryUi inv;
        private CraftingControl craft;
        private PlayerEntity Player;


        public InventoryMenu(PlayerEntity entity, GameManager game) : base(game)
        {
            Player = entity;
            var HostPanel = new Panel { Dock = Dock.Fill, Layout = LayoutMode.Horizontal };

            var RightPanel = new PrettyPanel() { Padding = new Padding(8) };
            var LeftPanel = new PrettyPanel() { Padding = new Padding(8) };

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
                Dock = Dock.Fill,
                Padding = new Padding(8)
            };

            PauseGame = true;


            LeftPanel.AddChild(new Label(){Font = Ressources.fontAlagard, Dock = Dock.Top, Text = "Inventory", Bound = new Rectangle(0,0,32,48)});
            LeftPanel.AddChild(inv);
            HostPanel.AddChild(LeftPanel);

            RightPanel.AddChild(new Label() { Font = Ressources.fontAlagard, Dock = Dock.Top, Text = "Hand Crafting", Bound = new Rectangle(0, 0, 32, 48) });
            RightPanel.AddChild(craft);
            HostPanel.AddChild(RightPanel);

            AddChild(HostPanel);
            
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(Engine.Graphic.GetResolutionRect(), Color.Black * 0.1f);
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            inv.HighlightedItem = Player.HoldingItem;
            base.OnUpdate(gameTime);
        }

    }
}