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
            var test = new DialogBox(Game.UI)
            {
                Bound = new Rectangle(0, 0, 96, 96),
                Dock = Dock.Bottom
            };
            
            backButton.OnMouseClick += BackButtonOnOnMouseClick;
            newButton.OnMouseClick += NewButtonOnOnMouseClick;
            deleteButton.OnMouseClick += delegate(object sender, EventArgs args) { test.Show(); };

            UiRoot.AddChild(menuButtonHost);
            UiRoot.AddChild(test);
            UiRoot.AddChild(gameListHost);
            
            menuButtonHost.AddChild(backButton);
            menuButtonHost.AddChild(newButton);
            menuButtonHost.AddChild(deleteButton);
            
        }

        private void NewButtonOnOnMouseClick(object sender, EventArgs eventArgs)
        {
            Game.Scene.Switch(new GameScene(Game));
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