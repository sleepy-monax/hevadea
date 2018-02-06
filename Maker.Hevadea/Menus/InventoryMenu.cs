using System.Security.Policy;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.UI;
using Maker.Hevadea.UI;

using Maker.Rise;
using Maker.Rise.Enums;
using Maker.Rise.Extension;
using Maker.Rise.UI;
using Maker.Rise.UI.Widgets;
using Maker.Rise.UI.Widgets.Containers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Menus
{
    public class InventoryMenu : Menu
    {
        public InventoryMenu(Entity entity, GameManager game) : base(game)
        {
            PauseGame = true;
            var inventory = new InventoryUi(entity.Components.Get<Inventory>().Content){ Padding = new Padding(4, 4) };
            var crafting = new CraftingControl(entity.Components.Get<Inventory>().Content){ Padding  = new Padding(4, 4)};
            Content = new AnchoredContainer
            {
                Childrens =
                {
                    new BlurPanel()
                    {
                        Anchor = Anchor.Center,
                        Origine = Anchor.Center,
                        Bound = new Rectangle(0, 0, 800, 600),
                        Content = new TileContainer
                        {
                            Childrens =
                            {
                                new BlurPanel
                                {
                                    Padding = new Padding(4),
                                    Content = inventory
                                },
                                new BlurPanel
                                {
                                    Padding = new Padding(4),
                                    Content = crafting
                                }
                            }
                        }
                    }
                }
            };
        } 
    }
}