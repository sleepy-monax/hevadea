using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.UI;
using Hevadea.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Hevadea.Scenes.Widgets
{
    public class WidgetHotBar : Widget
    {
        public ItemStorage Inventory { get; set; }
        private float _offset = 0f;
        private int _selected = 0;

        public event EventHandler ItemSelected;

        public WidgetHotBar(ItemStorage inventory)
        {
            Inventory = inventory;
        }

        public override void Update(GameTime gameTime)
        {
            _selected = _selected.Clamp(0, Inventory.GetStackCount() - 1);
            _offset -= (_offset - _selected) * 0.1f;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var ItemSize = Host.Height;
            var newSlected = _selected;

            for (var i = 0; i < Inventory.GetStackCount(); i++)
            {
                var item = Inventory.GetStack(i);
                var pos = new Point((int) (Host.X + (i - _offset) * ItemSize) + Host.Width / 2 - ItemSize / 2, Host.Y);
                var Rect = new Rectangle(pos.X, pos.Y, ItemSize, ItemSize);

                if (Rise.Pointing.AreaClick(Rect)) newSlected = i;

                spriteBatch.DrawSprite(item.Sprite, Rect, i == _selected ? Color.White : Color.White * 0.5f);
                spriteBatch.DrawString(Resources.FontRomulus, Inventory.Count(item).ToString(), pos.ToVector2(),
                    Color.White);
            }

            _selected = newSlected;
        }

        public Item GetSelectedItem()
        {
            return Inventory.GetStackCount() > 0 ? Inventory.GetStack(_selected) : null;
        }

        public void Next()
        {
            _selected = (_selected + 1).Clamp(0, Inventory.GetStackCount() - 1);
            ItemSelected?.Invoke(this, EventArgs.Empty);
        }

        public void Previouse()
        {
            _selected = (_selected - 1).Clamp(0, Inventory.GetStackCount() - 1);
            ItemSelected?.Invoke(this, EventArgs.Empty);
        }
    }
}