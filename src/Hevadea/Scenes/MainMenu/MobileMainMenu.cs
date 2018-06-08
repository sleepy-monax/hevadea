using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Scening;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.Utils;
using Hevadea.Registry;
using Microsoft.Xna.Framework;
using System;

namespace Hevadea.Scenes.MainMenu
{
    public class MobileMainMenu : Scene
    {
        public override void Load()
        {
			var background = RandomUtils.Choose(Ressources.ParalaxeForest, Ressources.ParalaxeMontain);
            Rise.Scene.SetBackground(background);

            var title = new Label
            {
                Text = Game.Name,
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitOffset = new Point(0, -72),
                Font = Ressources.FontAlagard,
                TextSize = 3f,
            };

            var subTitle = new Label
            {
                Text = "\"Tales of the unknow\"",
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitOffset = new Point(0, -16),
                Font = Ressources.FontRomulus,
                TextSize = 1f,
            };

            var continueButton = new Button
            {
                Text = "Continue",
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitOffset = new Point(0, 72),
                UnitBound = new Rectangle(0, 0, 256, 64),
            }.RegisterMouseClickEvent((sender) => Game.Play(Game.GetLastGame()));

            var newGameButton = new Button
            {
                Text = "New",
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitOffset = new Point(0, 72 * 2),
                UnitBound = new Rectangle(0, 0, 256, 64),
            }.RegisterMouseClickEvent((sender) => Game.New("world", GENERATOR.DEFAULT));

			Container = new Container(title, subTitle, newGameButton, Game.GetLastGame() != null ? continueButton : null);
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