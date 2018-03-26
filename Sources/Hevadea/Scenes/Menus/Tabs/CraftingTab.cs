using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Game.Craftings;
using Hevadea.Game.Registry;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Scenes.Menus.Tabs
{
    public class CraftingListItem : ListItem
    {
        private Recipe _recipe;
        
        public CraftingListItem(Recipe recipe)
        {
            _recipe = recipe;
        }

        public Recipe GetRecipe()
        {
            return _recipe;
        }
        
        public override void Draw(SpriteBatch spriteBatch, Rectangle host, GameTime gameTime)
        {
            _recipe.Result.GetSprite().Draw(spriteBatch, new Rectangle(host.Location, new Point(host.Height)), Color.White);
            spriteBatch.DrawString(Ressources.FontRomulus, _recipe.Result.GetName(), new Padding(0, 0, host.Height, 0).Apply(host), DrawText.Alignement.Left, DrawText.TextStyle.DropShadow, Color.White, Rise.Ui.ScaleFactor);
        }
    }
    
    public class CraftingTab : Tab
    {
        public CraftingTab()
        {
            Icon = new Sprite(Ressources.TileIcons, new Point(4, 2));
            
            var craftList = new ListWidget
            {
                Dock = Dock.Fill,
                ItemHeight = 64,
            };

            foreach (var recipe in RECIPIES.HandCrafted)
            {
                craftList.AddItem(new CraftingListItem(recipe));
            }
            
            Content = new DockContainer()
            {
                Childrens =
                {
                    new Label {Text = "Crafting", Font = Ressources.FontAlagard, Dock = Dock.Top},
                    craftList,
                }
            };
        }
    }
}
