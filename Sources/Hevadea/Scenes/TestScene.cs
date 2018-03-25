using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.Scening;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes
{
    class TestScene : Scene
    {
        public override void Load()
        {
            Container = new WidgetTabContainer
            {
                Tabs =
                {
                    new Tab{ Icon = new Sprite(Ressources.TileItems, 0)},
                    new Tab{ Icon = new Sprite(Ressources.TileItems, 1)},
                    new Tab{ Icon = new Sprite(Ressources.TileItems, 2)},
                    new Tab{ Icon = new Sprite(Ressources.TileItems, 3)},
                    new Tab{ Icon = new Sprite(Ressources.TileItems, 4)},
                }
            };
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
