using Hevadea.Framework.UI;
using Hevadea.Framework.Extension;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes
{
    public static class GuiFactory
    {
        public static Widget CreateSplitContainer(Rectangle bound, string panelATitle, Widget panelAContent, string panelBTitle, Widget panelBContent)
        {
            return new LayoutTile()
            {
                UnitBound = bound,
                Dock = Dock.Fill,
                Childrens =
                {
                    new WidgetPanel()
                    {
                        UnitBound = new Rectangle(0, 0, 400, 480),
                        Anchor = Anchor.Center,
                        Origine = Anchor.Left,
                        Dock = Dock.Left,
                        Content = new LayoutDock()
                        {
                            Childrens =
                            {
                                new WidgetLabel {Text = panelATitle, Font = Ressources.FontAlagard, Dock = Dock.Top},
                                panelAContent
                            }
                        }
                    },
                    new WidgetPanel()
                    {
                        UnitBound = new Rectangle(0, 0, 400, 480),
                        Anchor = Anchor.Center,
                        Origine = Anchor.Right,
                        Dock = Dock.Right,
                        Content = new LayoutDock()
                        {
                            Childrens =
                            {
                                new WidgetLabel {Text = panelBTitle, Font = Ressources.FontAlagard, Dock = Dock.Top},
                                panelBContent
                            }
                        }
                    }
                }
            };
        }
    }
}