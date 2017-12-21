using Maker.Rise;
using Maker.Rise.GameComponent;
using Maker.Rise.GameComponent.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace WorldOfImagination.Scenes
{
    public class PlayMenu : Scene
    {
        private readonly SpriteBatch sb;
        public PlayMenu(WorldOfImaginationGame game) : base(game)
        {
            sb = new SpriteBatch(game.GraphicsDevice);
        }

        public override void Load()
        {


            UiRoot.Padding = new Padding(64, 64, 256, 265);

            var menuButtonHost = new Panel(Game.UI)
            {
                Dock = Dock.Bottom,
                Bound = new Rectangle(64, 64, 64, 64),
                Layout = LayoutMode.Horizontal
               
            };
            
            var gameListHost = new Panel(Game.UI)
            {
                Dock = Dock.Fill,
                Layout = LayoutMode.Vertical
            };

            var titleLabel = new Label(Game.UI)
            {
                Bound = new Rectangle(64, 64, 64, 64),
                Text = "Singleplayer",
                Dock = Dock.Top
            };
            
            var newButton = new Button(Game.UI)
            {
                Text = "New",
                Icon = Game.Ress.icon_add
            };
            var backButton = new Button(Game.UI)
            {
                Text = "Back",
                Icon = Game.Ress.icon_back
            };
            var deleteButton = new Button(Game.UI)
            {
                Text = "Delete",
                Icon = Game.Ress.icon_delete
            };
            
            backButton.OnMouseClick += delegate (object sender, EventArgs args) { Game.Scene.Switch(new MainMenu(Game)); };
            newButton.OnMouseClick += delegate (object sender, EventArgs args) { Game.Scene.Switch(new GameScene(Game)); };
            deleteButton.OnMouseClick += delegate(object sender, EventArgs args) {  };

            UiRoot.AddChild(titleLabel);
            UiRoot.AddChild(menuButtonHost);
            UiRoot.AddChild(gameListHost);
            
            menuButtonHost.AddChild(backButton);
            menuButtonHost.AddChild(newButton);
            menuButtonHost.AddChild(deleteButton);
            
        }


        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, Game.RasterizerState);

            sb.Draw(Game.Ress.img_menu_background, new Rectangle(0, 0, Game.Graphics.GetWidth(), Game.Graphics.GetHeight()), Color.White);
            sb.End();
        }

        public override void Unload()
        {

        }
    }
}