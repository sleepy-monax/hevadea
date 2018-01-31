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
        Sprite sprite;
        CompositConectedTileRender render;
        SpriteBatch sb;

        public override void Load()
        {
            sprite = new Sprite(Ressources.tile_tiles, 7);
            render = new CompositConectedTileRender(sprite);
            sb = Engine.Graphic.CreateSpriteBatch();
        }

        public override void Unload()
        {

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            Engine.Graphic.Begin(sb, false);


            for (int i = 0; i < 256; i++)
            {
                var conection = new TileConection(i);
                var index = conection.ToByte();
                int x = (index % 8);
                int y = (index / 8);


                render.Draw(sb, new Vector2(x * 16, y*16), conection);
            }


            sb.End();
        }
    }
}
