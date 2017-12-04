using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WorldOfImagination.GameComponent.UI;
using WorldOfImagination.Utils;
using Button = WorldOfImagination.GameComponent.UI.Button;
using Padding = WorldOfImagination.GameComponent.UI.Padding;
using Panel = WorldOfImagination.GameComponent.UI.Panel;

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
            UiRoot.Padding = new Padding(192);
            Game.IsMouseVisible = true;
            
            var menuButtonHost = new Panel(Game.UI)
            {
                Bound = new Rectangle(0, 0, 64, 64),
                Dock  = Dock.Bottom,
                Layout = LayoutMode.Horizontal
            };
            
            var playButton = new Button(Game.UI)
            {
                Bound = new Rectangle(64, 64, 64, 64),
                Text = "Play",
                Icon = Game.Ress.icon_play
            };
            
            var multiPlayerButton = new Button(Game.UI)
            {
                Bound = new Rectangle(64, 64, 64, 64),
                Text = "Multi Player",
                Icon = Game.Ress.icon_people
            };
            
            var exitButton = new Button(Game.UI)
            {
                Bound = new Rectangle(64, 64, 64, 64),
                Text = "Exit", 
                Icon = Game.Ress.icon_close
            };
            
            
            playButton.OnMouseClick += PlayButtonOnOnMouseClick;
            exitButton.OnMouseClick += ExitButtonOnOnMouseClick;

            UiRoot.AddChild(menuButtonHost);
            menuButtonHost.AddChild(playButton);
            menuButtonHost.AddChild(multiPlayerButton);
            menuButtonHost.AddChild(exitButton);

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

            var titleRect = new Rectangle(0, (int)(Game.Graphics.GetHeight() * (3/8f)), Game.Graphics.GetWidth(), Game.Graphics.GetHeight() / 4);

            sb.FillRectangle(titleRect, new Color(0, 0, 0, 100));
            sb.DrawString(Game.Ress.alagard, "World Of Imagination", titleRect, Alignement.Center, Style.DropShadow, Color.White);
            sb.DrawString(Game.Ress.romulus, "\n\n\nTale of the foreigner", titleRect, Alignement.Center, Style.Regular, Color.Gold);
            sb.End();
        }

        public override void Update(GameTime gameTime)
        {
            
        }

    }
}
