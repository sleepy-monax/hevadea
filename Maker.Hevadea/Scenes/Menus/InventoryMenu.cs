using System.Collections.Generic;
using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Craftings;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Entities.Creatures;
using Maker.Hevadea.Scenes.Widgets;
using Maker.Rise.UI.Widgets;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Scenes.Menus
{
    public class InventoryMenu : Menu
    {
        public InventoryMenu(Entity entity, List<Recipe> recipies, GameManager game) : base(game)
        {
            var player = (PlayerEntity) entity;
            PauseGame = true;
            var inventory = new InventoryWidget(entity.Components.Get<Inventory>().Content)
                {Padding = new Padding(4, 4), Dock = Dock.Fill};

            var crafting = new CraftingWidget(entity.Components.Get<Inventory>().Content, recipies)
                {Padding = new Padding(4, 4), Dock = Dock.Fill};

            Content = GUIHelper.CreateSplitContainer(new Rectangle(0, 0, 800, 600), "Invertory", inventory, "Crafting", crafting);


            inventory.MouseClick += sender =>
            {
                inventory.HighlightedItem = inventory.SelectedItem;
                player.HoldingItem = inventory.SelectedItem;
            };
        }
    }
}