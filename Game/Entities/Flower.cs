using Hevadea.Entities.Components;
using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities
{
    public class Flower : Entity
    {
        public int Variant { get; set; } = 0;
        private static Sprite[] _sprites;

        public Flower()
        {
            SortingOffset = -16;
            Variant = Rise.Rnd.Next(0, 3);

            if (_sprites == null)
            {
                _sprites = new Sprite[3];
                for (var i = 0; i < 3; i++) _sprites[i] = new Sprite(Resources.TileEntities, new Point(6, i));
            }

            AddComponent(new ComponentBreakable());
            AddComponent(new ComponentFlammable());
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprites[Variant].Draw(spriteBatch, new Vector2(X - 8, Y - 8), Color.White);
        }
    }
}