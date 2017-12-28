using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using WorldOfImagination.Game;

namespace WorldOfImagination.Scenes
{
    public class PlayMenu : Scene
    {
        private readonly SpriteBatch sb;
        public PlayMenu()
        {
            sb = new SpriteBatch(Engine.Graphic.GraphicsDevice);
        }

        public override void Load()
        {


            UiRoot.Padding = new Padding(64, 64, 256, 265);

            var menuButtonHost = new Panel
            {
                Dock = Dock.Bottom,
                Bound = new Rectangle(64, 64, 64, 64),
                Layout = LayoutMode.Horizontal
               
            };
            
            var gameListHost = new Panel
            {
                Dock = Dock.Fill,
                Layout = LayoutMode.Vertical
            };

            var titleLabel = new Label
            {
                Bound = new Rectangle(64, 64, 64, 64),
                Text = "Singleplayer",
                Dock = Dock.Top
            };
            
            var newButton = new Button
            {
                Text = "New",
                Icon = EngineRessources.icon_add
            };
            var backButton = new Button
            {
                Text = "Back",
                Icon = EngineRessources.icon_back
            };
            var deleteButton = new Button
            {
                Text = "Delete",
                Icon = EngineRessources.icon_delete
            };
            
            backButton.OnMouseClick += 
                delegate (object sender, EventArgs args) 
                {
                    Engine.Scene.Switch(new MainMenu());
                };

            newButton.OnMouseClick += 
                delegate (object sender, EventArgs args) 
                {
                    var world = World.Generate(0);
                    Engine.Scene.Switch(new GameScene(world));
                };

            deleteButton.OnMouseClick += 
                delegate(object sender, EventArgs args) 
                {

                };

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
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, Engine.CommonRasterizerState);
            sb.Draw(EngineRessources.img_menu_background, new Rectangle(0, 0, Engine.Graphic.GetWidth(), Engine.Graphic.GetHeight()), Color.White);
            sb.End();
        }

        public override void Unload()
        {

        }
    }
}