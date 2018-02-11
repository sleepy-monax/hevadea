using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Entities.Creatures;
using Maker.Hevadea.Scenes.Widgets;
using Maker.Rise.UI.Widgets;
using Maker.Rise.UI.Widgets.Containers;

namespace Maker.Hevadea.Scenes.Menus
{
    public class InventoryMenu : Menu
    {
        public InventoryMenu(Entity entity, GameManager game) : base(game)
        {
            var player = (PlayerEntity) entity;
            PauseGame = true;
            var inventory = new InventoryWidget(entity.Components.Get<Inventory>().Content)
                {Padding = new Padding(4, 4), Dock = Dock.Fill};

            var crafting = new CraftingWidget(entity.Components.Get<Inventory>().Content)
                {Padding = new Padding(4, 4), Dock = Dock.Fill};


            Content = new TileContainer
            {
                Childrens =
                {
                    new BlurPanel
                    {
                        Padding = new Padding(4),
                        Content = new DockContainer
                        {
                            Childrens =
                            {
                                new Label {Text = "Inventory", Font = Ressources.FontAlagard, Dock = Dock.Top},
                                inventory
                            }
                        }
                    },
                    new BlurPanel
                    {
                        Padding = new Padding(4),
                        Content = new DockContainer
                        {
                            Childrens =
                            {
                                new Label {Text = "Crafting", Font = Ressources.FontAlagard, Dock = Dock.Top},
                                new Button
                                {
                                    Text = "Craft",
                                    Dock = Dock.Bottom,
                                    Padding = new Padding(8),
                                    BlurBackground = false
                                },
                                crafting
                            }
                        }
                    }
                }
            };


            inventory.MouseClick += sender =>
            {
                inventory.HighlightedItem = inventory.SelectedItem;
                player.HoldingItem = inventory.SelectedItem;
            };
        }
    }
}