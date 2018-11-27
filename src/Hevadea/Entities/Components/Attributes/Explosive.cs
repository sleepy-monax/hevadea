using Hevadea.Entities.Components.States;
using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Utils;
using Hevadea.Tiles.Components;

namespace Hevadea.Entities.Components.Attributes
{
    public class Explosive : EntityComponent
    {
        private bool _damageMyself;
        private float _range;
        private float _damages;

        public bool Detonated { get; private set; } = false;

        public Explosive(bool damageMyself, float range, float damages)
        {
            _damageMyself = damageMyself;
            _range = range;
            _damages = damages;
        }

        public void Detonate()
        {
            if (Detonated) return;
            Detonated = true;

            Owner.GameState.Camera.Thrauma += 0.2f;

            foreach (var c in Owner.Level.QueryCoordinates(Owner.Position, _range * Game.Unit))
            {
                // Apply damages to tiles.
                var tile = Owner.Level.GetTile(c);
                var distance = Mathf.Distance(c.WorldX, c.WorldY, Owner.X, Owner.Y);
                tile.Tag<DamageTile>()?.Hurt(GetDammage(distance) * Rise.Rnd.NextFloat(), c, Owner.Level);

                if (Rise.Rnd.NextDouble() * 1.25 < 1f - (distance / (_range * Game.Unit)))
                {
                    tile.Tag<BreakableTile>()?.Break(c, Owner.Level);
                }

                // Apply damages to entities
                foreach (var e in Owner.Level.QueryEntity(c))
                {
                    if (_damageMyself || e != Owner)
                    {
                        e.GetComponent<Health>()?.Hurt(Owner, GetDammage(distance) * Rise.Rnd.NextFloat());

                        if (e == Owner.GameState.LocalPlayer?.Entity)
                        {
                            Owner.GameState.Camera.Thrauma += GetPower(distance);
                        }

                        if (Rise.Rnd.NextFloat() <= 0.3f)
                        {
                            e.GetComponent<Explode>()?.Do();
                            e.GetComponent<Breakable>()?.Break();
                            e.GetComponent<Flammable>()?.SetInFire();
                        }
                    }
                }
            }
        }

        float GetPower(float distance)
        {
            return (1f - (distance / (_range * Game.Unit)));
        }

        public float GetDammage(float distance)
        {
            var value = _damages * GetPower(distance);
            return value;
        }
    }
}
