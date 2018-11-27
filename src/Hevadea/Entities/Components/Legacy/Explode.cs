using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.States;
using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Utils;
using Hevadea.Tiles.Components;

namespace Hevadea.Entities.Components
{
    public class Explode : EntityComponent
    {
        float _strenght;
        float _radius;
        bool _hasExplosed;

        public Explode(float strenght = 1f, float radius = 3f)
        {
            _strenght = strenght;
            _radius = radius;
            _hasExplosed = false;
        }

        public void Do()
        {
            if (_hasExplosed) return;
            _hasExplosed = true;

            Owner.GameState.Camera.Thrauma += 0.2f;

            foreach (var e in Owner.Level.QueryEntity(Owner.Position, _radius * Game.Unit))
            {
                if (e != Owner)
                {
                    var distance = Mathf.Distance(e.X, e.Y, Owner.X, Owner.Y);
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

            foreach (var c in Owner.Level.QueryCoordinates(Owner.Position, _radius * Game.Unit))
            {
                var tile = Owner.Level.GetTile(c);
                var distance = Mathf.Distance(c.WorldX, c.WorldY, Owner.X, Owner.Y);
                tile.Tag<DamageTile>()?.Hurt(GetDammage(distance) * Rise.Rnd.NextFloat(), c, Owner.Level);

                if (Rise.Rnd.NextDouble() * 1.25 < 1f - (distance / (_radius * Game.Unit)))
                {
                    tile.Tag<BreakableTile>()?.Break(c, Owner.Level);
                }
            }
        }

        float GetPower(float distance)
        {
            return (1f - (distance / (_radius * Game.Unit)));
        }

        public float GetDammage(float distance)
        {
            var value = _strenght * GetPower(distance);
            return value;
        }
    }
}