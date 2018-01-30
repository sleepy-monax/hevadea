using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Game.Registry;
using Maker.Rise;
using Maker.Rise.Enums;
using Maker.Rise.Extension;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.Hevadea.Game.UI
{
    public class InventoryUi : Control
    {
        public ItemStorage Inventory;
        public Item SelectedItem { get; set; }
        public Item HighlightedItem { get; set; } = null;
        private int scrollOffset = 0;

        public InventoryUi(ItemStorage i)
        {
            Inventory = i;
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var index = 0;

            SelectedItem = null;
            var text = $"({Inventory.Items.Count}/{Inventory.Capacity})";
            var textSize = Ressources.fontRomulus.MeasureString(text);
            spriteBatch.FillRectangle(Bound, Color.White * 0.1f);
            spriteBatch.DrawString(Ressources.fontRomulus, text, new Vector2(Bound.X + Bound.Width - textSize.X - Padding.Left - 4, Bound.Y + Bound.Height - textSize.Y - Padding.Down), Color.Gold);
            var maxScroll = 0;

            Engine.Graphic.GetGraphicsDevice().ScissorRectangle = Host;
            foreach (var i in ITEMS.ById)
            {
                if (i != null)
                {
                    var itemCount = Inventory.Count(i);

                    if (itemCount > 0)
                    {
                        var p = new Point(Host.X + 4, Host.Y + index * 64 + scrollOffset);

                        var rect = new Rectangle(p.X, p.Y, Host.Width - 8, 48);
                        var sprite_rect = new Rectangle(p.X + 8, p.Y + 8,32,32);

                        if (rect.Contains(Engine.Input.MousePosition))
                        {
                            SelectedItem = i;

                            spriteBatch.FillRectangle(rect, Color.White * 0.05f);
                            spriteBatch.DrawRectangle(rect, Color.White * 0.05f);
                        }

                        if (i == HighlightedItem)
                        {

                            spriteBatch.FillRectangle(rect, Color.Gold * 0.5f);
                            spriteBatch.DrawRectangle(rect, Color.Gold * 0.5f);
                        }

                        i.GetSprite().Draw(spriteBatch, sprite_rect, Color.White);
                        spriteBatch.DrawString(Ressources.fontRomulus, $"{i.GetName()} {itemCount,3}x", new Vector2(rect.X + 48, rect.Y + 12), Color.White);

                        maxScroll += 64;
                        index++;
                    }
                }
            }

            if (index == 0)
            {
                spriteBatch.DrawString(Ressources.fontRomulus, "Empty", Bound, Alignement.Center, TextStyle.DropShadow, Color.White);
            }

            Engine.Graphic.GetGraphicsDevice().ScissorRectangle = Engine.Graphic.GetResolutionRect();

            if (Bound.Contains(Engine.Input.MousePosition))
            {
                if (Engine.Input.MouseScrollUp)
                {
                    scrollOffset = Math.Min(scrollOffset + 16, 0);
                }

                if (Engine.Input.MouseScrollDown)
                {
                    scrollOffset = Math.Max(scrollOffset - 16, -maxScroll + Math.Min(maxScroll, Host.Height));
                }
            }


            var contentHeight = Math.Max(maxScroll, Host.Height);

            var viewableRatio = Host.Height / (float)contentHeight;
            var scrollBarArea = Host.Height;
            var thumbHeight = scrollBarArea * viewableRatio;

            var scrollTrackSpace = contentHeight - Host.Height; // (600 - 200) = 400 
            var scrollThumbSpace = Host.Height - thumbHeight; // (200 - 50) = 150
            var scrollJump = scrollTrackSpace / scrollThumbSpace; //  (400 / 150 ) = 2.666666666666667

            spriteBatch.FillRectangle(new Rectangle(Host.X + Host.Width - 2, Host.Y + (int)(-scrollOffset / scrollJump), 2, (int)(thumbHeight)), Color.Gold);
        }

        protected override void OnUpdate(GameTime gameTime)
        {

        }
    }
}