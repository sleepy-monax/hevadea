using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities
{
    public class GrassEntity : Entity
    {
        Sprite Sprite;
        public GrassEntity()
        {
            Height = 16;
            Width = 16;

            Sprite = new Sprite(Ressources.tile_entities, new Point(1,2));
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Sprite.Draw(spriteBatch, new Vector2(X, Y), Color.White);
        }
    }
}
