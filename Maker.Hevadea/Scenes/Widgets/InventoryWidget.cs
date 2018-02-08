using System;
using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Game.Registry;
using Maker.Rise;
using Maker.Rise.Enums;
using Maker.Rise.Extension;
using Maker.Rise.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Scenes.Widgets
{
    public class InventoryWidget : Widget
    {
        private readonly ItemStorage _inventory;
        private int _scrollOffset;

        public InventoryWidget(ItemStorage i)
        {
            _inventory = i;
        }

        public Item SelectedItem { get; set; }
        public Item HighlightedItem { get; set; } = null;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var index = 0;

            SelectedItem = null;
            var text = $"({_inventory.Items.Count}/{_inventory.Capacity})";
            var textSize = Ressources.FontRomulus.MeasureString(text);
            spriteBatch.FillRectangle(Bound, Color.White * 0.1f);
            spriteBatch.DrawString(Ressources.FontRomulus, text,
                new Vector2(Bound.X + Bound.Width - textSize.X - Padding.Left - 4,
                    Bound.Y + Bound.Height - textSize.Y - Padding.Down), Color.Gold);
            var maxScroll = 0;

            Engine.Graphic.GetGraphicsDevice().ScissorRectangle = Bound;

            foreach (var i in ITEMS.ById)
                if (i != null)
                {
                    var itemCount = _inventory.Count(i);

                    if (itemCount > 0)
                    {
                        var p = new Point(Host.X + 4, Host.Y + index * 52 + _scrollOffset);

                        var rect = new Rectangle(p.X, p.Y, Host.Width - 8, 48);
                        var sprite_rect = new Rectangle(p.X + 8, p.Y + 8, 32, 32);

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
                        spriteBatch.DrawString(Ressources.FontRomulus, $"{i.GetName()} {itemCount,3}x",
                            new Vector2(rect.X + 48, rect.Y + 12), Color.White);

                        maxScroll += 52;
                        index++;
                    }
                }

            Engine.Graphic.GetGraphicsDevice().ScissorRectangle = Engine.Graphic.GetResolutionRect();

            if (index == 0)
                spriteBatch.DrawString(Ressources.FontRomulus, "Empty", Bound, Alignement.Center, TextStyle.DropShadow,
                    Color.White);


            if (Bound.Contains(Engine.Input.MousePosition))
            {
                if (Engine.Input.MouseScrollUp) _scrollOffset = Math.Min(_scrollOffset + 16, 0);

                if (Engine.Input.MouseScrollDown)
                    _scrollOffset = Math.Max(_scrollOffset - 16, -maxScroll + Math.Min(maxScroll, Host.Height));
            }


            var contentHeight = Math.Max(maxScroll, Host.Height);
            var thumbHeight = Host.Height * (Host.Height / (float) contentHeight);
            var scrollJump = (contentHeight - Host.Height) / (Host.Height - thumbHeight);

            spriteBatch.FillRectangle(
                new Rectangle(Host.X + Host.Width - 2, Host.Y + (int) (-_scrollOffset / scrollJump), 2,
                    (int) thumbHeight), Color.Gold);
        }
    }
}