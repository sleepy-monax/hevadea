using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Framework.UI.Widgets
{
    public abstract class ListItem
    {
        public ListWidget Parent;
        public abstract void Draw(SpriteBatch spriteBatch, Rectangle host, GameTime gameTime);
    }

    public class ListWidget : Widget
     {
        public int ItemHeight { get; set; } = 32;

        private List<ListItem> _items = new List<ListItem>();
        private ListItem _selectedItem = null;
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

            Rise.Graphic.SetScissor(Bound);

            foreach (var i in _items)
                if (i != null)
                {
                    var p = new Point(Host.X + Scale(4), Host.Y + index * Scale(52) + _scrollOffset);

                    var rect = new Rectangle(p.X, p.Y, Host.Width - Scale(8), Scale(48));
                    var sprite_rect = new Rectangle(p.X + Scale(8), p.Y + Scale(8), Scale(32), Scale(32));

                    if (Rise.Pointing.AreaOver(rect))
                    {
                        spriteBatch.FillRectangle(rect, Color.White * 0.05f);
                        spriteBatch.DrawRectangle(rect, Color.White * 0.05f);
                    }

                    if (i == _selectedItem)
                    {
                        spriteBatch.FillRectangle(rect, Color.Gold * 0.5f);
                        spriteBatch.DrawRectangle(rect, Color.Gold * 0.5f);
                    }

                    i.Draw(spriteBatch, rect, gameTime);
                    index++;
                }


            if (_items.Count == 0)
                spriteBatch.DrawString(Rise.Ui.DefaultFont, "Empty", Bound, Text.Alignement.Center, Text.TextStyle.DropShadow, Color.White);


            if (Rise.Pointing.AreaOver(Bound))
            {
                var maxScroll = _items.Count * Scale(52);
                var contentHeight = Math.Max(maxScroll, Host.Height);
                var thumbHeight = Host.Height * (Host.Height / (float)contentHeight);
                var scrollJump = (contentHeight - Host.Height) / (Host.Height - thumbHeight);

                spriteBatch.FillRectangle(
                    new Rectangle(Host.X + Host.Width - Scale(2), Host.Y + (int)(-_scrollOffset / scrollJump), Scale(2),
                        (int)thumbHeight), Color.Gold);
            }

            Rise.Graphic.ResetScissor();
        }

        bool IsDown = false;
        private Point _lastPoint = Point.Zero;

        public override void Update(GameTime gameTime)
        {
            if (Rise.Input.MouseScrollUp) _scrollOffset += Scale(16);
            if (Rise.Input.MouseScrollDown) _scrollOffset -= Scale(16);

            if (Rise.Pointing.AreaDown(Bound) && !IsDown)
            {
                IsDown = true;

                _lastPoint = Rise.Pointing.GetAreaOver(Bound)[0];
            }

            if (!Rise.Pointing.AreaDown(Bound) && IsDown)
            {
                IsDown = false;
                _lastPoint = Point.Zero;
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
                IsDown = false;
            }

            var maxScroll = _items.Count * Scale(52);
            _scrollOffset = Mathf.Clamp(_scrollOffset, -maxScroll + Math.Min(maxScroll, Host.Height), 0);
        }
    }
}
