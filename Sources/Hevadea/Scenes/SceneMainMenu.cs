using Hevadea.Framework;
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
    public class MainMenu : Scene
    {
        public override void Load()
        {
            Rise.Scene.SetBackground(Ressources.ParalaxeForest);

            var menuPanel = new WidgetFancyPanel
            {
                Anchor = Anchor.Center,
                Origine = Anchor.Left,
                UnitBound = new Rectangle(0, 0, 320, 352),
                Padding = new Padding(16),
                Content = new TileContainer
                {
                    Flow = FlowDirection.TopToBottom,
                    Childrens =
                        {
                            
                            new Button
                                    { Text = "Continue", Padding = new Padding(4) }
                                .RegisterMouseClickEvent(sender => { }),
                            new Button
                                    { Text = "New Game", Padding = new Padding(4) }
                                .RegisterMouseClickEvent((sender) => { Rise.Scene.Switch(new SceneNewWorld());}),
                            new Button
                                    { Text = "Load game", Padding = new Padding(4) }
                                .RegisterMouseClickEvent((sender) => {Rise.Scene.Switch(new SceneLoadGame()); }),
                            new Button
                                    { Text = "Multiplayer", Padding = new Padding(4) }
                                .RegisterMouseClickEvent((sender) => {Rise.Scene.Switch(new SceneClientMutiplayer()); }),
                            new Button
                                    { Text = "Option", Padding = new Padding(4) }
                                .RegisterMouseClickEvent((sender) => {}),
                            new Button
                                    { Text = "Exit", Padding = new Padding(4) }
                                .RegisterMouseClickEvent((sender) => {}),
                        }

                }
            };

            var hevadeaLogo = new Label { Text = "Hevadea", UnitBound = new Rectangle(0, 0, 320, 352), Anchor = Anchor.Center, Origine = Anchor.Right, Font = Ressources.FontAlagardBig, Scale = 1f};

            Container = new AnchoredContainer() { Childrens = { menuPanel, hevadeaLogo } };
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