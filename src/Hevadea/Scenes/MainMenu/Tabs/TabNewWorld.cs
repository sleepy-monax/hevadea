using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.UI.Widgets.TextBox;
using Hevadea.Loading;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.MainMenu.Tabs
{
    public class TabNewWorld : Tab
    {
        public TabNewWorld()
        {
            Icon = new Sprite(Ressources.TileIcons, new Point(1, 2));

            var worldNameTextBox = new SingleLineTextBoxWidget(24, "new world", Ressources.FontRomulus) { Padding = new Padding(8) };

            var worldSeedtextBox = new SingleLineTextBoxWidget(24, Rise.Rnd.Next().ToString(), Ressources.FontRomulus) { Padding = new Padding(8) };

            var generateButton = new Button { Text = "Generate", Dock = Dock.Bottom }
            .RegisterMouseClickEvent((sender) =>
            {
                var sucess = int.TryParse(worldSeedtextBox.Text.String, out var seed);

                if (!sucess)
                {
                    seed = worldSeedtextBox.Text.String.GetHashCode();
                }

                Rise.Scene.Switch(new LoadingScene(TaskFactorie.ConstructNewWorld(GLOBAL.GetSavePath() + $"{worldNameTextBox.Text.String}/", seed)));
            });

            var worldOptions = new FlowLayout
            {
                Flow = FlowDirection.TopToBottom,
                Dock = Dock.Fill,
                Childrens =
                {
                    new Label { Text = "World name:", Padding = new Padding(8), TextAlignement = DrawText.Alignement.Left},
                    worldNameTextBox,
                    new Label { Text = "Seed:", Padding = new Padding(8), TextAlignement = DrawText.Alignement.Left},
                    worldSeedtextBox,
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
