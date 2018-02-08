using Maker.Hevadea.Game.Tiles.Render;
using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Scenes
{
    public class TestScene : Scene
    {
        private CompositConectedTileRender render;
        private SpriteBatch sb;
        private Sprite sprite;

        public override void Load()
        {
            sprite = new Sprite(Ressources.TileTiles, 7);
            render = new CompositConectedTileRender(sprite);
            sb = Engine.Graphic.CreateSpriteBatch();
        }

        public override void Unload()
        {
        }

        public override void OnUpdate(GameTime gameTime)
        {
        }

        public override void OnDraw(GameTime gameTime)
        {
            Engine.Graphic.Begin(sb, false);


            for (var i = 0; i < 256; i++)
            {
                var conection = new TileConection(i);
                var index = conection.ToByte();
                var x = index % 8;
                var y = index / 8;


                render.Draw(sb, new Vector2(x * 16, y * 16), conection);
            }


            sb.End();
        }
    }
}