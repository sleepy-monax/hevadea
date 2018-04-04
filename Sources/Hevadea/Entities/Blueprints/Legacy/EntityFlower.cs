using Hevadea.Entities.Components;
using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Blueprints.Legacy
{
    public class EntityFlower : Entity
    {
        public int Variant { get; set; } = 0;
        private static Sprite[] _sprites; 

        public EntityFlower()
        {
            SortingOffset = -16;
            Variant = Rise.Rnd.Next(0, 3);

            if (_sprites == null)
            {
                _sprites = new Sprite[3];
                for (int i = 0; i < 3; i++)
                {
                    _sprites[i] = new Sprite(Ressources.TileEntities, new Point(6, i));
                }
            }

            AddComponent(new Breakable());
            AddComponent(new Burnable(1f));
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprites[Variant].Draw(spriteBatch, new Vector2(X - 8, Y - 8), Color.White);
        }
    }
}
