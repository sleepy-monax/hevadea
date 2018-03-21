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
    class Burnable:Light , IEntityComponentUpdatable
    {
        public bool IsBurn { get; set; } = false;
        private float _dammages;
        private float _chanceToBreak;
        public float SpreadRange = 1.25f;

        public Burnable(float dammages, float chanceToBreak = 0.01f)
        {
            _dammages = dammages;
            IsBurn = false;
            _chanceToBreak = chanceToBreak;
        }

        public void Update(GameTime gameTime)
        {
            IsBurn = !(AttachedEntity.Get<Swim>()?.IsSwiming ?? false) && IsBurn;
            base.On = IsBurn;

            if (IsBurn)
            {
                AttachedEntity.Get<Health>()?.Hurt(AttachedEntity, _dammages*(float)gameTime.ElapsedGameTime.TotalSeconds, Direction.Down);
                if (Rise.Random.NextDouble() < 0.1f)
                {
                    var Entities = AttachedEntity.Level.GetEntitiesOnArea(new Rectangle((int)(AttachedEntity.X - SpreadRange*16), (int)(AttachedEntity.Y - SpreadRange*16), (int)(SpreadRange*16 * 2),(int)(SpreadRange*16 * 2)));
                    foreach(var e in Entities)
                    {
                        if (Rise.Random.NextDouble() < 0.01f)
                        {
                            if (e.Has<Burnable>())
                                e.Get<Burnable>().IsBurn = true;
                            e.Get<Explode>()?.Do();

                        }
                    }
                }
                AttachedEntity.Level.ParticleSystem.EmiteAt(new Color2Particle { Color = Color.Yellow, FadingColor = Color.Red * 0.5f, Life = (float)Rise.Random.NextDouble() * 3f }, AttachedEntity.X + 10 * ((float)Rise.Random.NextDouble() - 0.5f), AttachedEntity.Y + 10 * ((float)Rise.Random.NextDouble() - 0.5f), 10 * ((float)Rise.Random.NextDouble() - 0.5f), -32 * (float)Rise.Random.NextDouble());

                AttachedEntity.ParticleSystem.EmiteAt(new Color2Particle {Size = 8f*(float)Rise.Random.NextDouble(), Color = Color.Yellow, FadingColor = Color.Red * 0.5f, Life = (float)Rise.Random.NextDouble() * 3f},AttachedEntity.X + 10 * ((float)Rise.Random.NextDouble() - 0.5f), AttachedEntity.Y + 10 * ((float)Rise.Random.NextDouble() - 0.5f), 10*((float)Rise.Random.NextDouble()-0.5f) ,-32*(float)Rise.Random.NextDouble());
                if (Rise.Random.NextDouble() < _chanceToBreak)
                   AttachedEntity.Get<Breakable>()?.Break();
            }

        }
    }
}
