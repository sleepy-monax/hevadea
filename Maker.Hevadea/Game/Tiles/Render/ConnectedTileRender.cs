using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Hevadea.Game.Tiles.Render
{
    public class ConnectedTileRender
    {
        private SpriteSheet Sprites;

        public ConnectedTileRender(SpriteSheet sprites)
        {
            Sprites = sprites;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, TileConection connection)
        {
            var index = connection.ToByte();
            int x = (index % 8);
            int y = (index / 8);

            new Sprite(Sprites, new Point(x, y)).Draw(spriteBatch, position, Color.White);
        }
    }
}
