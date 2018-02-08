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

            var singleplayerButton = new Button {Padding = new Padding(4, 0), Text = "Singleplayer"};
            var multiplayerButton = new Button {Padding = new Padding(4, 0), Text = "Multiplayer"};
            var optionButton = new Button {Padding = new Padding(4, 0), Text = "Option"};
            var quitButton = new Button {Padding = new Padding(4, 0), Text = "Quit"};

            singleplayerButton.MouseClick += sender => { Engine.Scene.Switch(new PlayMenu()); };
            quitButton.MouseClick += sender => Engine.Stop();

            Container = new AnchoredContainer
            {
                Childrens =
                {
                    new PictureWidget
                    {
                        Picture = Ressources.ImgHevadeaLogo,
                        Anchor = Anchor.Center,
                        Origine = Anchor.Center
                    },
                    new Panel
                    {
                        Bound = new Rectangle(0, 0, 720, 64),
                        Origine = Anchor.Bottom,
                        Anchor = Anchor.Bottom,
                        Offset = new Point(0, -16),
                        Padding = new Padding(4, 8),

                        Content = new TileContainer
                        {
                            Flow = FlowDirection.LeftToRight,
                            Childrens = {singleplayerButton, multiplayerButton, optionButton, quitButton}
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