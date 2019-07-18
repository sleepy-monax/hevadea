using Hevadea.Framework.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Framework.UI
{
    public abstract class ListItem
    {
        public WidgetList Parent;

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
            spriteBatch.DrawString(Rise.Ui.DefaultFont, Text, host, TextAlignement.Center, TextStyle.DropShadow,
                Color.White, Rise.Ui.ScaleFactor);
        }
    }

    public class WidgetList : Widget
    {
        private int _scrollOffset;

        public bool AlowUnselecting { get; set; } = true;
        public int ItemHeight { get; set; } = 48;
        public Color BackColor { get; set; } = Color.White * 0.1f;
        public ListItem SelectedItem { get; private set; } = null;
        public ListItem OverItem { get; private set; } = null;
        public List<ListItem> Items { get; private set; } = new List<ListItem>();

        private Rectangle GetItemBound(int index)
        {
            var position = new Point(Host.X, Host.Y + index * Scale(ItemHeight) + _scrollOffset);
            var size = new Point(Host.Width, Scale(ItemHeight));

            return new Rectangle(position, size);
        }

        public void SelectFirst()
        {
            SelectedItem = Items.First();
        }

        public void AddItem(ListItem item)
        {
            item.Parent = this;
            Items.Add(item);
        }

        public void RemoveItem(ListItem item)
        {
            item.Parent = null;
            Items.Remove(item);
        }

        public void Clear()
        {
            Items.Clear();
        }

        public override void Update(GameTime gameTime)
        {
            OverItem = null;

            for (var i = 0; i < Items.Count; i++)
            {
                var currentItem = Items[i];
                var currentItemBound = GetItemBound(i);

                if (Rise.Pointing.AreaOver(currentItemBound) && AlowUnselecting) OverItem = currentItem;

                if (Rise.Pointing.AreaClick(currentItemBound) && AlowUnselecting) SelectedItem = currentItem;
            }

            if (Rise.Pointing.AreaOver(Bound))
                if (Rise.Pointing.AreaDown(Bound))
                {
                    var delta = Rise.Pointing.GetDeltaPosition().Y;
                    _scrollOffset -= delta;
                }

            var maxScroll = Items.Count * Scale(ItemHeight);
            _scrollOffset = Mathf.Clamp(_scrollOffset, -maxScroll + Math.Min(maxScroll, Host.Height), 0);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(Bound, BackColor);

            if (Items.Count > 0)
            {
                Rise.Graphic.SetScissor(Bound);

                for (var i = 0; i < Items.Count(); i++)
                {
                    var currentItem = Items[i];
                    var currentItemBound = GetItemBound(i);

                    if (currentItem == SelectedItem)
                        spriteBatch.FillRectangle(currentItemBound, ColorPalette.Accent * 0.5f);
                    else if (currentItem == OverItem)
                        spriteBatch.FillRectangle(currentItemBound, ColorPalette.BorderIdle * 0.5f);

                    currentItem.Draw(spriteBatch, currentItemBound, gameTime);
                }

                Rise.Graphic.ResetScissor();
            }
            else
            {
                spriteBatch.DrawString(
                    Rise.Ui.DefaultFont,
                    "Empty",
                    Bound,
                    TextAlignement.Center,
                    TextStyle.DropShadow,
                    Color.White);
            }

            if (Rise.Pointing.AreaOver(Bound))
            {
                var maxScroll = Items.Count * Scale(ItemHeight);
                var contentHeight = Math.Max(maxScroll, Host.Height);
                var thumbHeight = Host.Height * (Host.Height / (float) contentHeight);
                var scrollJump = (contentHeight - Host.Height) / (Host.Height - thumbHeight);

                spriteBatch.FillRectangle(
                    new Rectangle(
                        Host.X + Host.Width - Scale(4),
                        Host.Y + (int) (-_scrollOffset / scrollJump),
                        Scale(4),
                        (int) thumbHeight),
                    ColorPalette.Accent);
            }
        }
    }
}