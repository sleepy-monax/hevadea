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
        public Inventory Inventory;
        public Item SelectedItem { get; set; }

        public InventoryUi(Inventory i)
        {
            Inventory = i;
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var index = 0;
            var maxY = Bound.Height / 64;
            var maxX = Bound.Width / 64;

            SelectedItem = null;


            foreach (var i in ITEMS.ById)
            {
                if (i != null)
                {
                    var itemCount = Inventory.Count(i);

                    if (itemCount > 0)
                    {
                        var sx = index % maxX;
                        var sy = index / maxX;

                        var rect = new Rectangle(Bound.X + sx * 64, Bound.Y + sy * 64, 48, 48);
                        spriteBatch.FillRectangle(rect, Color.Black * 0.25f);
                        i.GetSprite().Draw(spriteBatch ,rect, Color.White);
                        spriteBatch.DrawString(Ressources.font_romulus, $"x{itemCount}", new Vector2(rect.X + 24, rect.Y + 32), Color.White);

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