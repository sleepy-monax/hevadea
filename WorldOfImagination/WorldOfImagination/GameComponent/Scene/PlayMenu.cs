using System;
using Microsoft.Xna.Framework;
using WorldOfImagination.GameComponent.UI;

namespace WorldOfImagination.GameComponent.Scene
{
    public class PlayMenu : Scene
    {
        public PlayMenu(WorldOfImaginationGame game) : base(game)
        {
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
            
            backButton.OnMouseClick += BackButtonOnOnMouseClick;

            UiRoot.AddChild(menuButtonHost);
            UiRoot.AddChild(gameListHost);
            
            menuButtonHost.AddChild(backButton);
            menuButtonHost.AddChild(newButton);
            menuButtonHost.AddChild(deleteButton);
            
        }

        private void BackButtonOnOnMouseClick(object sender, EventArgs eventArgs)
        {
            Game.Scene.Switch(new MainMenu(Game));
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime)
        {

        }

        public override void Unload()
        {

        }
    }
}