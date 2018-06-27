using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using System.IO;

namespace Hevadea.Scenes.MainMenu.Tabs
{
    public class ListItemWorld : ListItemText
    {
        public string WorldPath { get; }

        public ListItemWorld(string worldName, string worldPath) : base(worldName)
        {
            WorldPath = worldPath;
        }
    }

    public class TabLoadWorld : Tab
    {
        public TabLoadWorld()
        {
            Icon = new Sprite(Ressources.TileIcons, new Point(2, 2));

            var title = new Label
            {
                Text = "Load World",
                Font = Ressources.FontAlagard,
                Dock = Dock.Top
            };

            var saveList = new ListWidget()
            {
                Dock = Dock.Fill
            };

            var loadButton = new Button
            {
                Text = "Load",
                Dock = Dock.Bottom
            }
            .RegisterMouseClickEvent((sender) =>
            {
                if (saveList.SelectedItem != null)
                {
                    var item = (ListItemWorld)saveList.SelectedItem;
                    Game.Play(item.WorldPath);
                }
            });

            Content = new Container(title, loadButton, saveList);

            var s = Directory.GetDirectories(GamePaths.SavesFolder);

            foreach (var save in s)
            {
                saveList.AddItem(new ListItemWorld(Path.GetFileName(save), save));
            }
        }
    }
}