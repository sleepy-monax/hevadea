using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Tiles;
using Maker.Rise.Ressource;

namespace Maker.Hevadea.Game.Items
{
    public class Item
    {
        public Sprite Sprite;

        public virtual bool CanBeStackWith(Item other)
        {
            return GetType() == other.GetType();
        }

        public virtual float GetAttackBonus(Entity Target)
        {
            return 1f;
        }

        public virtual float GetAttackBonus(Tile Target)
        {
            return 0f;
        }

        public virtual void Attack(Mob user, Entity target)
        {
            target.Hurt(user, (int)(user.GetBaseDamages() * GetAttackBonus(target)), user.Facing);
        }
        
        public virtual void Attack(Mob user, TilePosition target)
        {
            var tile = user.Level.GetTile(target);
            tile.Hurt(user, (int)(user.GetBaseDamages() * GetAttackBonus(tile)), target, user.Facing);
        }
        
        public virtual void InteracteOn(Mob user, Entity entity)
        {
            entity.Interacte(user, this);
        }
        
        public virtual void InteracteOn(Mob user, TilePosition pos)
        {
            var tile = user.Level.GetTile(pos);
            tile.Interacte(user, this, pos);
        }
    }
}
