using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
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
                    var item = (ListItemText)saveList.SelectedItem;
                    Game.Play(item.Text);
                }
            });

            Content = new Container(title, loadButton, saveList);

            var s = Directory.GetDirectories(GamePaths.SavesFolder);

            foreach (var save in s)
            {
                saveList.AddItem(new ListItemText(save));
            }
        }
    }
}