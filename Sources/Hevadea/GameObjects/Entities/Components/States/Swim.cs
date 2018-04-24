using Hevadea.Framework;
using Hevadea.Framework.Graphic.Particles;
using Hevadea.Framework.Utils;
using Hevadea.GameObjects.Tiles.Components;
using Microsoft.Xna.Framework;

namespace Hevadea.GameObjects.Entities.Components.States
{
    public class Swim : EntityComponent, IEntityComponentUpdatable
    {
        public bool IsSwiming   { get; set; } = false;
        public bool WasSwiming { get; set; } = false;
        public bool IsSwimingPainfull { get; set; } = true;
        
        public void Update(GameTime gameTime)
        {
            var health   = Owner.GetComponent<Health>();
            var energy   = Owner.GetComponent<Energy>();
            var position = Owner.GetTilePosition();
            
            if (Owner.Level.IsAll<LiquideTile>(new Rectangle((int)Owner.X - 4, (int)Owner.Y - 4, 8,8)))
            {
                IsSwiming = true;
                if (energy != null && IsSwimingPainfull)
                {
                    energy.EnableNaturalRegeneration = false;
                    if (health != null)
                    {
                        energy.Reduce(0.01f);
                        if (energy.Value < 0.01f){health.Hurt(Owner.GetTileOnMyPosition(), 1f * gameTime.GetDeltaTime(), position.X, position.Y );}
                    }
                }
            }
            else
            {
                IsSwiming = false;
                if (energy!=null) energy.EnableNaturalRegeneration = true;
            }

            if (!WasSwiming && IsSwiming)
            {
                for (int i = 0; i < 10; i++)
                {
                    Owner.Level.ParticleSystem.EmiteAt(new ColoredParticle{ Color = Color.Azure, Life = 0.5f, FadeOut = 0.15f}, Owner.X, Owner.Y, (float)(Rise.Rnd.NextDouble() - 0.5f) * 64f, (float)(Rise.Rnd.NextDouble() - 0.75f) * 20f);
                    Owner.ParticleSystem.EmiteAt(new ColoredParticle{ Color = Color.LightBlue, Life = 0.5f, FadeOut = 0.15f}, Owner.X , Owner.Y, (float)(Rise.Rnd.NextDouble() - 0.5f) * 64f, (float)(Rise.Rnd.NextDouble() - 0.75f) * 20f);
                }
            }
            
            WasSwiming = IsSwiming;
        }
    }
}