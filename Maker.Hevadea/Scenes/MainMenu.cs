using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.Enums;
using Maker.Rise.Extension;
using Maker.Rise.Ressource;
using Maker.Rise.UI;
using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.Hevadea.Scenes
{
    public class MainMenu : Scene
    {
        private readonly SpriteBatch sb;

        public MainMenu()
        {
            sb = Engine.Graphic.CreateSpriteBatch();
        }

        public override void Load()
        {
            Engine.Scene.Background = Ressources.paralaxe_forest;
            UiRoot.Padding = new Padding(64, 64, 256, 265);
            
            var menuButtonHost = new Panel
            {
                Dock = Dock.Bottom,
                Layout = LayoutMode.Horizontal,
                Bound = new Rectangle(64, 64, 64, 72),
                
            };

            var playButton = new Button
            {
                Bound = new Rectangle(64, 64, 64, 64),
                Text = "play",
                Icon = EngineRessources.IconPlay,
                EnableBlur = true
                
            };

            var optionButton = new Button
            {
                Bound = new Rectangle(64, 64, 64, 64),
                Text = "option",
                Icon = EngineRessources.IconSettings,
                EnableBlur = true
            };

            var exitButton = new Button
            {
                Bound = new Rectangle(64, 64, 64, 64),
                Text = "exit",
                Icon = EngineRessources.IconClose,
                EnableBlur = true
            };

            playButton.OnMouseClick += PlayButtonOnOnMouseClick;
            exitButton.OnMouseClick += ExitButtonOnOnMouseClick;

            UiRoot.AddChild(menuButtonHost);
            
            menuButtonHost.AddChild(playButton);
            menuButtonHost.AddChild(optionButton);
            menuButtonHost.AddChild(exitButton);

            UiRoot.RefreshLayout();
        }

        private void PlayButtonOnOnMouseClick(object sender, EventArgs eventArgs)
        {
            Engine.Scene.Switch(new PlayMenu());
        }

        private void ExitButtonOnOnMouseClick(object sender, EventArgs eventArgs)
        {
            Engine.Stop();
        }

        public override void Unload()
        {
        }

        public override void Draw(GameTime gameTime)
        {
            var logo = Ressources.img_hevadea_logo;

            Engine.Graphic.Begin(sb);
            sb.FillRectangle(Engine.Graphic.GetResolutionRect(), Color.Black * 0.25f);
            sb.Draw(logo, Engine.Graphic.GetCenter() - logo.GetCenter(), Color.White);
            sb.End();
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}