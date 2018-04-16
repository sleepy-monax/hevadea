using System.Collections.Generic;
using Hevadea.GameObjects.Entities;
using Hevadea.Registry;
using System.Linq;

namespace Hevadea.GameObjects.Items.Tags
{
    public class EntityGroupeDamage
    {
        public EntityGroupe Groupe { get; }
        public float Damages { get; }
        public EntityGroupeDamage(EntityGroupe groupe, float damages)
        {
            Groupe = groupe;
            Damages = damages;
        }        
    }
    
    public class DamageTag : ItemTag
    {
        public float BaseDamage { get; }
        public List<EntityGroupeDamage> PerEntityDamage { get; set; } = new List<EntityGroupeDamage>();
        
        public DamageTag(float dmg = 1f)
        {
            BaseDamage = dmg;
        }

        public float GetDamages(Entity e)
        {
            foreach (var dmg in PerEntityDamage)
            {
                if (e.IsMemberOf(dmg.Groupe))
                {
                    return dmg.Damages;
                }
            }

            return BaseDamage;
        }
    }
}