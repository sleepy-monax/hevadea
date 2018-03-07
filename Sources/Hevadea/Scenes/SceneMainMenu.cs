using Hevadea.Framework;
using Hevadea.Framework.Scening;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Scenes.Widgets;
using Maker.Rise;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes
{
    public class MainMenu : Scene
    {
        public override void Load()
        {
            Rise.Scene.SetBackground(Ressources.ParalaxeForest);

            Container = new AnchoredContainer().AddChild(
                new WidgetFancyPanel
                {
                    Anchor  = Anchor.Center,
                    Origine = Anchor.Center,
                    UnitBound = new Rectangle(0, 0, 320, 416),
                    Padding = new Padding(16),
                    Content = new TileContainer
                    {
                        Flow = FlowDirection.TopToBottom,
                        Childrens =
                        {
                            new Label 
                                { Text = "Hevadea", Font = Ressources.FontAlagardBig},
                            new Button
                                    { Text = "Continue", Padding = new Padding(4) }
                                .RegisterMouseClickEvent(sender => {}),
                            new Button
                                    { Text = "New Game", Padding = new Padding(4) }
                                .RegisterMouseClickEvent((sender) => { Rise.Scene.Switch(new SceneWorldGeneration());}),
                            new Button
                                    { Text = "Load game", Padding = new Padding(4) }
                                .RegisterMouseClickEvent((sender) => {Rise.Scene.Switch(new SceneLoadGame()); }),
                            new Button
                                    { Text = "Option", Padding = new Padding(4) }
                                .RegisterMouseClickEvent((sender) => {}),
                            new Button
                                    { Text = "Exit", Padding = new Padding(4) }
                                .RegisterMouseClickEvent((sender) => {}),
                        }
                        
                    }
                });
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