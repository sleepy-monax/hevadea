using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Hevadea.Craftings;
using Hevadea.GameObjects.Items;

namespace Hevadea.Scenes.Widgets
{
    public class WidgetCrafting : Widget
    {
        private readonly ItemStorage _inventory;
        private readonly List<Recipe> _recipies;

        private Recipe _selectedRecipe;
        
        public WidgetCrafting(ItemStorage i, List<Recipe> recipies)
        {
            _inventory = i;
            _recipies = recipies;
            MouseClick += sender => _selectedRecipe?.Craft(_inventory);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(Bound, Color.White * 0.1f);
            var index = 0;

            foreach (var c in _recipies)
                if (c != null)
                {
                    var canBeCrafted = c.CanBeCrafted(_inventory);

                    var p = new Point(Host.X + Scale(4), Host.Y + index * Scale( 56));

                    var rect = new Rectangle(p.X, p.Y, Host.Width - Scale(8), Scale(48));
                    var spriteRect = new Rectangle(p.X + Scale(8), p.Y + Scale(8), Scale(32), Scale(32));

                    var costIndex = 0;
                    foreach (var i in c.Costs)
                    {
                        var ressourceCout = _inventory.Count(i.Item);
                        for (var v = 0; v < i.Count; v++)
                        {
                            var costRect = new Rectangle(rect.X + Scale(48 + 16 * costIndex), rect.Y + Scale(26), Scale(16), Scale(16));
                            if (v < ressourceCout)
                            {
                                i.Item.GetSprite().Draw(spriteBatch, costRect, Color.White);
                            }
                            else
                            {
                                i.Item.GetSprite().Draw(spriteBatch, costRect, Color.White * 0.25f);
                            }
                            costIndex++;
                        }
                    }

                    if (Rise.Pointing.AreaOver(rect) && canBeCrafted && _inventory.GetFreeSpace() >= c.Quantity)
                    {
                        spriteBatch.FillRectangle(rect, Color.White * 0.05f);
                        spriteBatch.DrawRectangle(rect, Color.White * 0.05f);
                        _selectedRecipe = c;
                    }

                    if (canBeCrafted)
                    {
                        c.Result.GetSprite().Draw(spriteBatch, spriteRect, Color.White);
                        spriteBatch.DrawString(Ressources.FontRomulus, $"{c.Quantity}x {c.Result.GetName()}",
                            new Vector2(rect.X + 48, rect.Y + 2), Color.White);
                    }
                    else
                    {
                        c.Result.GetSprite().Draw(spriteBatch, spriteRect, Color.White * 0.25f);
                        spriteBatch.DrawString(Ressources.FontRomulus, $"{c.Quantity}x {c.Result.GetName()}",
                            new Vector2(rect.X + 48, rect.Y + 2), Color.White * 0.25f);
                    }

                    index++;
                }
        }
    }
}