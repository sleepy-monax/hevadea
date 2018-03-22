using Hevadea.Framework;
using Hevadea.Framework.Utils;
using Hevadea.Game.Entities.Components.Interaction;
using Hevadea.Game.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Game.Entities.Components.Attributes
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
                        tile.Tag<Tags.Damage>()?.Hurt(GetDammage(distance)* Rise.Rnd.NextFloat(), tilePos, Owner.Level);

                        if (Rise.Rnd.NextDouble() * 1.25 < distance/ (_radius *16))
                        {
                            tile.Tag<Tags.Breakable>()?.Break(tilePos,Owner.Level);
                        }

                    }
                }
            }
        }
        public float GetDammage(float distance)
        {

           var value = _strenght*(distance/(_radius*16));
            return value;
        }
        
    }
}
