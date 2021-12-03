using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.UI;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hevadea.Scenes.Menus
{
    public class MenuChest : Menu
    {
        private readonly WidgetItemContainer _inventoryA;
        private readonly WidgetItemContainer _inventoryB;

        public MenuChest(Entity entity, Entity chest, GameState gameState) : base(gameState)
        {
            PauseGame = true;
            EscapeToClose = true;

            _inventoryA = new WidgetItemContainer(entity.GetComponent<ComponentInventory>().Content)
                {Padding = new Spacing(4, 4), Dock = Dock.Fill};
            _inventoryB = new WidgetItemContainer(chest.GetComponent<ComponentInventory>().Content)
                {Padding = new Spacing(4, 4), Dock = Dock.Fill};

            _inventoryA.MouseClick += Tranfer;
            _inventoryB.MouseClick += Tranfer;

            var closeBtn = new WidgetSprite()
            {
                Sprite = new Sprite(Resources.TileGui, new Point(7, 7)),
                UnitBound = new Rectangle(0, 0, 48, 48),
                Anchor = Anchor.TopLeft,
                Origine = Anchor.Center
            };

            closeBtn.MouseClick += CloseBtnOnMouseClick;

            Content = new WidgetFancyPanel()
            {
                Content = new LayoutDock()
                {
                    Children =
                    {
                        new LayoutDock()
                        {
                            Dock = Dock.Fill,
                            Children = {closeBtn}
                        },
                        GuiFactory.CreateSplitContainer(new Rectangle(0, 0, 64, 64), "Inventory", _inventoryA, "Chest",
                            _inventoryB),
                    }
                }
            };
        }

        private void Tranfer(Widget sender)
        {
            var invA = (WidgetItemContainer) sender;
            var invB = (WidgetItemContainer) sender == _inventoryA ? _inventoryB : _inventoryA;

            var item = invA.SelectedItem;

            if (Rise.Input.KeyDown(Keys.LeftShift))
            {
                while (invA.Content.Count(item) > 0 && invB.Content.HasFreeSlot())
                {
                    invA.Content.Remove(item, 1);
                    invB.Content.Add(item, 1);
                }
            }
            else
            {
                if (item != null && invB.Content.HasFreeSlot())
                {
                    invA.Content.Remove(item, 1);
                    invB.Content.Add(item, 1);
                }
            }
        }

        private void CloseBtnOnMouseClick(Widget sender)
        {
            GameState.CurrentMenu = new MenuInGame(GameState);
        }
    }
}