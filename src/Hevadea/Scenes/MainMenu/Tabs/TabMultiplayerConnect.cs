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
    public class TabMultiplayerConnect : Tab
    {
        private SingleLineTextBoxWidget connectIpTextBox;
        private SingleLineTextBoxWidget connectPortTextBox;

        public TabMultiplayerConnect()
        {
            Icon = new Sprite(Ressources.TileIcons, new Point(1, 3));
            connectIpTextBox = new SingleLineTextBoxWidget(24, "localhost", Ressources.FontRomulus) { Padding = new Margins(8) };
            connectPortTextBox = new SingleLineTextBoxWidget(24, "7777", Ressources.FontRomulus) { Padding = new Margins(8) };
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
            var connectTask = Jobs.ConnectToServer(connectIpTextBox.Text.String, int.Parse(connectPortTextBox.Text.String));
            connectTask.LoadingFinished += (task, e) => Rise.Scene.Switch(new SceneGameplay((Game)((LoadingTask)task).Result));
            Rise.Scene.Switch(new LoadingScene(connectTask));
        }
    }
}