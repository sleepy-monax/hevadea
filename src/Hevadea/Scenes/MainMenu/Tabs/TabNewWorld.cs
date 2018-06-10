using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.Threading;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.UI.Widgets.TextBox;
using Hevadea.Loading;
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

            var worldNameTextBox = new SingleLineTextBoxWidget(24, "new world", Ressources.FontRomulus) { Padding = new Margins(8) };

            var worldSeedtextBox = new SingleLineTextBoxWidget(24, Rise.Rnd.Next().ToString(), Ressources.FontRomulus) { Padding = new Margins(8) };

            var worldTypeList = new ListWidget() { UnitBound = new Rectangle(0, 0, 256, 128), AlowUnselecting = false};

            foreach (var item in GENERATOR.GENERATORS)
            {
                worldTypeList.AddItem(new ListItemText(item.Key));
            }

            worldTypeList.SelectFirst();

            var generateButton = new Button { Text = "Generate", Dock = Dock.Bottom }
            .RegisterMouseClickEvent((s) => Game.New(worldNameTextBox.Text.String, worldSeedtextBox.Text.String, GENERATOR.GENERATORS[((ListItemText)worldTypeList.SelectedItem).Text]));

            var worldOptions = new FlowLayout
            {
                Flow = FlowDirection.TopToBottom,
                Dock = Dock.Fill,
                Childrens =
                {
                    new Label { Text = "World name:", Padding = new Margins(8), TextAlignement = DrawText.Alignement.Left},
                    worldNameTextBox,
                    new Label { Text = "Seed:", Padding = new Margins(8), TextAlignement = DrawText.Alignement.Left},
                    worldSeedtextBox,
                    new Label { Text = "World type:", Padding = new Margins(8), TextAlignement = DrawText.Alignement.Left},
                    worldTypeList
                }
            };

            Content = new Container()
            {
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