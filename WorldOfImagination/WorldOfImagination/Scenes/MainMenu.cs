using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.Ressource;
using Maker.Rise.UI;
using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace WorldOfImagination.Scenes
{
    public class MainMenu : Scene
    {
        private readonly SpriteBatch sb;
        private ParalaxeBackground paralaxe;


        public MainMenu()
        {
            sb = new SpriteBatch(Engine.Graphic.GraphicsDevice);
        }

        public override void Load()
        {            
            paralaxe = new ParalaxeBackground(
                new ParalaxeLayer(EngineRessources.img_forest_background, 1.1f),
                new ParalaxeLayer(EngineRessources.img_forest_trees0, 1.5f),
                new ParalaxeLayer(EngineRessources.img_forest_light, 2f),
                new ParalaxeLayer(EngineRessources.img_forest_trees1, 2.5f)
            );
            
            var menuButtonHost = new Panel
            {
                Dock  = Dock.Left,
                Layout = LayoutMode.Vertical
            };
            
            var playButton = new MainMenuButton
            {
                Bound = new Rectangle(64, 64, 64, 72),
                Text = "play",
                Icon = EngineRessources.icon_play
            };


            var editorButton = new MainMenuButton
            {
                Bound = new Rectangle(64, 64, 64, 72),
                Text = "editor",
                Icon = EngineRessources.icon_edit
            };
            
            var optionButton = new MainMenuButton
            {
                Bound = new Rectangle(64, 64, 64, 72),
                Text = "option", 
                Icon = EngineRessources.icon_settings
            };
            
            var exitButton = new MainMenuButton
            {
                Bound = new Rectangle(64, 64, 64, 72),
                Text = "exit", 
                Icon = EngineRessources.icon_close
            };

            var titleLabel = new Label
            {
                Bound = new Rectangle(64, 64, 64, 64),
                Text = "Tales of Hevadea",
                Font = EngineRessources.font_alagard_big,
                Dock = Dock.Fill
            };

            playButton.OnMouseClick += PlayButtonOnOnMouseClick;
            exitButton.OnMouseClick += ExitButtonOnOnMouseClick;

            UiRoot.AddChild(menuButtonHost);
            UiRoot.AddChild(titleLabel);
            menuButtonHost.AddChild(playButton);
            menuButtonHost.AddChild(editorButton);
            menuButtonHost.AddChild(optionButton);
            menuButtonHost.AddChild(exitButton);

            menuButtonHost.Bound = new Rectangle(0, 0, 360, 64 * menuButtonHost.Childs.Count);
            var padding = Engine.Graphic.GetHeight() / 2 - menuButtonHost.Bound.Height / 2;
            UiRoot.Padding = new Padding(padding, padding, 16, 16);
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
        {;

            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, Engine.CommonRasterizerState);
            paralaxe.Draw(sb, gameTime);
            sb.FillRectangle(new Rectangle(0, 0, 96, Engine.Graphic.GetHeight()), Color.Black * 0.5f);
            sb.End();
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }

    }
}
