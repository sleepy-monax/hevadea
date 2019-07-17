using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
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

            var title = new WidgetLabel
            {
                Text = "Load World",
                Font = Ressources.FontAlagard,
                Dock = Dock.Top
            };

            var saveList = new WidgetList()
            {
                Dock = Dock.Fill
            };

            var loadButton = new WidgetButton
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

            Content = new LayoutDock();
            Content.AddChilds(title, loadButton, saveList);
            Content.Padding = new Margins(16);

            var s = Directory.GetDirectories(GamePaths.SavesFolder);

            foreach (var save in s)
            {
                saveList.AddItem(new ListItemWorld(Path.GetFileName(save), save));
            }
        }
    }
}