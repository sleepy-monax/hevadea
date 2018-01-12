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
        Inventory Inventory;

        public int SelectedSlot = -1;
        public int slotOffset = 0;

        public InventoryUi(Inventory i)
        {
            Inventory = i;
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var index = 0;
            var maxY = Bound.Height / 64;
            var maxX = Bound.Width / 64;

            SelectedSlot = -1;


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

                        index++;
                    }
                }
            }

                ////var rect = new Rectangle(Bound.X, Bound.Y + 48 * index, Bound.Width, 48);
                //if (rect.Contains(Engine.Input.MousePosition))
                //{
                //    spriteBatch.FillRectangle(new Rectangle(Bound.X + 2, Bound.Y + 2 + 48 * index, Bound.Width, 48), Color.Black * 0.25f);
                //    show = true;
                //    spriteBatch.FillRectangle(rect, new Color(0x34, 0x33, 0x60) * (animation.Linear));
                //    SelectedSlot = i;
                //}


                //ITEMS.ById[stack.ItemId].GetSprite().Draw(spriteBatch, new Rectangle(rect.X + 4, rect.Y + 4, 48, 48), Color.Black * 0.25f);
                //ITEMS.ById[stack.ItemId].GetSprite().Draw(spriteBatch, new Rectangle(rect.X, rect.Y, 48, 48), Color.White);
                //





        }

        protected override void OnUpdate(GameTime gameTime)
        {
            if (Engine.Input.MouseScrollDown) slotOffset++;
            if (Engine.Input.MouseScrollUp) slotOffset--;
            if (slotOffset < 0) slotOffset = 0;
            //if (slotOffset >= Inventory.Stacks.Count) slotOffset = Inventory.Stacks.Count - 1;

        }
    }
}