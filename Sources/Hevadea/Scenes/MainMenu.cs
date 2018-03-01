using Hevadea.Framework;
using Hevadea.Framework.Scening;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Scenes.Widgets;
using Maker.Rise;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes
{
    public class MainMenu : Scene
    {
        public override void Load()
        {
            Rise.Scene.SetBackground(Ressources.ParalaxeForest);

            var singleplayerButton = new Button { Font = Ressources.FontRomulus, Text = "Play"};
            var optionButton = new Button { Font = Ressources.FontRomulus, Text = "Option"};
            var quitButton = new Button { Font = Ressources.FontRomulus, Text = "Quit"};

            singleplayerButton.MouseClick += sender => { Rise.Scene.Switch(new PlayMenu()); };

            Container = new AnchoredContainer
            {
                Childrens =
                {
                    new FancyPanel
                    {
                        Bound = new Rectangle(0, 0, 720, 416),
                        Origine = Anchor.Center,
                        Anchor = Anchor.Center,
                        Padding = new Padding(15),

                        Content = new DockContainer
                        {
                            Childrens =
                            {
                                new TileContainer
                                {
                                    Dock = Dock.Bottom,
                                    Flow = FlowDirection.LeftToRight,
                                    Padding = new Padding(4),
                                    Marging = new Padding(2),
                                    Bound = new Rectangle(0, 0, 64, 64),
                                    Childrens = {singleplayerButton, optionButton, quitButton}
                                },                                
                                new PictureWidget
                                {
                                    Picture = Ressources.ImgHevadeaLogo,
                                    Anchor = Anchor.Center,
                                    Origine = Anchor.Center,
                                    Dock = Dock.Fill
                                }
                            }
                        } 
                            
                    }
                }
            };
        }

        public override void Unload()
        {
        }

        public override void OnUpdate(GameTime gameTime)
        {
        }

        public override void OnDraw(GameTime gameTime)
        {
        }
    }
}