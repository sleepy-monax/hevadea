using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.Threading;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Loading;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes.MainMenu.Tabs
{
    public class TabMultiplayerConnect : Tab
    {
        private TextBox connectIpTextBox;
        private TextBox connectPortTextBox;

        public TabMultiplayerConnect()
        {
            Icon = new Sprite(Ressources.TileIcons, new Point(1, 3));
            connectIpTextBox = new TextBox() { Padding = new Margins(8), Text = "localhost" };
            connectPortTextBox = new TextBox() { Padding = new Margins(8), Text = "7777" };
            var connectButton = new Button { Text = "Connect", Dock = Dock.Bottom }
                .RegisterMouseClickEvent(Connect);

            var connectionOptions = new FlowLayout
            {
                Flow = FlowDirection.TopToBottom,
                Dock = Dock.Fill,
                Childrens =
                {
                    new Label { Text = "IP:", Padding = new Margins(8), TextAlignement = DrawText.Alignement.Left},
                    connectIpTextBox,
                    new Label { Text = "Port:", Padding = new Margins(8), TextAlignement = DrawText.Alignement.Left},
                    connectPortTextBox,
                }
            };

            Content = new Container()
            {
                Childrens =
                {
                    new Label { Text = "Connect", Font = Ressources.FontAlagard, Dock = Dock.Top},
                    connectButton,
                    connectionOptions
                }
            };
        }

        private void Connect(Widget widget)
        {
            var task = Jobs.ConnectToServer
                           .SetArguments(new Jobs.ConnectToServerInfo(connectIpTextBox.Text, int.Parse(connectPortTextBox.Text)))
                           .Then((t, a) => { Rise.Scene.Switch(new SceneGameplay((GameState)((Job)t).Result)); });

            Rise.Scene.Switch(new LoadingScene(task));
        }
    }
}