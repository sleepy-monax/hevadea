using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes
{
    public static class GuiFactory
    {
        public static Widget CreateSplitContainer(Rectangle bound, string panelATitle, Widget panelAContent, string panelBTitle, Widget panelBContent)
        {
            return new TileLayout()
            {
                UnitBound = bound,
                Dock = Dock.Fill,
                Childrens =
                {
                    new Panel()
                    {
                        UnitBound = new Rectangle(0, 0, 400, 480),
                        Anchor = Anchor.Center,
                        Origine = Anchor.Left,
                        Dock = Dock.Left,
                        Content = new Container()
                        {
                            Childrens =
                            {
                                new Label {Text = panelATitle, Font = Ressources.FontAlagard, Dock = Dock.Top},
                                panelAContent
                            }
                        }
                    },
                    new Panel()
                    {
                        UnitBound = new Rectangle(0, 0, 400, 480),
                        Anchor = Anchor.Center,
                        Origine = Anchor.Right,
                        Dock = Dock.Right,
                        Content = new Container()
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