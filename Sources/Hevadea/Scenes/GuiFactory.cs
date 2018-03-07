using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes
{
    public static class GuiFactory
    {

        public static Widget CreateSplitContainer(Rectangle bound, string panelATitle, Widget panelAContent, string panelBTitle, Widget panelBContent)
        {
            return new TileContainer
            {
                UnitBound = bound,
                Marging = new Padding(16),
                Childrens =
                {
                    new WidgetFancyPanel
                    {
                        Padding = new Padding(15),
                        Content = new DockContainer
                        {
                            Childrens =
                            {
                                new Label {Text = panelATitle, Font = Ressources.FontAlagard, Dock = Dock.Top},
                                panelAContent
                            }
                        }
                    },
                    new WidgetFancyPanel
                    {
                        Padding = new Padding(15),
                        Content = new DockContainer
                        {
                            Childrens =
                            {
                                new Label {Text = panelBTitle, Font = Ressources.FontAlagard, Dock = Dock.Top},
                                panelBContent
                            }
                        }
                    }
                }
            };
        }

    }
}
