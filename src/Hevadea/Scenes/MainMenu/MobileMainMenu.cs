using Hevadea.Framework;
using Hevadea.Framework.Scening;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.Utils;
using Hevadea.Registry;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.MainMenu
{
    public class MobileMainMenu : Scene
    {
        public override void Load()
        {
            var background = RandomUtils.Choose(Ressources.ParalaxeForest, Ressources.ParalaxeMontain);
            Rise.Scene.SetBackground(background);
            Rise.Sound.Play(Ressources.Theme0);

            Rise.Debug.GENERAL = true;

            var title = new Label
            {
                Text = Game.Title,
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitOffset = new Point(0, -72),
                Font = Ressources.FontAlagard,
                TextSize = 3f,
            };

            var subTitle = new Label
            {
                Text = Game.SubTitle,
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitOffset = new Point(0, -16),
                Font = Ressources.FontRomulus,
                TextColor = ColorPalette.Accent,
                TextSize = 1f,
            };

            var continueButton = new Button("Continue")
            {
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitOffset = new Point(0, 72),
            }.RegisterMouseClickEvent((sender) => Game.Play(Game.GetLastGame()));

            var newGameButton = new Button("New")
            {
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitOffset = new Point(0, 72 * 2),
            }.RegisterMouseClickEvent((sender) => Game.New("world", Rise.Rnd.Next(), GENERATOR.DEFAULT));

            var version = new Label
            {
                Text = $"{Game.Title} {Game.Version}",
                Anchor = Anchor.BottomRight,
                Origine = Anchor.BottomRight,
                UnitOffset = new Point(-16, 0),
                Font = Ressources.FontHack,
                TextAlignement = Framework.Graphic.DrawText.Alignement.Right,
                TextColor = Color.White * 0.5f,
                TextSize = 1f,
            };

            Container = new Container(title, subTitle, newGameButton, version, Game.GetLastGame() != null ? continueButton : null);
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