using Hevadea.Entities;
using Hevadea.Entities.Blueprints;
using Hevadea.Tiles;
using System.Collections.Generic;

namespace Hevadea.Items.Tags
{
    public class GroupeDamage<TGroupe>
    {
        public Groupe<TGroupe> Groupe { get; }
        public float Damages { get; }

        public GroupeDamage(Groupe<TGroupe> groupe, float damages)
        {
            Groupe = groupe;
            Damages = damages;
        }
    }

    public class DamageTag : ItemTag
    {
        public float BaseEntityDamage { get; }
        public float BaseTileDamage { get; }

        public List<GroupeDamage<EntityBlueprint>> PerEntityDamage { get; set; }
            = new List<GroupeDamage<EntityBlueprint>>();

        public List<GroupeDamage<Tile>> PerTileDamages { get; set; }
            = new List<GroupeDamage<Tile>>();

        public DamageTag(float dmg = 1f, float dmgTile = 1f)
        {
            BaseEntityDamage = dmg;
            BaseTileDamage = dmgTile;
        }

        public float GetDamages(Entity e)
        {
            foreach (var dmg in PerEntityDamage)
                if (e.MemberOf(dmg.Groupe))
                    return dmg.Damages;

            return BaseEntityDamage;
        }

        public float GetDamages(Tile t)
        {
            foreach (var dmg in PerTileDamages)
                if (dmg.Groupe.Members.Contains(t))
                    return dmg.Damages;

            return BaseTileDamage;
        }
    }
}