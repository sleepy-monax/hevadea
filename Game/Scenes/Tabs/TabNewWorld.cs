using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.UI;
using Hevadea.Registry;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.Tabs
{
    public class TabNewWorld : Tab
    {
        public TabNewWorld()
        {
            Icon = new Sprite(Resources.TileIcons, new Point(1, 2));

            var worldNameTextBox = new WidgetTextBox()
            {
                Padding = new Spacing(8),
                Text = "new world"
            };

            var worldSeedtextBox = new WidgetTextBox()
            {
                Padding = new Spacing(8),
                Text = Rise.Rnd.Next().ToString()
            };

            var worldTypeList = new WidgetList() {UnitBound = new Rectangle(0, 0, 256, 128)};

            foreach (var item in GENERATOR.GENERATORS) worldTypeList.AddItem(new ListItemText(item.Key));

            worldTypeList.SelectFirst();

            var generateButton = new WidgetButton {Text = "Generate", Dock = Dock.Bottom}
                .RegisterMouseClickEvent((s) => Game.New(worldNameTextBox.Text, worldSeedtextBox.Text,
                    GENERATOR.GENERATORS[((ListItemText) worldTypeList.SelectedItem).Text]));

            var worldOptions = new LayoutFlow
            {
                Flow = LayoutFlowDirection.TopToBottom,
                Dock = Dock.Fill,
                Children =
                {
                    new WidgetLabel
                        {Text = "World name:", Padding = new Spacing(8), TextAlignement = TextAlignement.Left},
                    worldNameTextBox,
                    new WidgetLabel {Text = "Seed:", Padding = new Spacing(8), TextAlignement = TextAlignement.Left},
                    worldSeedtextBox,
                    new WidgetLabel
                        {Text = "World type:", Padding = new Spacing(8), TextAlignement = TextAlignement.Left},
                    worldTypeList
                }
            };

            Content = new LayoutDock()
            {
                Padding = new Spacing(16),
                Children =
                {
                    new WidgetLabel {Text = "New World", Font = Resources.FontAlagard, Dock = Dock.Top},
                    generateButton,
                    worldOptions,
                }
            };
        }
    }
}