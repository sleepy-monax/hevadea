using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            UiRoot.Padding = new Padding(16, 16, 16, 16);
            Game.IsMouseVisible = true;
            
            var menuButtonHost = new Panel(Game.UI)
            {
                //Dock  = Dock.Left,
                Layout = LayoutMode.Vertical
            };
            
            var playButton = new Button(Game.UI)
            {
                Bound = new Rectangle(64, 64, 64, 64),
                Text = "singleplayer",
                Icon = Game.Ress.icon_single_people
            };
            
            var multiPlayerButton = new Button(Game.UI)
            {
                Bound = new Rectangle(64, 64, 64, 64),
                Text = "multiplayer",
                Icon = Game.Ress.icon_people
            };
            
            var editorButton = new Button(Game.UI)
            {
                Bound = new Rectangle(64, 64, 64, 64),
                Text = "world editor", 
                //Icon = Game.Ress.icon_close
            };
            
            var optionButton = new Button(Game.UI)
            {
                Bound = new Rectangle(64, 64, 64, 64),
                Text = "option", 
                Icon = Game.Ress.icon_settings
            };
            
            var exitButton = new Button(Game.UI)
            {
                Bound = new Rectangle(64, 64, 64, 64),
                Text = "exit", 
                Icon = Game.Ress.icon_close
            };
            
            playButton.OnMouseClick += PlayButtonOnOnMouseClick;
            exitButton.OnMouseClick += ExitButtonOnOnMouseClick;

            UiRoot.AddChild(menuButtonHost);
            menuButtonHost.AddChild(playButton);
            menuButtonHost.AddChild(multiPlayerButton);
            menuButtonHost.AddChild(editorButton);
            menuButtonHost.AddChild(optionButton);
            menuButtonHost.AddChild(exitButton);

            menuButtonHost.Bound = new Rectangle(Game.Graphics.GetWidth() / 2 - 180, Game.Graphics.GetHeight() / 2 - (64 * menuButtonHost.Childs.Count) / 2, 360, 64 * menuButtonHost.Childs.Count);
            
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

            var titleRect = new Rectangle(0, 0, Game.Graphics.GetWidth(), Game.Graphics.GetHeight() / 4);

            //sb.FillRectangle(titleRect, new Color(0, 0, 0, 200));
            sb.DrawString(Game.Ress.font_alagard, "World Of Imagination", titleRect, Alignement.Center, Style.DropShadow, Color.White);
            sb.DrawString(Game.Ress.font_romulus, "\n\n\nTale of the foreigner", titleRect, Alignement.Center, Style.Regular, Color.Gold);
            sb.End();
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }

    }
}
