using Maker.Rise;
using Maker.Rise.GameComponent;
using Maker.Rise.GameComponent.Ressource;
using Maker.Rise.GameComponent.UI;
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


        public MainMenu(RiseGame game) : base(game)
        {
            sb = new SpriteBatch(game.GraphicsDevice);
        }

        public override void Load()
        {            
            Game.IsMouseVisible = true;
            paralaxe = new ParalaxeBackground(Game,
                new ParalaxeLayer(Game.Ress.img_forest_background, 1.1f),
                new ParalaxeLayer(Game.Ress.img_forest_trees0, 1.5f),
                new ParalaxeLayer(Game.Ress.img_forest_light, 2f),
                new ParalaxeLayer(Game.Ress.img_forest_trees1, 2.5f)
            );
            
            var menuButtonHost = new Panel(Game.UI)
            {
                Dock  = Dock.Left,
                Layout = LayoutMode.Vertical
            };
            
            var playButton = new MainMenuButton(Game.UI)
            {
                Bound = new Rectangle(64, 64, 64, 72),
                Text = "play",
                Icon = Game.Ress.icon_play
            };


            var editorButton = new MainMenuButton(Game.UI)
            {
                Bound = new Rectangle(64, 64, 64, 72),
                Text = "editor",
                Icon = Game.Ress.icon_edit
            };
            
            var optionButton = new MainMenuButton(Game.UI)
            {
                Bound = new Rectangle(64, 64, 64, 72),
                Text = "option", 
                Icon = Game.Ress.icon_settings
            };
            
            var exitButton = new MainMenuButton(Game.UI)
            {
                Bound = new Rectangle(64, 64, 64, 72),
                Text = "exit", 
                Icon = Game.Ress.icon_close
            };

            var titleLabel = new Label(Game.UI)
            {
                Bound = new Rectangle(64, 64, 64, 64),
                Text = "Tales of Hevadea",
                Font = Game.Ress.font_alagard_big,
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
            var padding = Game.Graphics.GetHeight() / 2 - menuButtonHost.Bound.Height / 2;
            UiRoot.Padding = new Padding(padding, padding, 16, 16);
            UiRoot.RefreshLayout();
        }

        private void PlayButtonOnOnMouseClick(object sender, EventArgs eventArgs)
        {
            Game.Scene.Switch(new PlayMenu(Game));
        }

        private void ExitButtonOnOnMouseClick(object sender, EventArgs eventArgs)
        {
            Game.Exit();
        }

        public override void Unload()
        {

        }

        public override void Draw(GameTime gameTime)
        {;

            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, Game.RasterizerState);
            paralaxe.Draw(sb, gameTime);
            sb.FillRectangle(new Rectangle(0, 0, 96, Game.Graphics.GetHeight()), Color.Black * 0.5f);
            sb.End();
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }

    }
}
