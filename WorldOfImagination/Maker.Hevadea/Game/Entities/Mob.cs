using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Game.Tiles;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities
{
    public class Mob : Entity
    {
        public Sprite Sprite;
        public Direction Facing = Direction.Down;
        public bool IsWalking = false;

        public virtual int GetBaseDamages()
        {
            return 1;
        }

        public override bool Move(int accelerationX, int accelerationY)
        {
            IsWalking = base.Move(accelerationX, accelerationY);
            return IsWalking;
        }

        public void Use(Item item, TilePosition tile)
        {

        }

        public void Attack(Item Weapon, TilePosition tile)
        {

        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            int animationFrame = (int)(gameTime.TotalGameTime.TotalSeconds * 8 % 4);

            if (IsWalking)
            {

                switch (animationFrame)
                {
                    case 0:
                        animationFrame = 0;
                        break;
                    case 1:
                        animationFrame = 2;
                        break;
                    case 2:
                        animationFrame = 1;
                        break;
                    case 3:
                        animationFrame = 2;
                        break;
                }

                Sprite.DrawSubSprite(spriteBatch, new Vector2(Position.X - 4, Position.Y - 8), new Point(animationFrame, (int)Facing), Color.White);
            }
            else
            {
                Sprite.DrawSubSprite(spriteBatch, new Vector2(Position.X - 4, Position.Y - 8), new Point(2, (int)Facing), Color.White);
            }
        }
    }
}
