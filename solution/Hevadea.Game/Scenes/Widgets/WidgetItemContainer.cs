using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.UI;
using Hevadea.Items;
using Hevadea.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Hevadea.Scenes.Widgets
{
    public class WidgetItemContainer : Widget
    {
        public ItemStorage Content { get; }
        private int _scrollOffset;

        public WidgetItemContainer(ItemStorage i)
        {
            Content = i;
        }

        public Item SelectedItem { get; set; }
        public Item HighlightedItem { get; set; } = null;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var index = 0;

            var text = $"({Content.Count()}/{Content.Capacity})";
            var textSize = Resources.FontRomulus.MeasureString(text);
            spriteBatch.DrawString(Resources.FontRomulus, text,
                new Vector2(Bound.X + Bound.Width - textSize.X - Padding.Left - Scale(4),
                    Bound.Y + Bound.Height - textSize.Y - Padding.Bottom), Color.Gold);

            Rise.Graphic.SetScissor(Bound);

            foreach (var i in ITEMS.ById)
                if (i != null)
                {
                    var itemCount = Content.Count(i);

                    if (itemCount > 0)
                    {
                        var p = new Point(Host.X + Scale(4), Host.Y + index * Scale(52) + _scrollOffset);

                        var rect = new Rectangle(p.X, p.Y, Host.Width - Scale(8), Scale(48));
                        var sprite_rect = new Rectangle(p.X + Scale(8), p.Y + Scale(8), Scale(32), Scale(32));

                        if (Rise.Pointing.AreaOver(rect))
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

                        spriteBatch.DrawSprite(i.Sprite, sprite_rect, Color.White);
                        spriteBatch.DrawString(Resources.FontRomulus, $"{i.Name} {itemCount,3}x",
                            new Vector2(rect.X + Scale(48), rect.Y + Scale(12)), Color.White);

                        index++;
                    }
                }

            if (Content.GetStackCount() == 0)
                spriteBatch.DrawString(Resources.FontRomulus, "Empty", Bound, TextAlignement.Center,
                    TextStyle.DropShadow,
                    Color.White);

            if (Rise.Pointing.AreaOver(Bound))
            {
                // TODO mouse zoom in and out
                //if (Rise.Input.MouseScrollUp) _scrollOffset += Scale(16);
                //if (Rise.Input.MouseScrollDown) _scrollOffset -= Scale(16);

                var maxScroll = Content.GetStackCount() * Scale(52);
                var contentHeight = Math.Max(maxScroll, Host.Height);
                var thumbHeight = Host.Height * (Host.Height / (float) contentHeight);
                var scrollJump = (contentHeight - Host.Height) / (Host.Height - thumbHeight);

                spriteBatch.FillRectangle(
                    new Rectangle(Host.X + Host.Width - Scale(2), Host.Y + (int) (-_scrollOffset / scrollJump),
                        Scale(2),
                        (int) thumbHeight), Color.Gold);
            }

            Rise.Graphic.ResetScissor();
        }

        private bool IsDown = false;
        private Point _lastPoint = Point.Zero;

        public override void Update(GameTime gameTime)
        {
            if (Rise.Pointing.AreaDown(Bound) && !IsDown)
            {
                IsDown = true;

                _lastPoint = Rise.Pointing.GetPosition();
            }

            if (!Rise.Pointing.AreaDown(Bound) && IsDown)
            {
                IsDown = false;
                _lastPoint = Point.Zero;
            }

            if (Rise.Pointing.AreaDown(Bound) && IsDown)
            {
                var newPoint = Rise.Pointing.GetPosition();
                var delta = _lastPoint.Y - newPoint.Y;
                _lastPoint = newPoint;

                _scrollOffset -= delta;
            }

            if (Rise.Pointing.AreaClick(Bound)) IsDown = false;

            var maxScroll = Content.GetStackCount() * Scale(52);
            _scrollOffset = Mathf.Clamp(_scrollOffset, -maxScroll + Math.Min(maxScroll, Host.Height), 0);
        }
    }
}