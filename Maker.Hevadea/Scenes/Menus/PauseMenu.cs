using Maker.Hevadea.Game;
using Maker.Hevadea.Scenes.Widgets;
using Maker.Rise.UI.Widgets;
using Maker.Rise.UI.Widgets.Containers;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Scenes.Menus
{
    public class PauseMenu : Menu
    {
        public PauseMenu(GameManager game) : base(game)
        {
            PauseGame = true;

            Content = new AnchoredContainer().AddChild(
                new FancyPanel
                {
                    Bound = new Rectangle(0,0,320,760),
                    Anchor = Anchor.Center,
                    Origine = Anchor.Center,
                    Content = new TileContainer
                    {
                        Flow = FlowDirection.TopToBottom,
                        Childrens =
                        {
                            new Button{ Text = "test" },
                            new Button{ Text = "test" },
                            new Button{ Text = "test" },
                            new Button{ Text = "test" },
                            new Button{ Text = "test" },
                        }
                        
                    }
                });
        }
    }
}
