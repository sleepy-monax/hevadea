using Maker.Hevadea.Scenes.Widgets;
using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.UI.Widgets;
using Maker.Rise.UI.Widgets.Containers;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Scenes
{
    public class PlayMenu : Scene
    {
        public override void Load()
        {         
            var backButton = new BackButton
            {
                Bound = new Rectangle(0, 0, 48, 48),
                Origine = Anchor.BottomLeft,
                Anchor = Anchor.BottomLeft,
                BlurBackground = false,
                Offset = new Point(0, -16)
            };
            backButton.MouseClick += sender => Engine.Scene.Switch(new MainMenu());

            var newButton = new Button
            {
                Text = "New",
                Bound = new Rectangle(0, 0, 200, 48),
                Origine = Anchor.BottomRight,
                Anchor = Anchor.BottomRight,
                BlurBackground = false,
                Offset = new Point(-16, -16)
            };
            newButton.MouseClick += sender => Engine.Scene.Switch(new WorldGenScene());
            
            var savesList = new FancyPanel
            {
                Anchor  = Anchor.Center,
                Origine = Anchor.Center,
                Bound   = new Rectangle(0, 0, 720, 416),
                Padding = new Padding(16),
                Content = new AnchoredContainer{Childrens = {backButton, newButton}}
            };

            Container = new AnchoredContainer()
            {
                Padding = new Padding(16),
                Childrens = {savesList}
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