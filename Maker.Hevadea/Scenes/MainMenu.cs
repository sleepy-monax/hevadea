using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.Enums;
using Maker.Rise.Extension;
using Maker.Rise.UI;
using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.Remoting.Channels;
using Maker.Rise.UI.Containers;
using Maker.Rise.UI.Layout;
using Maker.Rise.UI.Widgets;

namespace Maker.Hevadea.Scenes
{
    public class MainMenu : Scene
    {
        public override void Load()
        {
            Engine.Scene.Background = Ressources.paralaxe_forest;
            
            var buttonPlay   = new Button { EnableBlur = true, Padding = new Padding(2), Icon = EngineRessources.IconPlay,     Text = "Play" };
            buttonPlay.MouseClick += (sender, args) => { Engine.Scene.Switch(new PlayMenu()); };
            
            var buttonOption = new Button { EnableBlur = true, Padding = new Padding(2), Icon = EngineRessources.IconSettings, Text = "Option" };
            buttonOption.MouseClick += (sender, args) => { /*TODO: option scene*/};
            
            var buttonExit   = new Button { EnableBlur = true, Padding = new Padding(2), Icon = EngineRessources.IconClose,     Text = "Exit" };
            buttonExit.MouseClick += (sender, args) => { Engine.Stop();};
            
            var pictureBoxLogo = new PictureBox {Picture = Ressources.img_hevadea_logo, Dock = Dock.Fill};

            Container = new Container<CenterLayout>
            {
                Childs =
                {
                    new Panel(new Container<DockLayout>())
                    {
                        Size = new Point(600, 480),
                        Origine = Anchor.Center,
                        Anchor = Anchor.Center,
                        Padding = new Padding(8),
                        Childs =
                        {
                            pictureBoxLogo,
                            new Panel(new Container<HorizontalTileLayout>())
                            {
                                Dock = Dock.Bottom,
                                Size = new Point(64),
                                Padding = new Padding(0),
                                Childs = {buttonPlay, buttonOption, buttonExit}
                            }
                        }
                    }
                }
            };
        }
        
        public override void Unload()
        {

        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
        }
    }
}