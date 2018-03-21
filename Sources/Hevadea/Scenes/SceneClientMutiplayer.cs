using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Scening;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.UI.Widgets.TextBox;
using Hevadea.Game;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hevadea.Framework.Utils;
using Hevadea.Game.Loading;

namespace Hevadea.Scenes
{
    public class SceneClientMutiplayer : Scene
    {
        private SingleLineTextBoxWidget connectIpTextBox;
        private SingleLineTextBoxWidget connectPortTextBox;
        
        public override void Load()
        {
            connectIpTextBox = new SingleLineTextBoxWidget(24, "localhost", Ressources.FontRomulus) { Padding = new Padding(8) };
            connectPortTextBox = new SingleLineTextBoxWidget(24, $"{GameManager.PORT}", Ressources.FontRomulus) { Padding = new Padding(8) };
            var backButton = new Button { Text = "Back", Padding = new Padding(4) }
                .RegisterMouseClickEvent(sender => Rise.Scene.Switch(new MainMenu()));
            var connectButton = new Button { Text = "Connect", Padding = new Padding(4) }
                .RegisterMouseClickEvent(Connect);

            var connectPanel = new WidgetFancyPanel
            {
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitBound = new Rectangle(0, 0, 640, 352),
                Padding = new Padding(16),
                Content = new TileContainer
                {
                    Flow = FlowDirection.TopToBottom,
                    Childrens =
                    {
                        new Label { Text = "Connect", Font = Ressources.FontAlagard},
                        new Label { Text = "IP:", Padding = new Padding(8), TextAlignement = DrawText.Alignement.Left},
                        connectIpTextBox,
                            new Label { Text = "Port:", Padding = new Padding(8), TextAlignement = DrawText.Alignement.Left},
                        connectPortTextBox,
                        new TileContainer { Flow = FlowDirection.LeftToRight, Childrens = { backButton, connectButton}, Dock = Dock.Bottom },
                    }

                }
            };

            var hevadeaLogo = new Label { Text = "Hevadea", Font = Ressources.FontAlagardBig};

            Container = new AnchoredContainer() { Childrens = { connectPanel } };
        }

        private void Connect(Widget widget)
        {
            Rise.Scene.Switch(
                new LoadingScene(
                    TaskFactorie.ConstructConnectToServer("player", -1, connectIpTextBox.Text.String, int.Parse(connectPortTextBox.Text.String))));
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
