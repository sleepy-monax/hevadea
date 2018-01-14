using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Game.Tiles;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Entities.Component.Interaction
{
    public class AttackComponent : EntityComponent
    {
        public int BaseDamages { get; set; }
        public bool CanAttackTile { get; set; } = true;
        public bool CanAttackEntities { get; set; } = true;

        public AttackComponent(int baseDamages)
        {
            BaseDamages = baseDamages;
        }

        public void Attack(Item weapon)
        {
            var tilePosition = Owner.GetTilePosition();
            var dir = Owner.Facing.ToPoint();

            tilePosition = new TilePosition(tilePosition.X + dir.X, tilePosition.Y + dir.Y);

            var entities = Owner.Level.GetEntitiesOnArea(new Rectangle((int)(Owner.X + Owner.Height * dir.X), 
                                                                       (int)(Owner.Y + Owner.Width  * dir.Y),
                                                                       Owner.Height, Owner.Width));
            var hasAttacked = false;
            if (CanAttackEntities && entities.Count > 0)
            {
                foreach (var e in entities)
                {
                    if (!e.HasComponent<HealthComponent>() || e.GetComponent<HealthComponent>().Invicible) continue;
                    weapon.Attack(Owner, e, BaseDamages);
                    hasAttacked = true;
                    break;
                }
            }

            if (CanAttackTile && !hasAttacked)
            weapon.Attack(Owner, tilePosition, BaseDamages);
        }
    }
}
