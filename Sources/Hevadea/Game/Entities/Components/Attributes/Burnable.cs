using Hevadea.Framework;
using Hevadea.Framework.Graphic.Particles;
using Hevadea.Game.Entities.Components.Interaction;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Game.Entities.Components.Attributes
{
    class Burnable:EntityComponent , IEntityComponentUpdatable
    {
        public bool IsBurn;
        private float _dammages;
        private float _chanceToBreak;

        public Burnable(float dammages, float chanceToBreak = 0.6f)
        {
            _dammages = dammages;
            IsBurn = false;
            _chanceToBreak = chanceToBreak;
        }
        public void SetOnFire()
            => IsBurn = true;

        public void Update(GameTime gameTime)
        {
            if (IsBurn)
            {
                AttachedEntity.Get<Health>()?.Hurt(AttachedEntity, _dammages*(float)gameTime.ElapsedGameTime.TotalSeconds, Direction.Down);
                if (Rise.Random.NextDouble() < 0.1f)
                {
                    var Entities = AttachedEntity.Level.GetEntitiesOnArea(new Rectangle((int)(AttachedEntity.X - 64), (int)(AttachedEntity.Y - 64), 64 * 2, 64 * 2));
                    foreach(var e in Entities)
                    {
                        if (Rise.Random.NextDouble() < 0.001f)
                        {
                            e.Get<Burnable>()?.SetOnFire();
                            e.Get<Explode>()?.Do();

                        }
                    }
                }
                AttachedEntity.ParticleSystem.EmiteAt(new ColoredParticle { Color = Color.Red*0.5f},AttachedEntity.X,AttachedEntity.Y,10*((float)Rise.Random.NextDouble()-0.5f) ,-32*(float)Rise.Random.NextDouble());
                if (Rise.Random.NextDouble() < _chanceToBreak)
                   AttachedEntity.Get<Breakable>()?.Break();
            }

        }
    }
}
