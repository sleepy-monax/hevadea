﻿using Hevadea.Framework;
using Hevadea.Framework.Scening;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Scenes
{
    public class TitleSplash : Scene
    {
        public override void Load()
        {
            var background = RandomUtils.Choose(Ressources.ParalaxeForest, Ressources.ParalaxeMontain);
            Rise.Scene.SetBackground(background);

            var title = new Label
            {
                Text = Game.Title,
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitOffset = new Point(0),
                Font = Ressources.FontAlagard,
                TextSize = 6f,
            };

            var subTitle = new Label
            {
                Text = Game.SubTitle,
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitOffset = new Point(0, 72),
                Font = Ressources.FontRomulus,
                TextColor = Color.Gold,
                TextSize = 1f,
            };

            var prompt = new Label
            {
                Text = "> Press any key <",
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitOffset = new Point(0, 256),
                Font = Ressources.FontRomulus,
                TextColor = Color.White * 0.75f,
                TextSize = 1f,
            };

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

            Container = new Container(title, subTitle, prompt, version)
            {

            };
        }

        public override void OnDraw(GameTime gameTime)
        {

        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (Rise.Input.AnyKeyDown())
            {
                Game.GoToMainMenu();
            }
        }

        public override void Unload()
        {

        }
    }
}