using Maker.Hevadea.Scenes.Widgets;
using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.UI.Widgets;
using Maker.Rise.UI.Widgets.Containers;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Scenes
{
    public class MainMenu : Scene
    {
        public override void Load()
        {
            Engine.Scene.Background = Ressources.ParalaxeForest;

            var singleplayerButton = new Button {BlurBackground = false, Font = Ressources.FontRomulus, Text = "Play"};
            var optionButton = new Button {BlurBackground = false, Font = Ressources.FontRomulus, Text = "Option"};
            var quitButton = new Button {BlurBackground = false, Font = Ressources.FontRomulus, Text = "Quit"};

            singleplayerButton.MouseClick += sender => { Engine.Scene.Switch(new PlayMenu()); };
            quitButton.MouseClick += sender => Engine.Stop();

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