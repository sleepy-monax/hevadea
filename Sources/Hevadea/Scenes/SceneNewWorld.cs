using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Scening;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.UI.Widgets.TextBox;
using Hevadea.Framework.Utils;
using Hevadea.Game.Loading;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes
{
    public class SceneNewWorld : Scene
    {
        public override void Load()
        {
            var worldNameTextBox = new SingleLineTextBoxWidget(24, "new world", Ressources.FontRomulus) { Padding = new Padding(8) };

            var worldSeedtextBox = new SingleLineTextBoxWidget(24, Rise.Rnd.Next().ToString(), Ressources.FontRomulus) { Padding = new Padding(8) };

            var generateButton = new Button { Text = "Generate", Padding = new Padding(4) }
            .RegisterMouseClickEvent((sender) =>
            {
                var sucess = int.TryParse(worldSeedtextBox.Text.String, out var seed);

                if (!sucess)
                {
                    seed = worldSeedtextBox.Text.String.GetHashCode();
                }

                Rise.Scene.Switch(new LoadingScene(TaskFactorie.ConstructNewWorld(ConstVal.GetSavePath() + $"{worldNameTextBox.Text.String}/", seed)));
            });

            var backButton = new Button { Text = "Back", Padding = new Padding(4) }
            .RegisterMouseClickEvent((sender) => { Rise.Scene.Switch(new MainMenu()); });

            var panel = new WidgetFancyPanel
            {
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitBound = new Rectangle(0, 0, 640, 416),
                Padding = new Padding(16),
                Content = new TileContainer
                {
                    Flow = FlowDirection.TopToBottom,
                    Childrens =
                    {
                        new Label { Text = "New World", Font = Ressources.FontAlagard},
                        new Label { Text = "World name:", Padding = new Padding(8), TextAlignement = DrawText.Alignement.Left},
                        worldNameTextBox,
                        new Label { Text = "Seed:", Padding = new Padding(8), TextAlignement = DrawText.Alignement.Left},
                        worldSeedtextBox,
                        new TileContainer { Flow = FlowDirection.LeftToRight, Childrens = { backButton, generateButton} }   
                    }
                }
            };


            Container = new AnchoredContainer {Childrens = {panel}};
        }

        public override void OnDraw(GameTime gameTime)
        {

        }

        public override void OnUpdate(GameTime gameTime)
        {

        }

        public override void Unload()
        {

        }
    }
}
