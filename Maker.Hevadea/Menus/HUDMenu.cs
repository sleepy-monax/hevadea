using System.Runtime.InteropServices;
using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Menus;
using Maker.Hevadea.Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maker.Rise.Enums;
using Maker.Hevadea.UI;
using Maker.Rise.UI;
using Maker.Rise.UI.Widgets;
using Maker.Rise.UI.Widgets.Containers;

namespace Maker.Hevadea.Menus
{
    public class HUDMenu : Menu
    {
        private ProgressBar _healthProgressBar;
        private ProgressBar _energyProgressBar;
        
        public HUDMenu(GameManager game) : base(game)
        {
            _healthProgressBar = new ProgressBar { Padding = new Padding(0, 4), Bound = new Rectangle(0, 0, 128, 8), ProgressColor = Color.DarkRed, Offset = new Point(0, 16)};
            _energyProgressBar = new ProgressBar { Padding = new Padding(0, 4), Bound = new Rectangle(0, 0, 128, 8)};
            Content = new AnchoredContainer
            {
                Childrens =
                {
                    new TileContainer
                    {
                        Padding = new Padding(4, 0),
                        Bound = new Rectangle(0, 0, 356, 32),
                        Flow = FlowDirection.TopToBottom,
                        Childrens = {_healthProgressBar, _energyProgressBar }
                    }
                }
            };
        }

        public override void Update(GameTime gameTime)
        {
            var h = Game.Player.Components.Get<Health>();
            var e = Game.Player.Components.Get<Energy>();
            _healthProgressBar.Value = (float)h.GetValue();
            _energyProgressBar.Value = e.Value / e.MaxValue;
            base.Update(gameTime);
        }
    }
}
