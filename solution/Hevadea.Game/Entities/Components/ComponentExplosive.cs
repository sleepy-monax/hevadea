using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Tiles.Components;

namespace Hevadea.Entities.Components
{
    public class ComponentExplosive : EntityComponent
    {
        public bool DamageMyself { get; }
        public float Range { get; }
        public float Damages { get; }
        public bool Detonated { get; private set; }

        public ComponentExplosive(bool damageMyself, float range, float damages)
        {
            DamageMyself = damageMyself;
            Range = range;
            Damages = damages;
        }

        public void Detonate()
        {
            if (Detonated) return;
            Detonated = true;

            Owner.GameState.Camera.Thrauma += 0.2f;

            foreach (var c in Owner.Level.QueryCoordinates(Owner.Position, Range * Game.Unit))
            {
                // Apply damages to tiles.
                var tile = Owner.Level.GetTile(c);
                var distance = Mathf.Distance(c.WorldX, c.WorldY, Owner.X, Owner.Y);
                tile.Tag<DamageTile>()?.Hurt(GetDamages(distance) * Rise.Rnd.NextFloat(), c, Owner.Level);

                if (Rise.Rnd.NextDouble() * 1.25 < 1f - distance / (Range * Game.Unit))
                    tile.Tag<BreakableTile>()?.Break(c, Owner.Level);

                // Apply damages to entities
                foreach (var e in Owner.Level.QueryEntity(c))
                    if (DamageMyself || e != Owner)
                    {
                        e.GetComponent<ComponentHealth>()?.Hurt(Owner, GetDamages(distance) * Rise.Rnd.NextFloat());

                        if (e == Owner.GameState.LocalPlayer?.Entity)
                            Owner.GameState.Camera.Thrauma += GetPowerByDistance(distance);

                        if (Rise.Rnd.NextFloat() <= 0.3f)
                        {
                            e.GetComponent<ComponentExplosive>()?.Detonate();
                            e.GetComponent<ComponentBreakable>()?.Break();
                            e.GetComponent<ComponentFlammable>()?.SetInFire();
                        }
                    }
            }
        }

        public float GetPowerByDistance(float distance)
        {
            return 1f - distance / (Range * Game.Unit);
        }

        public float GetDamages(float distance)
        {
            var value = Damages * GetPowerByDistance(distance);
            return value;
        }
    }
}