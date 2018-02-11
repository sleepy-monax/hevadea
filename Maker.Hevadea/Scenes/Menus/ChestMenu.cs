using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Scenes.Widgets;
using Maker.Rise.UI.Widgets;
using Maker.Rise.UI.Widgets.Containers;

namespace Maker.Hevadea.Scenes.Menus
{
    public class ChestMenu : Menu
    {
        public ChestMenu(Entity entity, Entity chest, GameManager game) : base(game)
        {
            PauseGame = true;
            
            var inventoryA = new InventoryWidget(entity.Components.Get<Inventory>().Content)
                {Padding = new Padding(4, 4), Dock = Dock.Fill};
            var inventoryB = new InventoryWidget(chest.Components.Get<Inventory>().Content)
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
                                inventoryA
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
                                new Label {Text = "Chest", Font = Ressources.FontAlagard, Dock = Dock.Top},
                                inventoryB
                            }
                        }
                    }
                }
            };
        }
    }
}