using Maker.Hevadea.Enum;
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
        public Direction Facing { get; set; } = Direction.Down;
        private bool IsWalking = false;

        public virtual int GetBaseDamages()
        {
            return 1;
        }

        public bool Move(int ax, int ay, Direction facing)
        {
            var a = base.Move(ax, ay);
            Facing = facing;
            IsWalking = a;
            return a;
        }

        public virtual bool Pickup(Item item)
        {
            return false;
        }

        public void Use(Item item)
        {
        }

        public void Attack(Item Weapon)
        {
            var tilePosition = GetTilePosition();
            var dir = Facing.ToPoint();

            tilePosition = new TilePosition(tilePosition.X + dir.X, tilePosition.Y + dir.Y);

            var entities = Level.GetEntitiesOnArea(new Rectangle(X + Height * dir.X, Y + Width * dir.Y, Height, Width));

            if (entities.Count > 0)
            {
                foreach (var e in entities)
                {
                    if (!e.IsInvincible)
                    {
                        Weapon.Attack(this, e);
                        break;
                    }
                }
            }
            else
            {
                Weapon.Attack(this, tilePosition);
            }
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            int animationFrame = (int) (gameTime.TotalGameTime.TotalSeconds * 8 % 4);

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

                Sprite.DrawSubSprite(spriteBatch, new Vector2(X - 4, Y - 7), new Point(animationFrame, (int) Facing),
                    Color.White);
            }
            else
            {
                Sprite.DrawSubSprite(spriteBatch, new Vector2(X - 4, Y - 7), new Point(2, (int) Facing), Color.White);
            }

            IsWalking = false;
        }
    }
}