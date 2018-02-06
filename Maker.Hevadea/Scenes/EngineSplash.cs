using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Registry;
using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.Extension;
using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Scenes
{
    public class EngineSplash : Scene
    {
        private SpriteBatch sb;
        private Texture2D logo;
        public bool GoToGame = false;

        public override void Load()
        {
            sb = Engine.Graphic.CreateSpriteBatch();
            logo = Ressources.img_engine_logo;
        }

        public override void Unload()
        {
        }

        bool once = true;

        public override void OnUpdate(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalSeconds > 10 && once)
            {
                if (GoToGame)
                {
                    var world = GENERATOR.DEFAULT.Generate();
                    var player = new PlayerEntity();
                    Engine.Scene.Switch(new GameScene(new GameManager(world, player)));
                }
                else
                {
                    Engine.Scene.Switch(new MainMenu());
                    //Engine.Scene.Switch(new TestScene());
                }
                once = false;
            }
        }

        public override void OnDraw(GameTime gameTime)
        {
            Engine.Graphic.Begin(sb);
            sb.FillRectangle(Engine.Graphic.GetResolutionRect(), Color.Black);
            sb.Draw(logo, Engine.Graphic.GetCenter() - logo.GetCenter() , Color.White);
            sb.End();
        }
    }
}
