using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
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

            var worldNameTextBox = new WidgetTextBox()
            {
                Padding = new Margins(8),
                Text = "new world"
            };

            var worldSeedtextBox = new WidgetTextBox()
            {
                Padding = new Margins(8),
                Text = Rise.Rnd.Next().ToString()
            };

            var worldTypeList = new WidgetList() { UnitBound = new Rectangle(0, 0, 256, 128) };

            foreach (var item in GENERATOR.GENERATORS)
            {
                worldTypeList.AddItem(new ListItemText(item.Key));
            }

            worldTypeList.SelectFirst();

            var generateButton = new WidgetButton { Text = "Generate", Dock = Dock.Bottom }
            .RegisterMouseClickEvent((s) => Game.New(worldNameTextBox.Text, worldSeedtextBox.Text, GENERATOR.GENERATORS[((ListItemText)worldTypeList.SelectedItem).Text]));

            var worldOptions = new LayoutFlow
            {
                Flow = LayoutFlowDirection.TopToBottom,
                Dock = Dock.Fill,
                Childrens =
                {
                    new WidgetLabel { Text = "World name:", Padding = new Margins(8), TextAlignement = TextAlignement.Left},
                    worldNameTextBox,
                    new WidgetLabel { Text = "Seed:", Padding = new Margins(8), TextAlignement = TextAlignement.Left},
                    worldSeedtextBox,
                    new WidgetLabel { Text = "World type:", Padding = new Margins(8), TextAlignement = TextAlignement.Left},
                    worldTypeList
                }
            };

            Content = new LayoutDock()
            {
                Padding = new Margins(16),
                Childrens =
                {
                    new WidgetLabel { Text = "New World", Font = Ressources.FontAlagard, Dock = Dock.Top},
                    generateButton,
                    worldOptions,
                }
            };
        }
    }
}