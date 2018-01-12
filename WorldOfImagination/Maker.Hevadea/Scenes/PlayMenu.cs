using Maker.Hevadea.Game;
using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.Enum;
using Maker.Rise.Extension;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Scenes
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
                Bound = new Rectangle(64, 64, 64, 72),
                Layout = LayoutMode.Horizontal
            };

            var gameListHost = new Panel
            {
                Dock = Dock.Fill,
                Layout = LayoutMode.Horizontal
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
                Icon = EngineRessources.IconAdd
            };
            var backButton = new Button
            {
                Text = "Back",
                Icon = EngineRessources.IconBack
            };
            var deleteButton = new Button
            {
                Text = "Delete",
                Icon = EngineRessources.IconDelete
            };

            backButton.OnMouseClick +=
                delegate { Engine.Scene.Switch(new MainMenu()); };

            newButton.OnMouseClick +=
                delegate
                {
                    var world = World.Generate(0);
                    Engine.Scene.Switch(new GameScene(world));
                };

            deleteButton.OnMouseClick +=
                delegate { };

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
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null,
                Engine.CommonRasterizerState);
            sb.Draw(Ressources.img_forest_background,
                new Rectangle(0, 0, Engine.Graphic.GetWidth(), Engine.Graphic.GetHeight()), Color.White);
            sb.End();
        }

        public override void Unload()
        {
        }
    }
}