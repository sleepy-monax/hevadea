using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Registry;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.MainMenu.Tabs
{
    public class TabNewWorld : Tab
    {
        public TabNewWorld()
        {
            Icon = new Sprite(Ressources.TileIcons, new Point(1, 2));

            var worldNameTextBox = new TextBox()
            {
                Padding = new Margins(8),
                Text = "new world"
            };

            var worldSeedtextBox = new TextBox()
            {
                Padding = new Margins(8),
                Text = Rise.Rnd.Next().ToString()
            };

            var worldTypeList = new ListWidget() { UnitBound = new Rectangle(0, 0, 256, 128) };

            foreach (var item in GENERATOR.GENERATORS)
            {
                worldTypeList.AddItem(new ListItemText(item.Key));
            }

            worldTypeList.SelectFirst();

            var generateButton = new Button { Text = "Generate", Dock = Dock.Bottom }
            .RegisterMouseClickEvent((s) => Game.New(worldNameTextBox.Text, worldSeedtextBox.Text, GENERATOR.GENERATORS[((ListItemText)worldTypeList.SelectedItem).Text]));

            var worldOptions = new FlowLayout
            {
                Flow = FlowDirection.TopToBottom,
                Dock = Dock.Fill,
                Childrens =
                {
                    new Label { Text = "World name:", Padding = new Margins(8), TextAlignement = TextAlignement.Left},
                    worldNameTextBox,
                    new Label { Text = "Seed:", Padding = new Margins(8), TextAlignement = TextAlignement.Left},
                    worldSeedtextBox,
                    new Label { Text = "World type:", Padding = new Margins(8), TextAlignement = TextAlignement.Left},
                    worldTypeList
                }
            };

            Content = new Container()
            {
                Padding = new Margins(16),
                Childrens =
                {
                    new Label { Text = "New World", Font = Ressources.FontAlagard, Dock = Dock.Top},
                    generateButton,
                    worldOptions,
                }
            };
        }
    }
}