using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.Utils;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes
{
    public static class GuiFactory
    {



        public static Widget CreateSplitContainer(Rectangle bound, string panelATitle, Widget panelAContent, string panelBTitle, Widget panelBContent)
        {
            return new AnchoredContainer
            {
                UnitBound = bound,
                Childrens =
                {
                    new WidgetFancyPanel
                    {
                        UnitBound = new Rectangle(0, 0, 400, 480),
                        Anchor = Anchor.Center,
                        Origine = Anchor.Left,
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
                        UnitBound = new Rectangle(0, 0, 400, 480),
                        Anchor = Anchor.Center,
                        Origine = Anchor.Right,
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
