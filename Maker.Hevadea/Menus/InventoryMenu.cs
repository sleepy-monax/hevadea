﻿using System.Security.Policy;
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
            var player = (PlayerEntity) entity;
            PauseGame = true;
            var inventory = new InventoryUi(entity.Components.Get<Inventory>().Content)
                { Padding = new Padding(4, 4), Dock = Dock.Fill };
            
            var crafting = new CraftingControl(entity.Components.Get<Inventory>().Content)
                { Padding  = new Padding(4, 4), Dock = Dock.Fill };
            
            Content = new AnchoredContainer
            {
                Childrens =
                {
                    new Panel()
                    {
                        Anchor = Anchor.Center,
                        Origine = Anchor.Center,
                        Bound = new Rectangle(0, 0, 800, 480),
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
                                            new Label{ Text = "Inventory", Font = Ressources.fontAlagard, Dock = Dock.Top},
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
                                            new Label  { Text = "Crafting", Font = Ressources.fontAlagard, Dock = Dock.Top },
                                            new Button { Text = "Craft", Dock = Dock.Bottom , Padding = new Padding(8), BlurBackground = false},
                                            crafting,
                                        }
                                    }
                                }
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