using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.Tiles;
using Maker.Rise.Ressource;

namespace Maker.Hevadea.Game.Items
{
    public class Item
    {
        public Sprite Sprite;

        public virtual float GetAttackBonus(Entity Target)
        {
            return 1f;
        }

        public virtual float GetAttackBonus(Tile Target)
        {
            return 1f;
        }

        public virtual void Attack(Entity user, Entity target, int baseDamages)
        {
            if (target.HasComponent<HealthComponent>())
            {
                target.GetComponent<HealthComponent>().Hurt(user, (int) (baseDamages * GetAttackBonus(target)), user.Facing);
            }
        }

        public virtual void Attack(Entity user, TilePosition target, int baseDamages)
        {
            var tile = user.Level.GetTile(target);
            tile.Hurt(user, (int) (baseDamages * GetAttackBonus(tile)), target, user.Facing);
        }

        public virtual void InteracteOn(Entity user, Entity entity)
        {
            entity.Interacte(user, this, user.Facing);
        }

        public virtual void InteracteOn(Entity user, TilePosition pos)
        {
            var tile = user.Level.GetTile(pos);
            tile.Interacte(user, this, pos, user.Facing);
        }
    }
}