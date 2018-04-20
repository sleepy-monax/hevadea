using Hevadea.Framework;
using Hevadea.Framework.Utils;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Entities.Components.States;
using Hevadea.GameObjects.Tiles;
using Hevadea.GameObjects.Tiles.Components;

namespace Hevadea.GameObjects.Entities.Components
{
    public class Explode:EntityComponent
    {
        private float _strenght;
        private float _radius;
        private bool _hasExplosed;

        public Explode(float strenght = 1f , float radius = 3f)
        {
            _strenght = strenght;
            _radius = radius;
            _hasExplosed = false;
        }
        public void Do()
        {
            if (_hasExplosed) return;
            _hasExplosed = true;

            var entities = Owner.Level.GetEntitiesOnRadius(Owner.X, Owner.Y, _radius * 16f);

            foreach (var e in entities) 
            {
                if (e != Owner)
                {
                    var distance =  Mathf.Distance(e.X, e.Y, Owner.X, Owner.Y); 
                    e.GetComponent<Health>()?.Hurt(Owner, GetDammage(distance) * Rise.Rnd.NextFloat());

                    if (Rise.Rnd.NextFloat() <= 0.3f)
                    {
                        e.GetComponent<Explode>()?.Do();
                        e.GetComponent<Breakable>()?.Break();
                        e.GetComponent<Burnable>()?.SetInFire();
                    }
                }

            }

            var pos = Owner.GetTilePosition();
            for (int x = -(int)_radius; x <= (int)_radius; x++)
            {
                for (int y = -(int)_radius; y <= (int)_radius; y++)
                {
                    var tilePos = new TilePosition(pos.X + x, pos.Y + y);
                    var tile = Owner.Level.GetTile(tilePos);
                    var distance = Mathf.Distance(tilePos.WorldX, tilePos.WorldY, Owner.X, Owner.Y);

                    if (distance < _radius * 16)
                    {
                        tile.Tag<DamageTile>()?.Hurt(GetDammage(distance)* Rise.Rnd.NextFloat(), tilePos, Owner.Level);

                        if (Rise.Rnd.NextDouble() * 1.25 < 1f-(distance/ (_radius *16)))
                        {
                            tile.Tag<BreakableTile>()?.Break(tilePos,Owner.Level);
                        }

                    }
                }
            }
        }
        public float GetDammage(float distance)
        {

           var value = _strenght*(1f-(distance/(_radius*16)));
            return value;
        }
        
    }
}
