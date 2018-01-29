using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Game.Registry;
using Maker.Rise;
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
            spriteBatch.DrawString(Ressources.fontRomulus, text, new Vector2(Bound.X + Bound.Width - textSize.X - Padding.Left - 4, Bound.Y + Bound.Height - textSize.Y - Padding.Down), Color.Gold);


            foreach (var i in ITEMS.ById)
            {
                if (i != null)
                {
                    var itemCount = Inventory.Count(i);

                    if (itemCount > 0)
                    {
                        var p = new Point(Host.X + 4, Host.Y + index * 64 + 4);

                        var rect = new Rectangle(p.X, p.Y, Host.Width - 8, 48);
                        var sprite_rect = new Rectangle(p.X + 8, p.Y + 8,32,32);

                        spriteBatch.FillRectangle(rect, Color.White * 0.05f);
                        spriteBatch.DrawRectangle(rect, Color.White * 0.05f);

                        i.GetSprite().Draw(spriteBatch, sprite_rect, Color.White);
                        spriteBatch.DrawString(Ressources.fontRomulus, $"{i.GetName()} {itemCount,3}x", new Vector2(rect.X + 48, rect.Y + 12), Color.White);

                        if (rect.Contains(Engine.Input.MousePosition))
                        {
                            SelectedItem = i;
                        }
                        
                        index++;
                    }
                }
            }
        }

        protected override void OnUpdate(GameTime gameTime)
        {

        }
    }
}