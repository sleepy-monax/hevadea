using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using WorldOfImagination.GameComponent.UI;
using WorldOfImagination.Utils;

namespace WorldOfImagination.GameComponent.Scene
{
    public class MainMenu : Scene
    {
        private readonly SpriteBatch sb;


        public MainMenu(WorldOfImaginationGame game) : base(game)
        {
            sb = new SpriteBatch(game.GraphicsDevice);
        }

        public override void Load()
        {            
            
            Game.IsMouseVisible = true;
            
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
            
            playButton.OnMouseClick += PlayButtonOnOnMouseClick;
            exitButton.OnMouseClick += ExitButtonOnOnMouseClick;

            UiRoot.AddChild(menuButtonHost);
            menuButtonHost.AddChild(playButton);
            menuButtonHost.AddChild(editorButton);
            menuButtonHost.AddChild(optionButton);
            menuButtonHost.AddChild(exitButton);

            menuButtonHost.Bound = new Rectangle(0, 0, 360, 64 * menuButtonHost.Childs.Count);
            var padding = Game.Graphics.GetHeight() / 2 - menuButtonHost.Bound.Height / 2;
            UiRoot.Padding = new Padding(padding, padding, 16, 16);

            Game.UI.RefreshLayout();
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
        {
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap);

            sb.Draw(Game.Ress.img_menu_background, new Rectangle(0, 0, Game.Graphics.GetWidth(), Game.Graphics.GetHeight()), Color.White);
            sb.End();
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }

    }
}
