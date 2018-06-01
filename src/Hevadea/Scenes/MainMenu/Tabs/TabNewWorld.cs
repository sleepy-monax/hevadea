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

            var generateButton = new Button { Text = "Generate", Dock = Dock.Bottom }
            .RegisterMouseClickEvent((sender) =>
            {
                if (!int.TryParse(worldSeedtextBox.Text.String, out var seed))
                {
                    seed = worldSeedtextBox.Text.String.GetHashCode();
                }

				var job = Jobs.GenerateWorld;
				job.SetArguments(new Jobs.WorldGeneratorInfo(Game.GetSaveFolder() + $"{worldNameTextBox.Text.String}/", seed, GENERATOR.DEFAULT));
                
                job.Finish += (s, e) =>
                {
                    Game game = (Game)((Job)s).Result;
                    game.Initialize();
                    Rise.Scene.Switch(new SceneGameplay(game));
                };
                Rise.Scene.Switch(new LoadingScene(job));
            });

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