using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Hevadea.Framework.UI.Widgets
{
    public abstract class ListItem
    {
        public ListWidget Parent;

        public abstract void Draw(SpriteBatch spriteBatch, Rectangle host, GameTime gameTime);
    }

    public class ListItemText : ListItem
    {
        public string Text { get; set; }

        public ListItemText(string text)
        {
            Text = text;
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle host, GameTime gameTime)
        {
            spriteBatch.DrawString(Rise.Ui.DefaultFont, Text, host, DrawText.Alignement.Center, DrawText.TextStyle.DropShadow, Color.White, Rise.Ui.ScaleFactor);
        }
    }

    public class ListWidget : Widget
    {
        public int ItemHeight { get; set; } = 48;
        public int ItemMarging { get; set; } = 8;
        public Color BackColor { get; set; } = Color.White * 0.1f;
        public ListItem SelectedItem { get; private set; } = null;

        private List<ListItem> _items = new List<ListItem>();
        private ListItem _overItem = null;
        private int _scrollOffset;

        public void AddItem(ListItem item)
        {
            item.Parent = this;
            _items.Add(item);
        }

        public void RemoveItem(ListItem item)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var index = 0;

            spriteBatch.FillRectangle(Bound, BackColor);

            Rise.Graphic.SetScissor(Bound);

            _overItem = null;

            foreach (var i in _items)
                if (i != null)
                {
                    var p = new Point(Host.X + Scale(4), Host.Y + index * Scale(ItemHeight + ItemMarging) + _scrollOffset);

                    var rect = new Rectangle(p.X, p.Y, Host.Width - Scale(8), Scale(ItemHeight));

                    if (Rise.Pointing.AreaOver(rect) && Rise.Pointing.AreaOver(Host))
                    {
                        spriteBatch.FillRectangle(rect, ColorPalette.Border * 0.05f);
                        spriteBatch.DrawRectangle(rect, ColorPalette.Border, Scale(4));

                        _overItem = i;
                    }

                    if (i == SelectedItem)
                    {
                        spriteBatch.FillRectangle(rect, ColorPalette.Accent * 0.5f);
                        spriteBatch.DrawRectangle(rect, ColorPalette.Accent, Scale(4));
                    }

                    i.Draw(spriteBatch, rect, gameTime);
                    index++;
                }

            if (_items.Count == 0)
                spriteBatch.DrawString(Rise.Ui.DefaultFont, "Empty", Bound, DrawText.Alignement.Center, DrawText.TextStyle.DropShadow, Color.White);

            if (Rise.Pointing.AreaOver(Bound))
            {
                var maxScroll = _items.Count * Scale(ItemHeight + ItemMarging);
                var contentHeight = Math.Max(maxScroll, Host.Height);
                var thumbHeight = Host.Height * (Host.Height / (float)contentHeight);
                var scrollJump = (contentHeight - Host.Height) / (Host.Height - thumbHeight);

                spriteBatch.FillRectangle(
                    new Rectangle(Host.X + Host.Width - Scale(4), Host.Y + (int)(-_scrollOffset / scrollJump), Scale(4),
                        (int)thumbHeight), ColorPalette.Accent);
            }

            Rise.Graphic.ResetScissor();
        }

        private bool IsDown = false;
        private Point _lastPoint = Point.Zero;
        private Point _downPoint = Point.Zero;

        public override void Update(GameTime gameTime)
        {
            //if (Rise.Input.MouseScrollUp) _scrollOffset += Scale(16);
            //if (Rise.Input.MouseScrollDown) _scrollOffset -= Scale(16);

            if (Rise.Pointing.AreaDown(Bound) && !IsDown)
            {
                IsDown = true;

                _lastPoint = Rise.Pointing.GetAreaOver(Bound)[0];
                _downPoint = Rise.Pointing.GetAreaOver(Bound)[0];

                Logger.Log<ListWidget>("Down");
            }

            if (Rise.Pointing.AreaDown(Bound) && IsDown)
            {
                var newPoint = Rise.Pointing.GetAreaOver(Bound)[0];
                var delta = _lastPoint.Y - newPoint.Y;
                _lastPoint = newPoint;

                _scrollOffset -= delta;
            }

            if (Rise.Pointing.AreaClick(Bound))
            {
                var dist = Mathf.Distance(_downPoint.ToVector2(), Rise.Pointing.GetAreaOver(Bound)[0].ToVector2());

                if (dist < 16)
                {
                    SelectedItem = _overItem;
                }

                IsDown = false;
                _lastPoint = Point.Zero;
                _downPoint = Point.Zero;
                Logger.Log<ListWidget>("Up");
            }

            if (!Rise.Pointing.AreaDown(Bound) && IsDown)
            {
                IsDown = false;
                _lastPoint = Point.Zero;
                _downPoint = Point.Zero;
            }

            var maxScroll = _items.Count * Scale(ItemHeight + ItemMarging);
            _scrollOffset = Mathf.Clamp(_scrollOffset, -maxScroll + Math.Min(maxScroll, Host.Height), 0);
        }
    }
}