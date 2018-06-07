using Hevadea.Framework.Scening;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Framework.Sample.Scenes
{
    public class UISample : Scene
    {
        public override void Load()
        {
            var buttons = new List<Widget>();

            for (int i = 0; i < 8; i++)
            {
                buttons.Add(new Button($"Button{i}"));
            }

            Container = new TileLayout() { Flow = FlowDirection.TopToBottom}.AddChilds(buttons.ToArray());
        }

        public override void Unload()
        {

        }

        public override void OnUpdate(GameTime gameTime)
        {

        }

        public override void OnDraw(GameTime gameTime)
        {

        }
    }
}
