using Hevadea.Game;
using Hevadea.Game.Craftings;
using Hevadea.Game.Entities;
using Hevadea.Game.Entities.Component;
using Hevadea.Game.Entities.Creatures;
using Hevadea.Scenes.Widgets;
using Maker.Rise.UI.Widgets;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Hevadea.Scenes.Menus
{
    public class InventoryMenu : Menu
    {
        public InventoryMenu(Entity entity, List<Recipe> recipies, GameManager game) : base(game)
        {
            PauseGame = true;
            
            var player = (PlayerEntity) entity;
            var inventory = new InventoryWidget(entity.Components.Get<Inventory>().Content) {Padding = new Padding(4, 4), Dock = Dock.Fill};
            var crafting  = new CraftingWidget(entity.Components.Get<Inventory>().Content, recipies) {Padding = new Padding(4, 4), Dock = Dock.Fill};

            Content = GUIHelper.CreateSplitContainer(new Rectangle(0, 0, 800, 600), "Inventory", inventory, "Crafting", crafting);
            
            inventory.MouseClick += sender =>
            {
                inventory.HighlightedItem = inventory.SelectedItem;
                player.HoldingItem = inventory.SelectedItem;
            };
        }
    }
}