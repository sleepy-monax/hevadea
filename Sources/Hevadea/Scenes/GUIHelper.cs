using Hevadea.Scenes.Widgets;
using Maker.Rise.UI.Widgets;
using Maker.Rise.UI.Widgets.Containers;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes
{
    public static class GUIHelper
    {

        public static Widget CreateSplitContainer(Rectangle bound, string panelATitle, Widget panelAContent, string panelBTitle, Widget panelBContent)
        {
            return new TileContainer
            {
                Bound = bound,
                Marging = new Padding(16),
                Childrens =
                {
                    new FancyPanel
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
                    new FancyPanel
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
