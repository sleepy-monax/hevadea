using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Component.Misc
{
    public class AttackComponent : EntityComponent
    {
        public int BaseDamages { get; set; }

        public AttackComponent(int baseDamages)
        {
            BaseDamages = baseDamages;
        }

        public void Attack(Item weapon)
        {
            var tilePosition = Owner.GetTilePosition();
            var dir = Owner.Facing.ToPoint();

            tilePosition = new TilePosition(tilePosition.X + dir.X, tilePosition.Y + dir.Y);

            var entities = Owner.Level.GetEntitiesOnArea(new Rectangle(Owner.X + Owner.Height * dir.X, 
                                                                       Owner.Y + Owner.Width  * dir.Y,
                                                                       Owner.Height, Owner.Width));

            if (entities.Count > 0)
            {
                foreach (var e in entities)
                {
                    if (e.HasComponent<HealthComponent>() && !e.GetComponent<HealthComponent>().Invicible)
                    {
                        weapon.Attack(Owner, e, BaseDamages);
                        break;
                    }
                }
            }
            else
            {
                weapon.Attack(Owner, tilePosition, BaseDamages);
            }
        }
    }
}
