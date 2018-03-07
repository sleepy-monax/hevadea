using Hevadea.Game;
using Hevadea.Game.Craftings;
using Hevadea.Game.Entities;
using Hevadea.Game.Entities.Components;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;

namespace Hevadea.Scenes.Menus
{
    public class MenuPlayerInventory : Menu
    {
        public MenuPlayerInventory(Entity entity, List<Recipe> recipies, GameManager game) : base(game)
        {
            PauseGame = true;
            
            var player = (EntityPlayer) entity;
            var inventory = new WidgetItemContainer(entity.Get<Inventory>().Content) {Padding = new Padding(4, 4), Dock = Dock.Fill};
            var crafting  = new WidgetCrafting(entity.Get<Inventory>().Content, recipies) {Padding = new Padding(4, 4), Dock = Dock.Fill};

            Content = GuiFactory.CreateSplitContainer(new Rectangle(0, 0, 800, 600),
                "Inventory", inventory, 
                "Crafting", new DockContainer() { Childrens = {                             new Button
                                { Font  = Ressources.FontRomulus, Text = "Back", Padding = new Padding(4) }
                                .RegisterMouseClickEvent((sender) => {Game.CurrentMenu = new MenuInGame(Game);}),crafting },
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