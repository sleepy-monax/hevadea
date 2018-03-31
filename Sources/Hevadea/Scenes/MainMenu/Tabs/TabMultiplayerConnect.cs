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
            connectIpTextBox = new SingleLineTextBoxWidget(24, "localhost", Ressources.FontRomulus) { Padding = new Padding(8) };
            connectPortTextBox = new SingleLineTextBoxWidget(24, $"{GameManager.PORT}", Ressources.FontRomulus) { Padding = new Padding(8) };
            var connectButton = new Button { Text = "Connect", Dock = Dock.Bottom }
                .RegisterMouseClickEvent(Connect);


            var connectionOptions = new FlowContainer
            {
                Flow = FlowDirection.TopToBottom,
                Dock = Dock.Fill,
                Childrens =
                {
                    new Label { Text = "IP:", Padding = new Padding(8), TextAlignement = DrawText.Alignement.Left},
                    connectIpTextBox,
                        new Label { Text = "Port:", Padding = new Padding(8), TextAlignement = DrawText.Alignement.Left},
                    connectPortTextBox,   
                }
            };

            Content = new DockContainer()
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
            Rise.Scene.Switch(
                new LoadingScene(
                    TaskFactorie.ConstructConnectToServer("player", -1, connectIpTextBox.Text.String, int.Parse(connectPortTextBox.Text.String))));
        }
    }
}
