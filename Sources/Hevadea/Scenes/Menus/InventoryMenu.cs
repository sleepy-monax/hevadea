using Hevadea.Game;
using Hevadea.Game.Craftings;
using Hevadea.Game.Entities;
using Hevadea.Game.Entities.Component;
using Hevadea.Game.Entities.Creatures;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;

namespace Hevadea.Scenes.Menus
{
    public class InventoryMenu : Menu
    {
        public InventoryMenu(Entity entity, List<Recipe> recipies, GameManager game) : base(game)
        {
            PauseGame = true;
            
            var player = (PlayerEntity) entity;
            var inventory = new InventoryWidget(entity.Get<Inventory>().Content) {Padding = new Padding(4, 4), Dock = Dock.Fill};
            var crafting  = new CraftingWidget(entity.Get<Inventory>().Content, recipies) {Padding = new Padding(4, 4), Dock = Dock.Fill};

            Content = GuiHelper.CreateSplitContainer(new Rectangle(0, 0, 800, 600),
                "Inventory", inventory, 
                "Crafting", new DockContainer() { Childrens = {                             new Button
                                { Font  = Ressources.FontRomulus, Text = "Back", Padding = new Padding(4) }
                                .RegisterMouseClickEvent((sender) => {Game.CurrentMenu = new HudMenu(Game);}),crafting },
                    Dock = Dock.Fill
                });
            
            inventory.MouseClick += sender =>
            {
                inventory.HighlightedItem = inventory.SelectedItem;
                player.HoldingItem = inventory.SelectedItem;
            };
        }
    }
}