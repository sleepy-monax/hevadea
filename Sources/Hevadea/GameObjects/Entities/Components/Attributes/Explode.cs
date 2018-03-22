using Hevadea.Framework;
using Hevadea.Framework.Utils;
using Hevadea.GameObjects.Entities.Components.Interaction;
using Hevadea.GameObjects.Tiles;
using Microsoft.Xna.Framework;

namespace Hevadea.GameObjects.Entities.Components.Attributes
{
    public class Explode : Component
    {
        private float _strenght;
        private float _radius;
        private bool _hasExplosed;

        private float GetDammage(float distance) => _strenght * (1f - (distance / (_radius * 16)));
        
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

            var entities = Entity.Level.GetEntitiesOnRadius(Entity.X, Entity.Y, _radius * 16f);

            foreach (var e in entities) 
            {
                if (e == Entity) continue;
                
                var distance =  Mathf.Distance(e.X, e.Y, Entity.X, Entity.Y); 
                e.GetComponent<Health>()?.Hurt(Entity, GetDammage(distance) * Rise.Rnd.NextFloat());

                if (!(Rise.Rnd.NextFloat() <= 0.3f)) continue;
                
                e.GetComponent<Explode>()?.Do();
                e.GetComponent<Breakable>()?.Break();
                e.GetComponent<Burnable>()?.SetInFire();
            }

            var pos = Entity.GetTilePosition();
            for (var x = -(int)_radius; x <= (int)_radius; x++)
            {
                for (var y = -(int)_radius; y <= (int)_radius; y++)
                {
                    var tilePos = new TilePosition(pos.X + x, pos.Y + y);
                    var tile = Entity.Level.GetTile(tilePos);
                    var distance = Mathf.Distance(tilePos.WorldX, tilePos.WorldY, Entity.X, Entity.Y);

                    if (distance < _radius * 16)
                    {
                        tile.Tag<Tags.Damage>()?.Hurt(GetDammage(distance)* Rise.Rnd.NextFloat(), tilePos, Entity.Level);

                        if (Rise.Rnd.NextDouble() < 1f - (distance / (_radius *16)))
                        {
                            tile.Tag<Tags.Breakable>()?.Break(tilePos,Entity.Level);
                        }
                    }
                }
            }
        }
    }
}
