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
            if (_hasExplosed)
                return;
            _hasExplosed = true;
            var level = AttachedEntity.Level;
            var entities = level.GetEntitiesOnArea(new Rectangle((int)(AttachedEntity.X - _radius * 16),(int)(AttachedEntity.Y - _radius * 16), (int)(_radius*16*2), (int)(_radius*16*2)));
            foreach (var e in entities) if(e != AttachedEntity)
            {
                var distance =  Mathf.Distance(e.X, e.Y, AttachedEntity.X, AttachedEntity.Y); 
                e.Get<Health>()?.Hurt(AttachedEntity, GetDammage(distance) * (float)Rise.Random.NextDouble(), Direction.Up);

                if (Rise.Random.NextDouble()*1.25 <  distance / (_radius*16 ))
                {
                    e.Get<Explode>()?.Do();
                    e.Get<Breakable>()?.Break();
                    e.Get<Burnable>()?.SetOnFire();
                    }
            }
            var pos = AttachedEntity.GetTilePosition();
            for (int x = -(int)_radius; x <= (int)_radius; x++)
            {
                for (int y = -(int)_radius; y <= (int)_radius; y++)
                {
                    var tilePos = new TilePosition(pos.X + x, pos.Y + y);
                    var tile = AttachedEntity.Level.GetTile(tilePos);
                    var distance = Mathf.Distance(tilePos.WorldX, tilePos.WorldY, AttachedEntity.X, AttachedEntity.Y);
                    if (distance < _radius*16)
                    {
                        tile.Tag<Tags.Damage>()?.Hurt(GetDammage(distance)*(float)Rise.Random.NextDouble(), tilePos, AttachedEntity.Level);

                        if (Rise.Random.NextDouble()*1.25 < distance/ (_radius*16))
                        {
                            tile.Tag<Tags.Breakable>()?.Break(tilePos,AttachedEntity.Level);
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
