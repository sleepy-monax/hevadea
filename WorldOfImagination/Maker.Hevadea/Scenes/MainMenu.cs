using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.Enum;
using Maker.Rise.Ressource;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.Hevadea.Scenes
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
                new ParalaxeLayer(Ressources.img_forest_background, 1.1f),
                new ParalaxeLayer(Ressources.img_forest_trees0, 1.5f),
                new ParalaxeLayer(Ressources.img_forest_light, 2f),
                new ParalaxeLayer(Ressources.img_forest_trees1, 2.5f)
            );

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
                Icon = EngineRessources.IconPlay
            };

            var optionButton = new Button
            {
                Bound = new Rectangle(64, 64, 64, 64),
                Text = "option",
                Icon = EngineRessources.IconSettings
            };

            var exitButton = new Button
            {
                Bound = new Rectangle(64, 64, 64, 64),
                Text = "exit",
                Icon = EngineRessources.IconClose
            };

            var titleLabel = new Label
            {
                Bound = new Rectangle(64, 64, 64, 64),
                Text = "Tale of Hevadea",
                Font = Ressources.font_alagard_big,
                Dock = Dock.Fill
            };

            playButton.OnMouseClick += PlayButtonOnOnMouseClick;
            exitButton.OnMouseClick += ExitButtonOnOnMouseClick;

            UiRoot.AddChild(menuButtonHost);
            UiRoot.AddChild(titleLabel);
            
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
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null,
                Engine.CommonRasterizerState);
            paralaxe.Draw(sb, gameTime);
            sb.End();
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}