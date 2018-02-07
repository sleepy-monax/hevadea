using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Game.Registry;
using Maker.Rise;
using Maker.Rise.Extension;
using Maker.Rise.UI;
using Maker.Rise.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.UI
{
    public class CraftingControl : Widget
    {
        ItemStorage Inventory;
        public CraftingControl(ItemStorage i)
        {
            Inventory = i;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(Bound, Color.White * 0.1f);
            var index = 0;

            foreach (var c in RECIPIES.HandCrafted)
            {
                if (c != null)
                {
                    bool canBeCrafted = c.CanBeCrafted(Inventory);


                    var p = new Point(Host.X + 4, Host.Y + index * 64 + 4);

                    var rect = new Rectangle(p.X, p.Y, Host.Width - 8, 48);
                    var spriteRect = new Rectangle(p.X + 8, p.Y + 8, 32, 32);



                    var costIndex = 0;
                    foreach (var i in c.Costs)
                    {
                        for (int v = 0; v < i.Count; v++)
                        {
                            var costRect = new Rectangle(rect.X + 48 + 16 * costIndex, rect.Y + 26, 16, 16);
                            i.Item.GetSprite().Draw(spriteBatch, costRect, Color.White);
                            costIndex++;
                        }

                    }

                    if (rect.Contains(Engine.Input.MousePosition) && canBeCrafted)
                    {
                            
                        spriteBatch.FillRectangle(rect, Color.White * 0.05f);
                        spriteBatch.DrawRectangle(rect, Color.White * 0.05f);
                        
                        if (Engine.Input.MouseLeftClick)
                        {
                            c.Craft(Inventory);
                        }
                    }

                    if (canBeCrafted)
                    {
                        c.Result.GetSprite().Draw(spriteBatch, spriteRect, Color.White);
                        spriteBatch.DrawString(Ressources.fontRomulus, $"{c.Quantity}x {c.Result.GetName()}", new Vector2(rect.X + 48, rect.Y + 2), Color.White);
                    }
                    else
                    {
                        c.Result.GetSprite().Draw(spriteBatch, spriteRect, Color.White * 0.25f);
                        spriteBatch.DrawString(Ressources.fontRomulus, $"{c.Quantity}x {c.Result.GetName()}", new Vector2(rect.X + 48, rect.Y + 2), Color.White * 0.25f);
                    }

                    index++;
                    
                }
            }
        }
    }
}
