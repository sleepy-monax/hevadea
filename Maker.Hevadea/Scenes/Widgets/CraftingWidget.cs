using System.Collections.Generic;
using Maker.Hevadea.Game.Craftings;
using Maker.Hevadea.Game.Items;
using Maker.Rise;
using Maker.Rise.Extension;
using Maker.Rise.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Scenes.Widgets
{
    public class CraftingWidget : Widget
    {
        private readonly ItemStorage _inventory;
        private readonly List<Recipe> _recipies;

        public CraftingWidget(ItemStorage i, List<Recipe> recipies)
        {
            _inventory = i;
            _recipies = recipies;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(Bound, Color.White * 0.1f);
            var index = 0;

            foreach (var c in _recipies)
                if (c != null)
                {
                    var canBeCrafted = c.CanBeCrafted(_inventory);

                    var p = new Point(Host.X + 4, Host.Y + index * 52 + 4);

                    var rect = new Rectangle(p.X, p.Y, Host.Width - 8, 48);
                    var spriteRect = new Rectangle(p.X + 8, p.Y + 8, 32, 32);

                    var costIndex = 0;
                    foreach (var i in c.Costs)
                    {
                        var ressourceCout = _inventory.Count(i.Item);
                        for (var v = 0; v < i.Count; v++)
                        {
                            var costRect = new Rectangle(rect.X + 48 + 16 * costIndex, rect.Y + 26, 16, 16);
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

                    if (rect.Contains(Engine.Input.MousePosition) && canBeCrafted && _inventory.GetFreeSpace() >= c.Quantity)
                    {
                        spriteBatch.FillRectangle(rect, Color.White * 0.05f);
                        spriteBatch.DrawRectangle(rect, Color.White * 0.05f);

                        if (Engine.Input.MouseLeftClick) c.Craft(_inventory);
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