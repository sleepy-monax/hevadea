using Hevadea.Game;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using System.IO;
using Hevadea.Framework;
using Hevadea.Framework.Scening;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;

namespace Hevadea.Scenes
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
                Offset = new Point(0, -16)
            };
            backButton.MouseClick += sender => Rise.Scene.Switch(new MainMenu());

            var newButton = new Button
            {
                Text = "New",
                Bound = new Rectangle(0, 0, 200, 48),
                Origine = Anchor.BottomRight,
                Anchor = Anchor.BottomRight,
                Offset = new Point(-16, -16)
            };
            newButton.MouseClick += sender => Rise.Scene.Switch(new WorldGenScene());
            
            var savesList = new FancyPanel
            {
                Anchor  = Anchor.Center,
                Origine = Anchor.Center,
                Bound   = new Rectangle(0, 0, 720, 416),
                Padding = new Padding(16),
                Content = new DockContainer()
            };

            Container = new AnchoredContainer
            {
                Padding = new Padding(16),
                Childrens = {savesList, backButton, newButton}
            };

            var s = Directory.GetDirectories(Rise.Platform.GetStorageFolder() + "/Saves/");
            var list = (DockContainer) savesList.Content;
            foreach (var save in s)
            {
                list.AddChild(new Button {Text = save, Dock = Dock.Top}
                .RegisterMouseClickEvent(sender =>
                    {
                        var button = (Button) (sender);
                        Rise.Scene.Switch(new GameScene(GameManager.Load(button.Text)));
                    }));
            }
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