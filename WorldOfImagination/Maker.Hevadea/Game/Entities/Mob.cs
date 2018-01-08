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
            
        }
    }
}