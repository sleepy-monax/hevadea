using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Loading;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using System.IO;

namespace Hevadea.Scenes.MainMenu.Tabs
{
    public class TabLoadWorld : Tab
    {
        public TabLoadWorld()
        {
            Icon = new Sprite(Ressources.TileIcons, new Point(2, 2));
            var saveList = new ListWidget()
            {
                Dock = Dock.Fill
            };

            var multiplayer = new CheckBox() { Text = "Multiplayer", Dock = Dock.Left, UnitBound = new Rectangle(0, 0, 256, 64) };
            var loadButton = new Button { Text = "Load", Dock = Dock.Fill }
            .RegisterMouseClickEvent((sender) =>
            {
                if (saveList.SelectedItem != null)
                {
                    var item = (ListItemText)saveList.SelectedItem;
                    var loadWorldTask = TaskFactorie.LoadWorld(item.Text);
                    loadWorldTask.LoadingFinished += (task, e) => Rise.Scene.Switch(new SceneGameplay((Game)((LoadingTask)task).Result));
                    Rise.Scene.Switch(new LoadingScene(loadWorldTask));
                }
            });

            Content = new Container()
            {
                Childrens =
                {
                    new Label { Text = "Load World", Font = Ressources.FontAlagard, Dock = Dock.Top },
                    new Container( multiplayer, loadButton ){ Dock = Dock.Bottom },
                    saveList,
                }
            };

            var s = Directory.GetDirectories(Game.GetSaveFolder());

            foreach (var save in s)
            {
                saveList.AddItem(new ListItemText(save));
            }
        }
    }
}