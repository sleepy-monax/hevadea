using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Registry;
using Maker.Hevadea.UI;
using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.Enums;
using Maker.Rise.Extension;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.Hevadea.Scenes
{
    public class PlayMenu : Scene
    {
        private readonly SpriteBatch sb;

        public PlayMenu()
        {
            sb = Engine.Graphic.CreateSpriteBatch();
        }

        public override void Load()
        {
            var width = Math.Min(Engine.Graphic.GetWidth(), 1200);
            var padX = Engine.Graphic.GetWidth() / 2 - width / 2;
            UiRoot.Padding = new Padding(0, 0, padX, padX);

            var container = new PrettyPanel { Dock = Dock.Fill};

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
                delegate 
                {
                    Engine.Scene.Switch(new MainMenu());
                };

            newButton.OnMouseClick +=
                delegate
                {
                    var world = GENERATOR.DEFAULT.Generate();
                    var player = new PlayerEntity();
                    Engine.Scene.Switch(new GameScene(new GameManager(world, player)));
                };

            deleteButton.OnMouseClick +=
                delegate { };

            UiRoot.AddChild(container);
            container.AddChild(titleLabel);
            container.AddChild(menuButtonHost);
            container.AddChild(gameListHost);

            menuButtonHost.AddChild(backButton);
            menuButtonHost.AddChild(newButton);
            menuButtonHost.AddChild(deleteButton);
        }


        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            var logo = Ressources.img_hevadea_logo;

            Engine.Graphic.Begin(sb);
            sb.FillRectangle(Engine.Graphic.GetResolutionRect(), Color.Black * 0.25f);
            sb.End();
        }

        public override void Unload()
        {
        }
    }
}