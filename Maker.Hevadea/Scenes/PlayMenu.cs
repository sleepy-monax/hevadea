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
using System.ComponentModel;
using Maker.Rise.UI.Widgets;
using Maker.Rise.UI.Widgets.Containers;

namespace Maker.Hevadea.Scenes
{
    public class PlayMenu : Scene
    {
        public override void Load()
        {
            var backButton = new BackButton 
                { Bound = new Rectangle(0, 0, 48, 48), Origine = Anchor.BottomLeft, Anchor = Anchor.BottomLeft, BlurBackground = true, Offset = new Point(0, -16)};
            backButton.MouseClick += sender => Engine.Scene.Switch(new MainMenu());
            
            var newButton = new Button {Text = "New", Bound = new Rectangle(0, 0, 120, 48), Origine = Anchor.BottomRight, Anchor = Anchor.BottomRight, BlurBackground = true, Offset = new Point(-16, -16)};
            newButton.MouseClick += sender =>
            {
                var world = GENERATOR.DEFAULT.Generate();
                var player = new PlayerEntity();
                Engine.Scene.Switch(new GameScene(new GameManager(world, player)));
            }; 
            
            Container = new AnchoredContainer
            {
                Childrens = { backButton, newButton }
            };
        }


        public override void OnUpdate(GameTime gameTime)
        {
        }

        public override void OnDraw(GameTime gameTime)
        {

        }

        public override void Unload()
        {
        }
    }
}