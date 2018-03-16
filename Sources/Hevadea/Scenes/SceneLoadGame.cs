using Hevadea.Framework;
using Hevadea.Framework.Scening;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Game.Loading;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using System.IO;

namespace Hevadea.Scenes
{
    public class SceneLoadGame : Scene
    {
        public override void Load()
        {

            var saveList = new ListWidget() { Dock = Dock.Fill, Padding = new Padding(4) };


            var backButton = new Button
            {
                Text = "Back", Padding = new Padding(4)
            }.RegisterMouseClickEvent(sender => Rise.Scene.Switch(new MainMenu()));

            var loadButton = new Button
            { Text = "Load", Padding = new Padding(4) }
            .RegisterMouseClickEvent((sender) => 
            {

                if (saveList.SelectedItem != null)
                {
                    var item = (ListItemText)saveList.SelectedItem;
                    Rise.Scene.Switch(new LoadingScene(TaskFactorie.ConstructLoadWorld(item.Text)));
                }
            });



            var hostPanel = new WidgetFancyPanel
            {
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitBound = new Rectangle(0, 0, 640, (int)(Rise.Graphic.GetHeight() * 0.9f / Rise.Ui.ScaleFactor)),
                Padding = new Padding(16),
                Content = new DockContainer()
                {
                    Childrens =
                    {
                        new Label { Text = "Load World", Font = Ressources.FontAlagard, Dock = Dock.Top },
                        new TileContainer { Flow = FlowDirection.LeftToRight, Childrens = { backButton, loadButton}, Dock = Dock.Bottom },
                        saveList,
                    }
                }
            };

            Container = new AnchoredContainer
            {
                Padding = new Padding(16),
                Childrens = {hostPanel, backButton}
            };

            var s = Directory.GetDirectories(Rise.Platform.GetStorageFolder() + "/Saves/");

            foreach (var save in s)
            {
                saveList.AddItem(new ListItemText(save));
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